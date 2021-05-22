Imports System.IO
Module GameUtils
    ''' <summary>
    ''' Crée une nouvelle MsgBox de type Yes/No et retourne true si yes a été cliqué, false sinon.
    ''' Conçu pour agir comme le confirm("message") de Javascript
    ''' </summary>
    ''' <param name="message">Le message que doit contenir la MsgBox</param>
    ''' <returns>true si le bouton "oui" a été cliqué, false sinon</returns>
    Public Function confirm(message As String) As Boolean
        Return MsgBox(message, MsgBoxStyle.YesNo) = MsgBoxResult.Yes
    End Function


    ''' <summary>fonction statique transformant un chemin relatif au dossier de projet en chemin absolu. Doit être démarré sur VS2019</summary>
    ''' <param name="filePath">Le chemin relatif au dossier du projet en format windows</param>
    ''' <returns>littéralement Directory.GetCurrentDirectory().Replace("\bin\Debug", "\").Replace("\bin\Release", "\") + filePath</returns>
    Public Function getFile(filePath As String, replace As Boolean) As String
        Dim rawCD As String = Directory.GetCurrentDirectory()
        If (replace) Then
            Dim newCD = rawCD.Replace("\bin\Debug", "\").Replace("\bin\Release", "\")
            Return newCD + filePath
        Else
            Return rawCD + "\" + filePath
        End If
    End Function

    Public Function CD() As String
        Return Directory.GetCurrentDirectory()
    End Function
End Module
