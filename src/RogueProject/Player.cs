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
        
        private bool m_ReleaseUpKey = false;
        private bool m_ReleaseDownKey = false;
        private bool m_ReleaseRightKey = false;
        private bool m_ReleaseLeftKey = false;

        private Vector2 m_PlayerIndex = new Vector2(0, 0);

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
            Vector2 _PlayerIndex,
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

        public void SetIndexPlayer(Vector2 _indexPlayer, List<Case> _GridOfCase)
        {
            this.m_PlayerIndex = _indexPlayer;

        }

        /// <summary>
        /// Permet au joueur de bouger
        /// A réecrire une fois fonction fini
        /// </summary>
        public void Move(DIRECTION _Direction, Case[][] _GridOfCase)
        {
            switch (_Direction)
            {
                case DIRECTION.LEFT:
                    if (this.m_Pos.X - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().X)
                    {
                        this.m_Pos.X -= _GridOfCase[0][0].GetTexture().Width;
                    }
                    break;
                case DIRECTION.RIGHT:
                    if (this.m_Pos.X + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().X)
                    {
                        this.m_Pos.X += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                    }
                    break;
                case DIRECTION.UP:

                    if (this.m_Pos.Y - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().Y)
                    {
                        this.m_Pos.Y -= _GridOfCase[0][0].GetTexture().Width;
                    }
                    break;
                case DIRECTION.DOWN:

                    if (this.m_Pos.Y + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().Y)
                    {
                        this.m_Pos.Y += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
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
            Case[][] _GridOfCase
            )
        {


            if (m_ReleaseUpKey && m_ReleaseDownKey && m_ReleaseLeftKey && m_ReleaseRightKey)
            {
                if (_Kstate.IsKeyDown(Keys.Up))
                {
                    this.Move(DIRECTION.UP, _GridOfCase);

                    m_ReleaseUpKey = false;
                }
                if (_Kstate.IsKeyDown(Keys.Down))
                {
                    this.Move(DIRECTION.DOWN, _GridOfCase);
                    m_ReleaseDownKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Left))
                {
                    this.Move(DIRECTION.LEFT, _GridOfCase);
                    m_ReleaseLeftKey = false;
                }

                if (_Kstate.IsKeyDown(Keys.Right))
                {
                    this.Move(DIRECTION.RIGHT, _GridOfCase);
                    m_ReleaseRightKey = false;
                }
            }
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


            return true;
        }

        public override void Attack(Entity _entity)
        {
            
        }


    }
}
