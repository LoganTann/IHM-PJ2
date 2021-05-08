### Accueil

* `ComboBox` pour le nom du joueur. Affichera la liste des joueurs déjà connus (cf section suivante)
* `Button` pour démarrer la partie. Disabled si nom < 3 chars
* `Button` pour quitter l'application + confirm msgbox
* `Button` pour afficher le tableau des scores

| pas nom | nom |
| --------- | ---- |
| ![image](https://user-images.githubusercontent.com/28659185/117537806-98ce9d00-b003-11eb-8a49-9aacfa090cc7.png) | ![image](https://user-images.githubusercontent.com/28659185/117537809-9ff5ab00-b003-11eb-95ab-5e4b378014be.png) |

### partie

* 20 cartes donc 5 types de 4 cartes à trouver. Doit être représenté par des `Labels`
* 60 secondes pour les identifier
* `Button` pour quitter la partie + confirmation msgbox
* `Label` pour rappeler le nom du joueur, `Label` pour le temps restant
* Si carte soulevée : 
  * Si carte différente de la précédente soulevée : masque toutes les cartes relevées
  * Si 4 cartes identiques, désactive le label
* Le jeu notifie au joueur le nombre de carrés trouvés et le temps effectué
* Reviens en menu principal une fois fini

| normal | ok |
| ------- | -- |
| ![image](https://user-images.githubusercontent.com/28659185/117537822-b3087b00-b003-11eb-8f95-1ed4f8cc5a58.png) | ![image](https://user-images.githubusercontent.com/28659185/117537893-037fd880-b004-11eb-844a-fed44bcfe7c0.png) |

### enregistrement des joueurs
* Le nom des joueurs ayant joué au moins une partie + meilleur score et stats
* Un module doit garder en mémoire: Nom, nbre max de carrés d'une partie, temps min associé, culul du temps de jeu
* Affichage du tableau des scores
  * stats des différents joueurs sur une `ListBox` synchro
  * Affichage des joueurs triés selon le nombre de carrés identifiés (ex aequo départagés sur le temps associé)
  * combobox pour rechercher un joueur par son nom et affichage des stats sur une msgbox
  * Sélection d'un joueur remplis la combobox par son nom
* Stockage dans un fichier

### dossier à fournir

- pour le 28 mai
- introduction de présentation + fonctionnement et captures d'écran
- Documentation des fonctionnalités et des options créées
- schéma présentant les liens entre les formulaires et les events pour passer de l'un à l'autre
- exemple des inscriptions enregistrées dans les fichiers
- Conclusion pour pistes d'amélioration
