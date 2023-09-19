using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Formats.Asn1;

namespace RogueProject
{
    /// <summary>
    /// Créer une class permettant au GameCore d'implémenté des ennemis dans le jeu.
    /// </summary>
    internal class Enemy : Entity
    {

        private uint EXPERIENCE_POINT_DEFAULT = 15;
        private uint ACTION_POINT_DEFAULT = 1;

        private uint m_ExperienceGiven;
        private uint m_actionPoint;

        /// <summary>
        /// Créer un objet de type Entity et instencie toutes ces propriétés.
        /// Permet de généré des créatures,joueur etc.. pouvant bouger, attaquer, mourrir, etc..
        /// </summary>
        /// <param name="_Texture2D">Texture de le Ground</param>
        /// <param name="_SpriteBatch">SpriteBatch du GameCore permettant de dessiner la Texture sur la fenêtre</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Ground</param>
        /// <param name="_Velocity">vitesse du Ground</param>
        /// <param name="_SourceRectangle">Taille du Ground</param>
        /// <param name="_Color">Couleur du Ground</param>
        /// <param name="_Rotation">Orientation du Ground</param>
        /// <param name="_Origin">Position d'origin du Ground</param>
        /// <param name="_Scale">Mise à l'échelle du Ground</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Ground/param>
        public Enemy(
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
            ) : base(_Texture2D, _SpriteBatch, _HealthPoint, _Damage, _Defense, _Position, _Velocity, 
                _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {

            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);
        }

        /// <summary>
        /// Permet à l'Ennemie de mourrir.
        /// </summary>
        public override void Death() {
            Debug.WriteLine("Morbius");
        }

        /// <summary>
        /// Permet à l'Ennemie d'attaquer.
        /// </summary>
        public override void Attack(Entity _entity)
        {
            uint entityHeathPoint = _entity.GetHealthPoint();
            uint entityDamage = _entity.GetDamage();
            uint entityDefense = _entity.GetDefense();

            uint curr_damage = this.m_Damage - entityDefense;

            entityHeathPoint -= curr_damage;

            _entity.SetHealthPoint(entityHeathPoint);
        }

        /// <summary>
        /// Permet à l'Ennemie de bouger.
        /// </summary>
        public override void Move()
        {
            throw new NotImplementedException();
        }


        public void Update(GameTime gameTime) {

            Move();
        }
    }
}

