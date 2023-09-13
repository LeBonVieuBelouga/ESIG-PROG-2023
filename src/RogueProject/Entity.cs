using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace RogueProject
{
    internal class Entity : Sprite
    {
        private const int HEALTH_DEFAULT = 1;
        private const int DAMAGE_DEFAULT = 0;
        private const int DEFENSE_DEFAULT = 0;
        private const float DEFAULT_ENTITY_VELOCITY = 1f;

        private int m_HealthPoint;
        private int m_Damage;
        private int m_Defense;
        private bool m_IsDead = false;

        /// <summary>
        /// Créer un objet de type Ground et instencie toutes ces propriétés 
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
            int _HealthPoint = HEALTH_DEFAULT,
            int _Damage = DAMAGE_DEFAULT,
            int _Defense = DEFENSE_DEFAULT,
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

        public void SetHealthPoint(int _HealthPoint = HEALTH_DEFAULT) {
            if (_HealthPoint <= HEALTH_DEFAULT)
            {
                Debug.WriteLine("LES POINTS DE VIE SONT EN-DESSOUS DE LA VALEUR MINIMAL");

            }
            else {
                this.m_HealthPoint = HEALTH_DEFAULT;
            }
        }

        public void Death() {
            if (m_IsDead = true)
            {

            }
            else { 
            
            }
        
        }
    }
}
