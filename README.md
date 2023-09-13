# README
- Auteur(s) : **Johan Jaquet, Léo Küttel**
- Version : **V0.1.2**
- Date de mise à jour : **07.09.2023**
- Date de création : **28.08.2023**
- Description :

----
## Table des matières
   
- [Inspiration : Rogue (1980)](#inspiration-rogue-1980)
- [Installation](#installation)
- [Source](#source)
  
----

## Nomenclature des Variables

Ce tableau présente une nomenclature des variables utilisant des préfixes pour indiquer leur visibilité ou leur rôle.

| Type      | Préfixe   | Exemple              | Description |
|-----------|-----------|----------------------|-------------|
| Membre    | m_        | m_VariableOne        | Variables utilisées comme membres internes d'une classe ou d'une structure. |
| Constante / Read only | -         | VARIABLE_FOUR        | Variables contenant des valeurs constantes qui ne changent pas pendant l'exécution. |
| Enuméré   | enum_     | enum_VariableFive    | Variables associées à des énumérations, souvent utilisées pour des options prédéfinies. |
| Courante  | curr_     | curr_VariableSix     | Variables utilisées dans le contexte actuel, généralement pour améliorer la lisibilité. |
| Texture 2D| _Tex2D    | VariableSeven_Tex2D  | Variables représentant des textures 2D dans les environnements graphiques. |
| Position  | _Pos      | VariableSeven_Pos    | Variables décrivant des positions, souvent dans des contextes spatiaux. |

## Description du Projet
Ce projet a pour but de recréer le jeu [Rogue (1980)](#inspiration-rogue-1980) en C# à l'aide des framework [MonoGame](https://www.monogame.net/) et [.NET](https://dotnet.microsoft.com/en-us/). 
Certaines mécaniques diffère du jeu de base, comme la gestion dynamique de la lumière dans les pièces qui n'est pas pris qui n'est pas présent dans le jeu et la magie qui a été retiré.

## Fonctionnement de la lumière
Pour définir la luminosité d'une case, celle-ci est calculé selon la puissance d'action de l'éméteur qu'elle reçoit, sa distance et la valeur maximal et minimal qu'elle peut avoir.

Exemple :

```
LUMINOSITE_MAX = "#00000"; //
LUMINOSITE_MINI = "#FFFFF"; // 

PuissanceEmeteur = 8;
DistanceCaseEmetteur = 3;

LuminositeCase = PuissanceEmeteur / Math.Pow(DistanceCaseEmetteur,2) * (LUMINOSITE_MAX - LUMINOSITE_MINI)

```
![Exemple de dégradation de la lumière théorique](https://github.com/LeBonVieuBelouga/ESIG-PROG-2023/blob/main/res/img/ExempleLumiere.png)

Emeteur_Puissance = 8;
Distance 
## Inspiration : Rogue (1980)
<a name="inspiration-rogue-1980"></a>

![Rogue Game](https://github.com/LeBonVieuBelouga/ESIG-PROG-2023/blob/main/res/img/rogue_screenshot.jpg)

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

### Environement de Developpement 
Nous avons développé le projet sur Visual Studio avec l'extension  Monogame
- lien vers la procédure d'installation de l'extension : [https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html](https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html)
- 

## Source 
<a name="source"></a>

- Procedural Landmass Generation (E01: Introduction) : [https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3](https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3)
- Roguelike UML exemple : [https://app.genmymodel.com/api/repository/thrognuk/Roguelike](https://app.genmymodel.com/api/repository/thrognuk/Roguelike)
- Decoded Rogue : [https://www.maizure.org/projects/decoded-rogue/index.html](https://www.maizure.org/projects/decoded-rogue/index.html)
- SadConsole :  [https://github.com/Thraka/SadConsole/tree/master](https://github.com/Thraka/SadConsole/tree/master)
- Amongus Jumper : [https://github.com/divtec-cejef/2021-JCO-Platformer-31-ProgramationOO](https://github.com/divtec-cejef/2021-JCO-Platformer-31-ProgramationOO)