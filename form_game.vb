﻿Imports System.Threading

Public Class form_game

    Private Const NBR_CARD_TYPES = 5, NBR_SAME_CARDS = 4
    Private allImages(NBR_CARD_TYPES - 1) As Image
    Private imageBackCard As Image
    Private allLabels(NBR_CARD_TYPES * NBR_SAME_CARDS - 1) As Label
    Private lastCard As String
    Private compteurCartesTrouvées As Integer = 0
    Private compteurTypesCartesTrouvée As Integer = 0
    Private WithEvents timer1 As New System.Windows.Forms.Timer()
    Private Const ALLOWED_TIME As Integer = 10
    Private remainingTime As Integer = ALLOWED_TIME
    Private lastFoundTime As Integer = ALLOWED_TIME


    ' METHODES D'INITIALISATION ------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Initialise le tableau allImages
    ''' </summary>
    Private Sub loadAllImages(shouldReplace As Boolean)
        Try
            For i As Integer = 0 To allImages.Length - 1
                allImages(i) = Image.FromFile(GameUtils.getFile($"images\Card{i}.png", shouldReplace))
            Next
            imageBackCard = Image.FromFile(GameUtils.getFile($"images\BackCard.png", shouldReplace))
        Catch ex1 As System.IO.FileNotFoundException
            If (shouldReplace) Then
                loadAllImages(False)
            Else
                MsgBox("Erreur lors du chargement des images. Nous avons tout fait pour chercher mais nous n'avons pas trouvé le fichier : " &
                vbNewLine & ex1.Message(), MsgBoxStyle.Critical, "Impossible de poursuivre...")
                exitToMenu() ' TODO : ne dois pas afficher seconde erreur
            End If
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
    ''' Ajoute les cartes (labels) préchargées au sein du flowLayoutPanel présent dans le formulaire, de manière tirée
    ''' </summary>
    Private Sub printAllCardLabels_sorted()
        For Each lbl As Label In allLabels
            cards_container.Controls.Add(lbl) ' assez verbeux pour comprendre ce que ça fait
        Next
    End Sub

    ''' <summary>
    ''' printAllCardLabels_sorted mais de manière random
    ''' </summary>
    Private Sub printAllCardLabels_random()
        ' allLabels
        ' cards_container.Controls.Add()
    End Sub

    ' EVENTS ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Porte d'entrée du formulaire
    ''' </summary>
    Private Sub onFormLoad(sender As Object, e As EventArgs) Handles Me.Load
        ' phase d'initialisation
        loadAllImages(True)
        loadAllCardLabels()
        printAllCardLabels_sorted()

        timer1.Interval = 1000

        lbl_profile.Text = GameStorage.getPlayerName()
        lbl_time.Text = secsToStr(remainingTime, "mm:ss")
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
            lastFoundTime = ALLOWED_TIME - remainingTime

            ' Si tous les types ont été trouvés : gagné !
            If compteurTypesCartesTrouvée = NBR_CARD_TYPES Then
                onGameFinished()
            End If
        End If
    End Sub

    Private Sub onGameFinished()
        timer1.Enabled = False

        GameStorage.updateCurrentPlayerScore(ALLOWED_TIME - remainingTime, compteurTypesCartesTrouvée, lastFoundTime)

        GameStorage.Sauvegarder()

        exitToMenu()
    End Sub

    ''' <summary> Au clic du bouton rouge natif de fermeture </summary>
    Private Sub onWindowClosing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyClass.Closing
        e.Cancel = True
        ConfirmClose()
    End Sub
    ''' <summary> Au clic du bouton "quitter" </summary>
    Private Sub onQuitbtnClick(sender As Object, e As EventArgs) Handles quitBtn.Click
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


    ''' <summary>
    ''' Affiche une boite de confirmation de fermeture, et appelle exitToMenu() si clique sur OK.
    ''' </summary>
    Private Sub ConfirmClose()
        ' Bouton de confirmation
        If (GameUtils.confirm(CLOSE_CONFIRM_MSG)) Then
            exitToMenu()
        End If
    End Sub
    Private Const CLOSE_CONFIRM_MSG = "Voulez-vous vraiment abandonner la partie en cours ?"

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

    ''' <summary>
    ''' Donné une valeur en secondes, convertis en minutes/secondes selon le template mis en paramètre.
    ''' Ex : donné 90 secs et la chaine "mm minutes et ss secondes", retourne "1 minutes et 30 secondes" 
    ''' </summary>
    ''' <param name="time">Le temps en secondes. Cette fonction est conçue pour un maximum de 3599 secondes (59 minutes 59 secs)</param>
    ''' <param name="template">Une chaine de caractères où l'occurence "mm" sera remplacée par la valeur des minutes, et "ss" sera remplacée par la valeur des secondes</param>
    ''' <returns>(time, template) => template.Replace("ss", time Mod 60).Replace("mm",  (time - (time Mod 60)) / 60)</returns>
    Private Function secsToStr(time As Integer, template As String) As String
        Dim ss As Integer = time Mod 60
        Dim mm As Integer = (time - ss) / 60
        Return template.Replace("ss", ss).Replace("mm", mm)
    End Function
End Class