using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;


namespace RogueProject
{
    /// <summary>
    /// Classe représentant un Player (joueur)
    /// Cette classe représente le joueur, il peut faire une multitude d'action tel que bouger, attaquer, ouvrir l'inventaire,...
    /// Cette classe hérite de la classe Entity
    /// </summary>
    internal class Player : Entity
    {

        private bool releaseUpKey = false;
        private bool releaseDownKey = false;
        private bool releaseRightKey = false;
        private bool releaseLeftKey = false;

        /// <summary>
        /// Créer un objet de type Player et instencie toutes ces propriétés.
        /// Permet de généré des créatures,joueur etc.. pouvant bouger, attaquer, mourrir, etc..
        /// </summary>
        /// <param name="_VisibilityLevel">Niveau de lumière du Player</param>
        /// <param name="_Content">Contenu du Player (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur le Player</param>
        /// <param name="_Texture2D">Texture de le Player</param>
        /// <param name="_SpriteBatch">SpriteBatch du GameCore permettant de dessiner la Texture sur la fenêtre</param>
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
            Texture2D _Texture2D,
            SpriteBatch _SpriteBatch,
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
            ) : base(_Texture2D, _SpriteBatch, _HealthPoint, _Damage, _Defense, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);
        }

        /// <summary>
        /// Permet au joueur de bouger
        /// A réecrire une fois fonction fini
        /// </summary>
        public void Move(DIRECTION _Direction, List<Case> _ListCase, GameTime _GameTime, KeyboardState _Kstate)
        {
            switch (_Direction)
            {
                case DIRECTION.LEFT:
                    if (this.m_Pos.X - _ListCase[0].GetTexture().Width >= _ListCase[0].GetPosition().X)
                    {
                        this.m_Pos.X -= _ListCase[0].GetTexture().Width;
                    }
                    break;
                case DIRECTION.RIGHT:
                    if (this.m_Pos.X + _ListCase[_ListCase.Count - 1].GetTexture().Width <= _ListCase[_ListCase.Count - 1].GetPosition().X)
                    {
                        this.m_Pos.X += _ListCase[_ListCase.Count - 1].GetTexture().Width;
                    }
                    break;
                case DIRECTION.UP:

                    if (this.m_Pos.Y - _ListCase[0].GetTexture().Width >= _ListCase[0].GetPosition().Y)
                    {
                        this.m_Pos.Y -= _ListCase[0].GetTexture().Width;
                    }
                    break;
                case DIRECTION.DOWN:

                    if (this.m_Pos.Y + _ListCase[_ListCase.Count - 1].GetTexture().Width <= _ListCase[_ListCase.Count - 1].GetPosition().Y)
                    {
                        this.m_Pos.Y += _ListCase[_ListCase.Count - 1].GetTexture().Width;
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


        public bool Update(
            GameTime _GameTime,
            KeyboardState _Kstate,
            List<Case> _ListCase
            )
        {


            if (releaseUpKey && releaseDownKey && releaseLeftKey && releaseRightKey)
            {
                if (_Kstate.IsKeyDown(Keys.Up))
                {
                    this.Move(DIRECTION.UP, _ListCase, _GameTime, _Kstate);

                    releaseUpKey = false;
                }
                if (_Kstate.IsKeyDown(Keys.Down))
                {
                    this.Move(DIRECTION.DOWN, _ListCase, _GameTime, _Kstate);
                    releaseDownKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Left))
                {
                    this.Move(DIRECTION.LEFT, _ListCase, _GameTime, _Kstate);
                    releaseLeftKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Right))
                {
                    this.Move(DIRECTION.RIGHT, _ListCase, _GameTime, _Kstate);
                    releaseRightKey = false;
                }
            }
            if (_Kstate.IsKeyUp(Keys.Up) && !releaseUpKey)
            {
                releaseUpKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Down) && !releaseDownKey)
            {
                releaseDownKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Right) && !releaseRightKey)
            {
                releaseRightKey = true;
            }
            if (_Kstate.IsKeyUp(Keys.Left) && !releaseLeftKey)
            {
                releaseLeftKey = true;
            }


            return true;
        }
        ///
        // FAIRE CLASSE UPDATE QUI RENVOIE VRAI SI LES MONSTRES PEUVENT JOUER 

    }
}
