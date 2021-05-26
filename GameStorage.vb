Imports System.Xml

Module GameStorage

    ' Stockage d'un joueur:
    ' - Nom,
    ' - nbre max de carrés d'une partie
    ' - temps min associé
    ' - cumul du temps de jeu
    Public Structure Joueur
        Public Nom As String
        Public nbrMaxCarréTrouvés As Integer
        Public tempsMin As Integer
        Public cumulTmpJeu As Integer
    End Structure


    ' Stockage de paramètres : 
    ' - réglage du temps alloué au joueur (0 pour désactiver) -> via un picker
    ' - Thème utilisé -> ListBox 
    ' - Chemin du fichier de sauvegarde (!) -> chemin
    ' - Le pause sera par défaut

    Public Structure Paramètres
        Public tempsMax As Integer
        Public themeUtilise As String
        Public cheminSauvegarde As String
        Public pause As Boolean
    End Structure


    Private tabJoueurs As New List(Of Joueur)
    Private stockageParamètres As Paramètres

    Private idJoueurCourant As Integer

    Private hasInit As Boolean = False

    Public Sub init()
        If (hasInit) Then
            Exit Sub
        End If
        hasInit = True
        Charger()
    End Sub

    Public Function getTabJoueurs() As List(Of Joueur)
        Return tabJoueurs
    End Function

    Public Function addProfile(nom As String)
        Dim nouveauJoueur As Joueur
        With nouveauJoueur
            .Nom = nom
            .nbrMaxCarréTrouvés = 0
            .tempsMin = 0
            .cumulTmpJeu = 0
        End With
        tabJoueurs.Add(nouveauJoueur)
        Return nouveauJoueur
    End Function

    Public Function listeNoms() As List(Of String)
        Dim returnValue As New List(Of String)
        For Each j As Joueur In tabJoueurs
            returnValue.Add(j.Nom)
        Next
        Return returnValue
    End Function

    Public Sub setPlayerName(s As String)
        Dim NameExists As Boolean = False
        Dim i As Integer = 0
        For Each j As Joueur In tabJoueurs
            If (j.Nom.Equals(s)) Then
                NameExists = True
                idJoueurCourant = i
            End If
            i += 1
        Next
        If (Not NameExists) Then
            addProfile(s)
            idJoueurCourant = i
        End If
    End Sub

    Public Sub updateCurrentPlayerScore(TempsTotalDeJeu As Integer, NombreDePairesTrouvees As Integer, TempsAssocié As Integer)
        ' pour modifier un élément du tableau on doit copier cet element dans une variable locale 
        ' puis redéfinir l'élément du tableau par la variable locale modifiée

        Dim joueurCourant As Joueur = tabJoueurs(idJoueurCourant)
        joueurCourant.cumulTmpJeu += TempsTotalDeJeu

        If (NombreDePairesTrouvees > joueurCourant.nbrMaxCarréTrouvés) Then
            ' Si paires supérieure : on mets à jour paires et son temps
            joueurCourant.nbrMaxCarréTrouvés = NombreDePairesTrouvees
            joueurCourant.tempsMin = TempsAssocié
        ElseIf (NombreDePairesTrouvees = joueurCourant.nbrMaxCarréTrouvés) Then
            ' Si paires identiques mais temps meilleur : modifier le temps
            If (TempsAssocié > joueurCourant.tempsMin) Then
                joueurCourant.tempsMin = TempsAssocié
            End If
        End If

        tabJoueurs(idJoueurCourant) = joueurCourant
    End Sub

    Public Sub Sauvegarder()
        Dim fichierDeSauvegarde As XmlDocument = New XmlDocument()
        fichierDeSauvegarde.LoadXml("<jeu></jeu>")
        Dim balise_joueurs As XmlElement
        balise_joueurs = fichierDeSauvegarde.CreateElement("joueurs")

        For Each j As Joueur In tabJoueurs
            Dim balise_joueur As XmlElement
            Dim balise_nom As XmlElement
            Dim balise_tempsCumul As XmlElement
            Dim balise_paires As XmlElement
            Dim balise_tempsAssocie As XmlElement
            balise_joueur = fichierDeSauvegarde.CreateElement("joueur")
            balise_nom = fichierDeSauvegarde.CreateElement("nom")
            balise_tempsCumul = fichierDeSauvegarde.CreateElement("tempsCumul")
            balise_paires = fichierDeSauvegarde.CreateElement("paires")
            balise_tempsAssocie = fichierDeSauvegarde.CreateElement("tempsAssocie")

            balise_nom.InnerText = j.Nom
            balise_tempsCumul.InnerText = j.cumulTmpJeu
            balise_paires.InnerText = j.nbrMaxCarréTrouvés
            balise_tempsAssocie.InnerText = j.tempsMin

            balise_joueur.AppendChild(balise_nom)
            balise_joueur.AppendChild(balise_tempsCumul)
            balise_joueur.AppendChild(balise_paires)
            balise_joueur.AppendChild(balise_tempsAssocie)
            balise_joueurs.AppendChild(balise_joueur)
        Next
        fichierDeSauvegarde.DocumentElement.AppendChild(balise_joueurs)
        fichierDeSauvegarde.Save(GameUtils.CD() + "\sauvegarde.XML")
    End Sub

    Public Sub Charger()
        Dim fichierDeSauvegarde As XmlDocument = New XmlDocument()

        Try
            fichierDeSauvegarde.Load(GameUtils.CD() + "\sauvegarde.XML")
        Catch ex As Exception
            addProfile("Logan")
            addProfile("Sofiane")
            Sauvegarder()
            Exit Sub
        End Try


        Dim balises_joueur As XmlNodeList
        balises_joueur = fichierDeSauvegarde.DocumentElement.GetElementsByTagName("joueur")
        For Each uneBalise_joueur In balises_joueur
            Dim joueurCharge As New Joueur
            For Each infosJoueur In uneBalise_joueur.ChildNodes
                Select Case infosJoueur.LocalName
                    Case "nom"
                        joueurCharge.Nom = infosJoueur.InnerText
                    Case "tempsCumul"
                        joueurCharge.cumulTmpJeu = infosJoueur.InnerText
                    Case "paires"
                        joueurCharge.nbrMaxCarréTrouvés = infosJoueur.InnerText
                    Case "tempsAssocie"
                        joueurCharge.tempsMin = infosJoueur.InnerText
                End Select
            Next
            tabJoueurs.Add(joueurCharge)
        Next
    End Sub

    Public Function getPlayerName()
        Dim retval As String = "Houston, we have a problem"
        Try
            retval = tabJoueurs(idJoueurCourant).Nom
        Catch ex As Exception
        End Try
        Return retval
    End Function

End Module
