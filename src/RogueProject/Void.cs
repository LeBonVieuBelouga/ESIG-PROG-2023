using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RogueProject
{
    class Void : Case
    {
        const float DEFAULT_VOID_VELOCITY = 0;

        /// <summary>
        /// Créer un objet de type Void et instencie toutes ces propriétés 
        /// </summary>
        /// <param name="_VisibilityLevel">Niveau de lumière du Void</param>
        /// <param name="_Content">Contenu du Void (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur le Void</param>
        /// <param name="_Texture2D">Texture de le Void</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Void</param>
        /// <param name="_Velocity">vitesse du Void</param>
        /// <param name="_SourceRectangle">Taille du Void</param>
        /// <param name="_Color">Couleur du Void</param>
        /// <param name="_Rotation">Orientation du Void</param>
        /// <param name="_Origin">Position d'origin du Void</param>
        /// <param name="_Scale">Mise à l'échelle du Void</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Void/param>
        public Void(
            int _VisibilityLevel,
            Sprite _Content,
            bool _IsWalkable,
            Texture2D _Texture2D,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_VOID_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            float _Scale = DEFAULT_SCALE,
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_VisibilityLevel, _Content, _IsWalkable, _Texture2D, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            // Code lors de la création d'une case vide (Void)
        }
    }
}
