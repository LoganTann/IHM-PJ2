Public Class form_scores
    Private Sub form_scores_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each j As Joueur In GameStorage.getTabJoueurs()
            statJoueur(j)
        Next
    End Sub

    Private Sub statJoueur(joueur As Joueur)
        Dim stats As String = "Statistiques Joueur : " & vbNewLine
        stats &= $"Nom : {joueur.Nom}" & vbNewLine
        stats &= $"- Temps de jeu: {joueur.cumulTmpJeu} secondes" & vbNewLine
        stats &= $"- Nombre de paires trouvées : {joueur.nbrMaxCarréTrouvés}" & vbNewLine
        stats &= $"  - Temps associé à la dernière paire trouvée: {joueur.tempsMin} secondes"
        MsgBox(stats)
    End Sub
    ' - stats des joueurs sur une ListBox synchro (cf. TP, pas ce qu'il y a de plus joli :/)
    ' - Affichage des joueurs triés selon le nombre de carrés identifiés (ex aequo départagés sur le temps associé)
    '   --> faire sa fonction de tri
    ' - combobox pour rechercher un joueur par son nom et affichage des stats sur une msgbox
    ' - Synchro entre listBox et comboBox
End Class