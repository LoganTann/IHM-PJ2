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


    Private tabJoueurs As Joueur()



End Module
