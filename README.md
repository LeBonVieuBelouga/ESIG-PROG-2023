# README
- Auteur(s) : **Johan Jaquet, Léo Küttel**
- Version : **V0.1.1**
- Date de mise à jour : **29.08.2023**
- Date de création : **28.08.2023**
- Description :

----
## Table des matières
   
- [Inspiration : Rogue (1980)](#inspiration-rogue-1980)
- [Installation](#installation)
- [Source](#source)
  
----

## Inspiration : Rogue (1980)
<a name="inspiration-rogue-1980"></a>

![Rogue Game](https://github.com/LeBonVieuBelouga/ESIG-PROG-2023/blob/main/res/rogue_screenshot.jpg)

### Description

Rogue est un jeu vidéo de rôle et d'aventure, créé en 1980 par Michael Toy, Glenn Wichman et Ken Arnold. C'est l'un des premiers jeux à utiliser des graphiques ASCII pour représenter le monde du jeu.

Le joueur incarne un aventurier qui explore un donjon rempli de monstres, de trésors et de pièges. Chaque niveau du donjon est généré de manière procédurale, ce qui signifie qu'aucune partie ne se ressemble. L'objectif du joueur est de descendre en profondeur dans le donjon pour trouver le trésor légendaire, l'Amulette de Yendor, et de revenir à la surface vivant.

### Gameplay

- **Personnage**: Au début, le joueur choisit une classe (guerrier, magicien, etc.) et un nom pour son personnage.
- **Contrôles**: Le jeu se joue en tour par tour. Les déplacements se font à l'aide des touches directionnelles (ou des commandes spéciales pour attaquer, ramasser des objets, etc.).
- **Monstres**: Le donjon est peuplé de monstres variés, chacun ayant ses propres capacités et comportements.
- **Objets**: Le joueur peut trouver et ramasser divers objets, tels que des armes, des armures, des potions et des sorts.
- **Santé et Magie**: Le personnage a une certaine quantité de points de vie (HP) et de points de magie. Ils peuvent être restaurés en utilisant des objets ou des compétences spéciales.
- **Mort permanente**: Si le personnage meurt, c'est la fin du jeu. Il n'y a pas de sauvegardes ni de points de contrôle.

### Objectifs

1. Descendre plus profondément: Explorez chaque niveau du donjon, en évitant les monstres et en résolvant des énigmes pour accéder aux niveaux inférieurs.
2. Trouver l'Amulette de Yendor: L'objectif principal est de localiser l'Amulette légendaire qui se trouve quelque part dans les profondeurs du donjon.
3. Survivre et prospérer: Amassez des trésors, équipez-vous pour devenir plus puissant et apprenez à utiliser les sorts et les objets à votre avantage.

### Remarque

Ce chapitre est une version simplifiée et ne couvre pas tous les détails du jeu Rogue. Pour en savoir plus, consultez les ressources historiques et les documentations disponibles.

## Installation
<a name="installation"></a>

1. Clonez ce dépôt : `git clone https://github.com/votre-utilisateur/rogue-1980.git`
2. Naviguez vers le dossier : `cd rogue-1980`
3. Compilez et exécutez le jeu : `gcc rogue.c -o rogue && ./rogue`

## Source 
<a name="source"></a>

- Procedural Landmass Generation (E01: Introduction) : [https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3](https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3)
- Roguelike UML exemple : [https://app.genmymodel.com/api/repository/thrognuk/Roguelike](https://app.genmymodel.com/api/repository/thrognuk/Roguelike)
- Decoded Rogue : [https://www.maizure.org/projects/decoded-rogue/index.html](https://www.maizure.org/projects/decoded-rogue/index.html)
