<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_options
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CheckBoxPause = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCartesAleatoire = New System.Windows.Forms.CheckBox()
        Me.labelTimer = New System.Windows.Forms.Label()
        Me.ButtonIncrementTime = New System.Windows.Forms.Button()
        Me.ButtonDecrementTime = New System.Windows.Forms.Button()
        Me.buttonValidation = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CheckBoxPause
        '
        Me.CheckBoxPause.AutoSize = True
        Me.CheckBoxPause.Location = New System.Drawing.Point(110, 90)
        Me.CheckBoxPause.Name = "CheckBoxPause"
        Me.CheckBoxPause.Size = New System.Drawing.Size(287, 21)
        Me.CheckBoxPause.TabIndex = 0
        Me.CheckBoxPause.Text = "activer/désactiver la fonctionnalité pause"
        Me.CheckBoxPause.UseVisualStyleBackColor = True
        '
        'CheckBoxCartesAleatoire
        '
        Me.CheckBoxCartesAleatoire.AutoSize = True
        Me.CheckBoxCartesAleatoire.Location = New System.Drawing.Point(110, 144)
        Me.CheckBoxCartesAleatoire.Name = "CheckBoxCartesAleatoire"
        Me.CheckBoxCartesAleatoire.Size = New System.Drawing.Size(149, 21)
        Me.CheckBoxCartesAleatoire.TabIndex = 1
        Me.CheckBoxCartesAleatoire.Text = "aléatoire désactivé"
        Me.CheckBoxCartesAleatoire.UseVisualStyleBackColor = True
        '
        'labelTimer
        '
        Me.labelTimer.AutoSize = True
        Me.labelTimer.Location = New System.Drawing.Point(182, 220)
        Me.labelTimer.Name = "labelTimer"
        Me.labelTimer.Size = New System.Drawing.Size(51, 17)
        Me.labelTimer.TabIndex = 3
        Me.labelTimer.Text = "Label1"
        '
        'ButtonIncrementTime
        '
        Me.ButtonIncrementTime.Location = New System.Drawing.Point(264, 208)
        Me.ButtonIncrementTime.Name = "ButtonIncrementTime"
        Me.ButtonIncrementTime.Size = New System.Drawing.Size(55, 51)
        Me.ButtonIncrementTime.TabIndex = 5
        Me.ButtonIncrementTime.Text = "+"
        Me.ButtonIncrementTime.UseVisualStyleBackColor = True
        '
        'ButtonDecrementTime
        '
        Me.ButtonDecrementTime.Location = New System.Drawing.Point(87, 208)
        Me.ButtonDecrementTime.Name = "ButtonDecrementTime"
        Me.ButtonDecrementTime.Size = New System.Drawing.Size(55, 51)
        Me.ButtonDecrementTime.TabIndex = 6
        Me.ButtonDecrementTime.Text = "-"
        Me.ButtonDecrementTime.UseVisualStyleBackColor = True
        '
        'buttonValidation
        '
        Me.buttonValidation.Location = New System.Drawing.Point(134, 309)
        Me.buttonValidation.Name = "buttonValidation"
        Me.buttonValidation.Size = New System.Drawing.Size(151, 45)
        Me.buttonValidation.TabIndex = 7
        Me.buttonValidation.Text = "Validation"
        Me.buttonValidation.UseVisualStyleBackColor = True
        '
        'form_options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 395)
        Me.Controls.Add(Me.buttonValidation)
        Me.Controls.Add(Me.ButtonDecrementTime)
        Me.Controls.Add(Me.ButtonIncrementTime)
        Me.Controls.Add(Me.labelTimer)
        Me.Controls.Add(Me.CheckBoxCartesAleatoire)
        Me.Controls.Add(Me.CheckBoxPause)
        Me.Name = "form_options"
        Me.Text = "form_options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBoxPause As CheckBox
    Friend WithEvents CheckBoxCartesAleatoire As CheckBox
    Friend WithEvents labelTimer As Label
    Friend WithEvents ButtonIncrementTime As Button
    Friend WithEvents ButtonDecrementTime As Button
    Friend WithEvents buttonValidation As Button
End Class
