﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace RogueProject
{
    /// <summary>
    /// Classe représentant un Player (joueur)
    /// Cette classe représente le joueur, il peut faire une multitude d'action tel que bouger, attaquer, ouvrir l'inventaire,...
    /// Cette classe hérite de la classe Entity
    /// </summary>
    internal class Player : Entity
    {

        // Variable membre du Joueur
        private bool m_ReleaseUpKey = false;
        private bool m_ReleaseDownKey = false;
        private bool m_ReleaseRightKey = false;
        private bool m_ReleaseLeftKey = false;
        private Vector2 m_EntityIndex = new Vector2(0, 0);

        /// <summary>
        /// Constructeur d'un joueur avec toutes ses informations
        /// </summary>
        /// <param name="_EntityIndex">Index du joueur dans le tableau de case</param>
        /// <param name="_GridOfCase">Tableau de toutes les cases</param>
        /// <param name="_Texture2D">Texture de le Player</param>
        /// <param name="_HealthPoint">Point de vie du joueur</param>
        /// <param name="_Damage">Attaque du joueur</param>
        /// <param name="_Defense">Defens du joueur</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Player</param>
        /// <param name="_Velocity">vitesse du Player</param>
        /// <param name="_SourceRectangle">Taille du Player</param>
        /// <param name="_Color">Couleur du Player</param>
        /// <param name="_Rotation">Orientation du Player</param>
        /// <param name="_Origin">Position d'origin du Player</param>
        /// <param name="_Scale">Mise à l'échelle du Player</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Player/param>
        public Player(
            Vector2 _EntityIndex,
            Case[][] _GridOfCase,
            Texture2D _Texture2D,
            uint _HealthPoint = HEALTH_DEFAULT,
            uint _Damage = DAMAGE_DEFAULT,
            uint _Defense = DEFENSE_DEFAULT,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_ENTITY_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            Vector2 _Scale = new Vector2(),
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_EntityIndex, _GridOfCase,_Texture2D, _HealthPoint, _Damage, _Defense, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);
        }
    
        /// <summary>
        /// Déplace le joueur dans une direction donné
        /// S'occupe aussi de supprimer le joueur du contenu de la case précédente et l'ajoute sur la nouvelle case
        /// </summary>
        /// <param name="_Direction">Direction dans laquelle le joueur va (variable de type DIRECTION qui peut contenir les type : UP, DOWN, LEFT, RIGHT)</param>
        /// <param name="_GridOfCase">Le quadrillage de case</param>
        public void Move(DIRECTION _Direction, Case[][] _GridOfCase)
        {
            // Vérifie de quelle côté le joueur veut se déplacer
            switch (_Direction)
            {
                // Gauche
                case DIRECTION.LEFT:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().X - this.GetTexture().Width)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.X -= _GridOfCase[0][0].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X - 1,this.m_EntityIndex.Y), _GridOfCase);
                    }
                    break;
                // Droite
                case DIRECTION.RIGHT:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().X)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.X += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X + 1, this.m_EntityIndex.Y), _GridOfCase);
                    }
                    break;
                // Haut
                case DIRECTION.UP:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().Y - this.GetTexture().Height)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.Y -= _GridOfCase[0][0].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y - 1), _GridOfCase);
                    }
                    break;
                // Bas
                case DIRECTION.DOWN:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().Y)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.Y += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y + 1), _GridOfCase);
                    }
                    break;
            }
        }

        /// <summary>
        /// Permet au joueur d'attaquer
        /// A réecrire une fois fonction fini
        /// </summary>
        public void Attack()
        {
        }

        /// <summary>
        /// Permet au joueur de mourir
        /// A réecrire une fois fonction fini
        /// </summary>
        public override void Death()
        {

        }

        /// <summary>
        /// Fonction Update du Player, s'éxécute à chaque tick du jeu
        /// Cette fonction s'occupe de toutes les actions qu'un Player peut faire
        /// par exemple : les déplacements, les attaques, les menus,...
        /// </summary>
        /// <param name="_GameTime">Temps entre chaque appel de la fonction Update</param>
        /// <param name="_Kstate">Inputs du clavier</param>
        /// <param name="_GridOfCase">Quadrillage du jeu</param>
        /// <returns>Retourne vrai si le joueur à effectuer une action qui termine son tour</returns>
        public bool Update(
            GameTime _GameTime,
            KeyboardState _Kstate,
            Case[][] _GridOfCase
            )
        {

            bool turnIsOver = false;

            // Si aucune autre touche de mouvement est déjà enfoncé
            if (m_ReleaseUpKey && m_ReleaseDownKey && m_ReleaseLeftKey && m_ReleaseRightKey)
            {
                // Vérifie si une touche de mouvement est appuyé (haut, bas, gauche et droite)
                if (_Kstate.IsKeyDown(Keys.Up))
                {
                    // Avance le joueur vers le haut
                    this.Move(DIRECTION.UP, _GridOfCase);
                    turnIsOver = true;
                    m_ReleaseUpKey = false;
                }
                if (_Kstate.IsKeyDown(Keys.Down))
                {
                    // Avance le joueur vers le bas
                    this.Move(DIRECTION.DOWN, _GridOfCase);
                    turnIsOver = true;
                    m_ReleaseDownKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Left))
                {
                    // Avance le joueur vers le gauche
                    this.Move(DIRECTION.LEFT, _GridOfCase);
                    turnIsOver = true;
                    m_ReleaseLeftKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Right))
                {
                    // Avance le joueur vers le droite
                    this.Move(DIRECTION.RIGHT, _GridOfCase);
                    turnIsOver = true;
                    m_ReleaseRightKey = false;
                }
            }

            // vérifie si les touches de mouvement sont relâché pour permettre de faire un mouvement
            if (_Kstate.IsKeyUp(Keys.Up) && !m_ReleaseUpKey)
            {
                m_ReleaseUpKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Down) && !m_ReleaseDownKey)
            {
                m_ReleaseDownKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Right) && !m_ReleaseRightKey)
            {
                m_ReleaseRightKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Left) && !m_ReleaseLeftKey)
            {
                m_ReleaseLeftKey = true;
            }

            // Retourne vrai si le joueur à fait une action terminant son tour
            return turnIsOver;
        }

        public override void Attack(ref Entity _entity)
        {

        }


    }
}