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
    internal class Enemy : Entity
    {
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
            ) : base(_Texture2D, _SpriteBatch, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {

            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            //this.SetDefense(_Defense);
        }

        public override void Death() {
            Debug.WriteLine("Morbius");
        }
    }
}
}
