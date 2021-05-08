Public Class form_home
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_play.Click
        Me.Hide()
        Dim newGame As New form_game()
        newGame.Show()
    End Sub
End Class
