Public Class form_scores
    Private sortedArray As List(Of Joueur)
    Private Sub form_scores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sortedArray = GameStorage.getTabJoueurs()
        sortedArray.Sort(New Comparison(Of Joueur)(AddressOf Joueur_compareTo))

        updateListBoxDisplay(False)
    End Sub

    Private Sub updateListBoxDisplay(sortDesc As Boolean)
        clearAll()
        If (sortDesc) Then
            For i As Integer = sortedArray.Count - 1 To 0 Step -1
                insertNames(sortedArray(i))
            Next
        Else
            For Each player As Joueur In sortedArray
                insertNames(player)
            Next
        End If
        profileName.SelectedIndex = 0
    End Sub

    Private Sub clearAll()
        playerCombo.Items.Clear()
        profileName.Items.Clear()
        totalTime.Items.Clear()
        totalPlays.Items.Clear()
        score.Items.Clear()
        scoreTime.Items.Clear()
    End Sub

    Private Sub insertNames(j As Joueur)
        playerCombo.Items.Add(j.Nom)
        profileName.Items.Add(j.Nom)
        totalTime.Items.Add(j.cumulTmpJeu)
        totalPlays.Items.Add("todo")
        score.Items.Add(j.nbrMaxCarréTrouvés)
        scoreTime.Items.Add(j.tempsMin)
    End Sub

    ''' <summary>
    '''  Fonction de comparaison entre deux joueurs. Ref : https://www.dotnetperls.com/sort-vbnet
    ''' </summary>
    Private Function Joueur_compareTo(valueA As Joueur, valueB As Joueur) As Integer
        ' Carrés triés par ordre décroissant
        Dim fistSortComparaison = valueB.nbrMaxCarréTrouvés.CompareTo(valueA.nbrMaxCarréTrouvés)
        If (fistSortComparaison = 0) Then
            ' Temps trié par ordre croissant si nombre de carrés identiques
            Return valueA.tempsMin.CompareTo(valueB.tempsMin)
        Else
            Return fistSortComparaison
        End If
    End Function

    Private Sub statJoueur(joueur As Joueur)
        Dim stats As String = "Statistiques Joueur : " & vbNewLine
        stats &= $"Nom : {joueur.Nom}" & vbNewLine
        stats &= $"- Temps de jeu: {joueur.cumulTmpJeu} secondes" & vbNewLine
        stats &= $"- Nombre de paires trouvées : {joueur.nbrMaxCarréTrouvés}" & vbNewLine
        stats &= $"  - Temps associé à la dernière paire trouvée: {joueur.tempsMin} secondes"
        MsgBox(stats)
    End Sub

    Private Sub checkbox_SortOrder_CheckedChanged(sender As Object, e As EventArgs) Handles checkbox_SortOrder.CheckedChanged
        If (checkbox_SortOrder.Checked) Then
            checkbox_SortOrder.Text = "▲"
            updateListBoxDisplay(True)
        Else
            checkbox_SortOrder.Text = "▼"
            updateListBoxDisplay(False)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub syncAllBoxes(sender As Object, e As EventArgs) _
        Handles profileName.SelectedIndexChanged, totalTime.SelectedIndexChanged,
                totalPlays.SelectedIndexChanged, score.SelectedIndexChanged,
                scoreTime.SelectedIndexChanged, playerCombo.SelectedIndexChanged
        profileName.SelectedIndex = sender.SelectedIndex
        totalTime.SelectedIndex = sender.SelectedIndex
        totalPlays.SelectedIndex = sender.SelectedIndex
        score.SelectedIndex = sender.SelectedIndex
        scoreTime.SelectedIndex = sender.SelectedIndex
        playerCombo.SelectedIndex = sender.SelectedIndex
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim isSortedDesc = checkbox_SortOrder.Checked
        If (isSortedDesc) Then
            statJoueur(sortedArray(sortedArray.Count() - 1 - playerCombo.SelectedIndex))
        Else
            statJoueur(sortedArray(playerCombo.SelectedIndex))
        End If
    End Sub
End Class