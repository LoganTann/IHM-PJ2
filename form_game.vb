Imports System.Threading

Public Class form_game


    Private Const NBR_CARD_TYPES = 5, NBR_SAME_CARDS = 4
    Private allImages(NBR_CARD_TYPES - 1) As Image
    Private imageBackCard As Image
    Private allLabels(NBR_CARD_TYPES * NBR_SAME_CARDS - 1) As Label
    Private lastCart As String
    Private compteurCartesTrouvées As Integer = 0
    Private compteurTypesCartesTrouvée As Integer = 0
    Private timerDémarée As Boolean = False
    Private WithEvents timer1 As New Timer(1000)



    ' METHODES D'INITIALISATION ------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Initialise le tableau allImages
    ''' </summary>
    Private Sub loadAllImages()
        For i As Integer = 0 To allImages.Length - 1
            allImages(i) = Image.FromFile(GameUtils.getFile($"images\Card{i}.png"))
        Next
        imageBackCard = Image.FromFile(GameUtils.getFile($"images\BackCard.png"))

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

    ' EVENTS EFFECTIFS ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Porte d'entrée du formulaire
    ''' </summary>
    Private Sub form_game_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' phase d'initialisation
        loadAllImages()
        loadAllCardLabels()
        printAllCardLabels()

        timer1.Interval = 1000

        ' TODO : initialiser le nom du joueur
        ' TODO : initialiser le timer
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles timer1.Elapsed
        ' ceci s'exécute toutes les secondes
    End Sub

    Private Sub onCardClick(sender As Object, e As EventArgs)
        ' TODO : Logique de jeu
        ' désactiver une carte (juste pour __tester__ et voir si l'event est triggered)
        ' sender.Enabled = False

        If timerDémarée = False Then
            timerDémarée = True
            timer1.Enabled = True
            timer1.Start()

        End If

        If Not sender.Image.Equals(imageBackCard) Then
            Exit Sub
        End If

        If compteurCartesTrouvées > 0 Then
            If sender.Name.Equals(lastCart) Then
                compteurCartesTrouvées += 1

            Else

                sender.image = allImages(sender.Name)

                Me.Refresh()

                Thread.Sleep(1000)

                For Each lbl As Label In cards_container.Controls
                    If lbl.Enabled = True Then
                        If Not lbl.Image.Equals(imageBackCard) Then
                            lbl.Image = imageBackCard
                        End If

                    End If

                Next

                compteurCartesTrouvées = 0

                Exit Sub

            End If
        Else
            compteurCartesTrouvées = 1
        End If

        lastCart = sender.Name


        If compteurCartesTrouvées = 4 Then
            For Each lbl As Label In cards_container.Controls
                If lbl.Name.Equals(lastCart) Then
                    lbl.Enabled = False
                End If

            Next
            compteurCartesTrouvées = 0
            compteurTypesCartesTrouvée += 1
        End If

        If compteurTypesCartesTrouvée = 5 Then
            MsgBox("Vous avez avez gagné la partie")
        End If


        sender.image = allImages(sender.Name)

    End Sub

    ' TODO : Lorsque le jeu est fini, enregistrer le score si nécessaire + affichages.


    ' Confirmation de fermeture
    Private Const CLOSE_CONFIRM_MSG = "Voulez-vous vraiment abandonner la partie en cours ?"
    Private Sub ConfirmClose()
        ' Bouton de confirmation
        If (GameUtils.confirm(CLOSE_CONFIRM_MSG)) Then
            Me.Hide() ' Quitte le form courant
            Dim accueil As New form_home() ' récupère le menu
            accueil.Show() ' l'affiche
        End If
    End Sub
    Private Sub ConfirmClose_native(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyClass.Closing
        ' Au clic du bouton rouge natif de fermeture
        e.Cancel = True
        ConfirmClose()
    End Sub
    Private Sub ConfirmClose_btn(sender As Object, e As EventArgs) Handles Button1.Click
        ' Au clic du bouton "quitter"
        ConfirmClose()
    End Sub
End Class