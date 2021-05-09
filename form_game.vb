Public Class form_game


    Private Const NBR_CARD_TYPES = 5, NBR_SAME_CARDS = 4
    Private allImages(NBR_CARD_TYPES - 1) As Image
    Private allLabels(NBR_CARD_TYPES * NBR_SAME_CARDS - 1) As Label


    ' METHODES D'INITIALISATION ------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Initialise le tableau allImages
    ''' </summary>
    Private Sub loadAllImages()
        For i As Integer = 0 To allImages.Length - 1
            allImages(i) = Image.FromFile(GameUtils.getFile($"images\Card{i}.png"))
        Next
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
                    .Image = allImages(row)
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

        ' TODO : initialiser le nom du joueur
        ' TODO : initialiser le timer
    End Sub

    Private Sub onCardClick(sender As Object, e As EventArgs)
        ' TODO : Logique de jeu
        ' désactiver une carte (juste pour __tester__ et voir si l'event est triggered)
        sender.Enabled = False
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