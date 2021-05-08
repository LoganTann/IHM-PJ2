Imports System.IO
Public Class form_game


    Private Const NBR_CARD_TYPES = 5, NBR_SAME_CARDS = 4
    Private allImages(NBR_CARD_TYPES - 1) As Image
    Private allLabels(NBR_CARD_TYPES * NBR_SAME_CARDS - 1) As Label


    ' METHODES UTILITAIRES ------------------------------------------------------------------------------------------------

    ''' <summary>fonction statique transformant un chemin relatif au dossier de projet en chemin absolu. Doit être démarré sur VS2019</summary>
    ''' <param name="filePath">Le chemin relatif au dossier du projet en format windows</param>
    ''' <returns>littéralement Directory.GetCurrentDirectory().Replace("\bin\Debug", "\").Replace("\bin\Release", "\") + filePath</returns>
    Shared Function getFile(filePath As String) As String
        Dim rawCD As String = Directory.GetCurrentDirectory()
        Dim newCD = rawCD.Replace("\bin\Debug", "\").Replace("\bin\Release", "\")
        Return newCD + filePath
    End Function

    ''' <summary>
    ''' Initialise le tableau allImages
    ''' </summary>
    Private Sub loadAllImages()
        For i As Integer = 0 To allImages.Length - 1
            allImages(i) = Image.FromFile(getFile($"images\Card{i}.png"))
        Next
    End Sub

    ''' <summary>
    ''' Initialise le tableau allLabels
    ''' </summary>
    Private Sub loadAllLabels()
        Dim i As Integer = 0
        For row As Integer = 0 To NBR_CARD_TYPES - 1
            For col As Integer = 0 To NBR_SAME_CARDS - 1
                allLabels(i) = New Label
                allLabels(i).Image = allImages(row)
                allLabels(i).Size = allImages(row).Size
                allLabels(i).Text = ""
                allLabels(i).AutoSize = False
                allLabels(i).Name = row ' pas prévu pour mais bon, tant que ça stocke le type d'image
                i += 1
            Next
        Next
    End Sub

    ' EVENTS EFFECTIFS ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Porte d'entrée du formulaire
    ''' </summary>
    Private Sub form_game_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' phase d'initialisation
        loadAllImages()
        loadAllLabels()

        For Each lbl As Label In allLabels
            AddHandler lbl.Click, AddressOf onCardClick
            cards_container.Controls.Add(lbl)
        Next
    End Sub

    Private Sub onCardClick(sender As Object, e As EventArgs)
        ' désactiver une carte (juste pour tester et voir si l'event est triggered)
        sender.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Bouton de confirmation
        Dim result As MsgBoxResult = MsgBox("Quitter ?", MsgBoxStyle.YesNo)
        If (result = MsgBoxResult.Yes) Then

            Me.Hide() ' Quitte le form courant
            Dim accueil As New form_home() ' récupère le menu
            accueil.Show() ' l'affiche
        End If
    End Sub
End Class