<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_home
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btn_play = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_play
        '
        Me.btn_play.Location = New System.Drawing.Point(109, 89)
        Me.btn_play.Name = "btn_play"
        Me.btn_play.Size = New System.Drawing.Size(75, 23)
        Me.btn_play.TabIndex = 0
        Me.btn_play.Text = "Jouer"
        Me.btn_play.UseVisualStyleBackColor = True
        '
        'form_home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 263)
        Me.Controls.Add(Me.btn_play)
        Me.Name = "form_home"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_play As Button
End Class
