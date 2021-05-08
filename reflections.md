### form_homepage

![screen](https://user-images.githubusercontent.com/28659185/117537809-9ff5ab00-b003-11eb-95ab-5e4b378014be.png)

**notes** : 
* Pas clair dans l'énoncé : Combobox ou textbox ou les deux ??
* 3 labels, 1 textbox, 4 boutons

**Fonctions**

validName: true si le nom entré est valide, faux sinon
* `return textbox_name.text.lengh >= 3`

**Elements**

textBox_name: 
* event onChange: Quand le texte change, on active ou pas le bouton play selon la validité de l'entrée
  ```vb
  button_play.Enabled = validName();
  ```

btn_play: (+ Scores + Options)
* even onClick: ouvre le formulaire approprié et ferme l'autre
  ```vb
  Me.hide()
  Dim newGame As new form_game();
  newGame.show()
  ```

btn_quit:
* even onClick: confirmation et si oui, quitter
  ```vb
  Dim result as MsgBoxResult = MsgBox("Quitter ?", MsgBoxStyle.YesNo)
  if (result = MsgBoxResult.Yes) then
    ' [commande pour quitter l'appli]
  end if
  ```
