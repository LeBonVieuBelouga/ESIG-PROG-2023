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
    /// Classe représentant une Entity (entité) qui peut s'apparenter à un être vivant
    /// Cette classe est abstraite donc impossible de créer un objet de ce type
    /// Cette classe hérite de la classe Sprite
    /// </summary>
    internal abstract class Entity : Sprite
    {
        protected const uint HEALTH_DEFAULT = 1;
        protected const uint DAMAGE_DEFAULT = 0;
        protected const uint DEFENSE_DEFAULT = 0;
        protected const float DEFAULT_ENTITY_VELOCITY = 1f;

        private uint m_HealthPoint;
        private uint m_Damage;
        private uint m_Defense;
        private bool m_IsDead = false;

        /// <summary>
        /// Créer un objet de type Entity et instencie toutes ces propriétés.
        /// Permet de généré des créatures,joueur etc.. pouvant bouger, attaquer, mourrir, etc..
        /// </summary>
        /// <param name="_VisibilityLevel">Niveau de lumière du Ground</param>
        /// <param name="_Content">Contenu du Ground (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur le Ground</param>
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
        public Entity(
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
            ) : base (_Texture2D, _SpriteBatch, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth) {

            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);

        }
        /// <summary>
        /// Setter pour m_HealthPoint
        /// </summary>
        /// <param name="_HealthPoint"></param>
        public void SetHealthPoint(uint _HealthPoint = HEALTH_DEFAULT) {
            if (_HealthPoint == HEALTH_DEFAULT)
            {
                Debug.WriteLine("Les points de vie sont passé à zero");
                this.m_IsDead = true;
                this.Death();
            }
            else {
                this.m_HealthPoint = _HealthPoint;
            }
        }
        /// <summary>
        /// Getter pour m_HealthPoint
        /// </summary>
        /// <returns>this.m_HealthPoint</returns>
        public uint  GetHealthPoint()
        {
                return this.m_HealthPoint;
        }

        /// <summary>
        /// Setter pour m_Damage 
        /// </summary>
        /// <param name="_Damage"></param>
        public void SetDamage(uint _Damage = DAMAGE_DEFAULT) {
          
                this.m_Damage = _Damage;
        }

        /// <summary>
        /// Getter pour m_Damage
        /// </summary>
        /// <returns></returns>
        public uint GetDamage()
        {
            return this.m_Damage;
        }

        /// <summary>
        /// Setter pour m_Defense
        /// </summary>
        /// <param name="_Defense"></param>
        public void SetDefense(uint _Defense = DEFENSE_DEFAULT) { 
        
            this.m_Defense = _Defense;

        }


        /// <summary>
        /// Getter pour m_Defense
        /// </summary>
        /// <returns></returns>
        public uint GetDefense()
        {
            return this.m_Defense;

        }

        /// <summary>
        /// Permet de tuer l'Entity.
        /// </summary>
        public abstract void Death();

        /// <summary>
        /// Permet à l'Entity de bouger.
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// Permet à l'Entity d'attaquer.
        /// </summary>
        public abstract void Attack();
    }
}
