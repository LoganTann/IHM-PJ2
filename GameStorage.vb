Module GameStorage
    ' TODO : Ceci doit garder en mémoire les __paramètres du jeux__ et l'ensemble des __joueurs__

    ' TODO : fonction pour sauvegarder ou charger les paramètres sur un fichier

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
    ' - (facultatif) CheaterMode qui désactive le random et les vérifications -> code konami

    Public Structure Paramètres
        Public tempsMax As Integer
        Public thèmeUtilisé As String
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
        addProfile("Logan")
        addProfile("Sofiane")
        addProfile("Lucas")
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



    Public Function getPlayerName()
        ' TODO vérifier si bien init
        Return tabJoueurs(idJoueurCourant).Nom
    End Function

End Module
