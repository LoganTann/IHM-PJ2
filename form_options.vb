Public Class form_options

    Private allowPause As Boolean

    Private disableRandom As Boolean

    Private time As Integer

    Private Sub form_options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim param As Paramètres = GameStorage.getParamètre()

        allowPause = param.allowPause
        disableRandom = param.disableRandom
        time = param.allowedTime

        CheckBoxPause.Checked = allowPause
        CheckBoxCartesAleatoire.Checked = disableRandom
        labelTimer.Text = form_game.secsToStr(time, "mm:ss")


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPause.CheckedChanged
        allowPause = CheckBoxPause.Checked


    End Sub



    Private Sub updateLabel()
        labelTimer.Text = form_game.secsToStr(time, "mm:ss")

        If time >= 180 Then
            ButtonIncrementTime.Enabled = False
        End If

        If time <= 0 Then
            ButtonDecrementTime.Enabled = False
        End If


    End Sub
    Private Sub ButtonIncrementTime_Click(sender As Object, e As EventArgs) Handles ButtonIncrementTime.Click
        If time < 180 Then
            time += 15
            updateLabel()
        End If

        ButtonDecrementTime.Enabled = True

    End Sub
    Private Sub ButtonDecrementTime_Click(sender As Object, e As EventArgs) Handles ButtonDecrementTime.Click
        If time > 0 Then
            time -= 15
            updateLabel()
        End If

        ButtonIncrementTime.Enabled = True

    End Sub

    Private Sub CheckBoxCartesAleatoire_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCartesAleatoire.CheckedChanged
        disableRandom = CheckBoxCartesAleatoire.Checked


    End Sub

    Private Sub buttonValidation_Click(sender As Object, e As EventArgs) Handles buttonValidation.Click

        Dim newParamètre As Paramètres
        newParamètre.allowPause = allowPause
        newParamètre.disableRandom = disableRandom
        newParamètre.allowedTime = time

        GameStorage.setParamètre(newParamètre)

        GameStorage.Sauvegarder()


        Me.Hide()
        form_home.Show()

    End Sub
End Class