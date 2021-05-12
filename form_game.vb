Imports System.Threading

Public Class form_game

    Private Const NBR_CARD_TYPES = 5, NBR_SAME_CARDS = 4
    Private allImages(NBR_CARD_TYPES - 1) As Image
    Private imageBackCard As Image
    Private allLabels(NBR_CARD_TYPES * NBR_SAME_CARDS - 1) As Label
    Private lastCard As String
    Private compteurCartesTrouvées As Integer = 0
    Private compteurTypesCartesTrouvée As Integer = 0
    Private WithEvents timer1 As New System.Windows.Forms.Timer()
    Private remainingTime As Integer = 60


    ' METHODES D'INITIALISATION ------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Initialise le tableau allImages
    ''' </summary>
    Private Sub loadAllImages()
        Try
            For i As Integer = 0 To allImages.Length - 1
                allImages(i) = Image.FromFile(GameUtils.getFile($"images\Card{i}.png"))
            Next
            imageBackCard = Image.FromFile(GameUtils.getFile($"images\BackCard.png"))
        Catch ex As System.IO.FileNotFoundException
            MsgBox("Erreur lors du chargement des images. Ce projet est (pour le moment) conçu pour être démarré dans le dossier Debug ou Release de VisualStudio, les assets se trouvant dans la racine du projet. Information sur l'erreur : " &
                   vbNewLine & ex.Message(), MsgBoxStyle.Critical, "Impossible de poursuivre...")
        End Try
    End Sub

    ''' <summary>
    ''' Initialise le tableau allLabels
    ''' </summary>
    Private Sub loadAllCardLabels()
        Dim i As Integer = 0
        For row As Integer = 0 To NBR_CARD_TYPES - 1
            For col As Integer = 0 To NBR_SAME_CARDS - 1
                allLabels(i) = New Label
                ' Ajoute l'event "au clic d'une carte, faire..."
                AddHandler allLabels(i).Click, AddressOf onCardClick
                ' Un peu de customisation : 
                With allLabels(i)
                    .Image = imageBackCard
                    .Size = allImages(row).Size
                    .Text = ""
                    .AutoSize = False
                    .Name = row ' pas prévu pour mais bon, tant que ça stocke le type d'image
                End With
                i += 1
            Next
        Next
    End Sub
    ''' <summary>
    ''' Affiche les cartes dans le formulaire. Sera utile si jamais on veut activer le random ou pas.
    ''' </summary>
    Private Sub printAllCardLabels()
        For Each lbl As Label In allLabels
            cards_container.Controls.Add(lbl) ' assez verbeux pour comprendre ce que ça fait
        Next
    End Sub

    ' EVENTS OU METHODES D'ACTION ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Porte d'entrée du formulaire
    ''' </summary>
    Private Sub onLoad(sender As Object, e As EventArgs) Handles Me.Load
        ' phase d'initialisation
        loadAllImages()
        loadAllCardLabels()
        printAllCardLabels()

        timer1.Interval = 1000

        ' TODO : initialiser le nom du joueur
    End Sub

    Private Sub onEachSecs(sender As Object, e As System.EventArgs) Handles timer1.Tick
        remainingTime -= 1
        lbl_time.Text = secsToStr(remainingTime, "mm:ss")

        If (remainingTime <= 0) Then
            onGameFinished()
        ElseIf (remainingTime <= 10) Then
            lbl_time.ForeColor = Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Action lorsqu'une carte est cliquée. La principale logique du jeu est ici, ce n'est pas forcément l'implémentation la plus élégante ou optimisée
    ''' mais on a tenté d'implémenter ce qu'on ferait dans notre tête si c'était une partie physique
    ''' </summary>
    Private Sub onCardClick(clickedCard As Label, e As EventArgs)
        ' (démarre le timer si ce n'est pas encore le cas, ie. une fois la première carte retournée)
        If Not timer1.Enabled Then
            timer1.Enabled = True
            timer1.Start()
        End If

        ' retourne la carte, mais si déjà révélée, pas la peine d'aller plus loin le coup est invalide
        If clickedCard.Image.Equals(imageBackCard) Then
            clickedCard.Image = allImages(clickedCard.Name)
        Else
            Exit Sub
        End If

        ' Si la carte est différente de la carte précédente retournée
        If compteurCartesTrouvées > 0 And Not clickedCard.Name.Equals(lastCard) Then
            ' dévoile la carte, mets en pause le thread en pensant à re-paint le formulaire avant l'interruption
            Me.Refresh()
            Thread.Sleep(1000)

            flipAllRevealedCards()
            compteurCartesTrouvées = 0
            Exit Sub
        Else ' Sinon on update juste les valeurs de vérifications
            compteurCartesTrouvées += 1
            lastCard = clickedCard.Name
        End If

        ' Si toutes les cartes du même type ont été révélées
        If compteurCartesTrouvées = NBR_SAME_CARDS Then
            disableAllCardsOfType(lastCard)
            compteurCartesTrouvées = 0
            compteurTypesCartesTrouvée += 1
        End If

        ' Si tous les types ont été trouvés : gagné !
        If compteurTypesCartesTrouvée = NBR_CARD_TYPES Then
            onGameFinished()
        End If


        ' clickedCard.Image = allImages(clickedCard.Name)
    End Sub

    Private Sub onGameFinished()
        timer1.Enabled = False

        Dim stats As String = "Statistiques de fin de partie : "
        stats &= "Temps passé: " & secsToStr(remainingTime, "mm min ss") & vbNewLine
        stats &= "Nombre de paires trouvées : " & compteurTypesCartesTrouvée
        MsgBox(stats)

        ' TODO : Lorsque le jeu est fini, enregistrer le score si nécessaire + affichages.

        exitToMenu()
    End Sub

    ''' <summary> Au clic du bouton rouge natif de fermeture </summary>
    Private Sub ConfirmClose_native(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyClass.Closing
        e.Cancel = True
        ConfirmClose()
    End Sub
    ''' <summary> Au clic du bouton "quitter" </summary>
    Private Sub ConfirmClose_btn(sender As Object, e As EventArgs) Handles Button1.Click
        ConfirmClose()
    End Sub


    ' METHODES UTILITAIRES ------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Ferme le formulaire courant, et ouvre form_home
    ''' </summary>
    Private Sub exitToMenu()
        Me.Hide() ' Quitte le form courant
        Dim accueil As New form_home() ' récupère le menu
        accueil.Show() ' l'affiche
    End Sub

    ' Confirmation de fermeture
    Private Const CLOSE_CONFIRM_MSG = "Voulez-vous vraiment abandonner la partie en cours ?"
    Private Sub ConfirmClose()
        ' Bouton de confirmation
        If (GameUtils.confirm(CLOSE_CONFIRM_MSG)) Then
            exitToMenu()
        End If
    End Sub

    Private Sub disableAllCardsOfType(cardType As String)
        For Each lbl As Label In cards_container.Controls
            If lbl.Name.Equals(cardType) Then
                lbl.Enabled = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' cache chaque cartes révélées et non grisées du container
    ''' </summary>
    Private Sub flipAllRevealedCards()
        For Each lbl As Label In cards_container.Controls
            If lbl.Enabled And Not lbl.Image.Equals(imageBackCard) Then
                lbl.Image = imageBackCard
            End If
        Next
    End Sub

    Private Function secsToStr(time As Integer, template As String) As String
        Dim ss As Integer = time Mod 60
        Dim mm As Integer = (time - ss) / 60
        Return template.Replace("ss", ss).Replace("mm", mm)
    End Function
End Class