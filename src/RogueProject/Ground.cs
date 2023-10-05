using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RogueProject
{
    /// <summary>
    /// Classe réprésentant un Ground (sol) 
    /// </summary>
    class Ground : Case
    {
        const float DEFAULT_GROUND_VELOCITY = 0;

        /// <summary>
        /// Créer un objet de type Ground et instencie toutes ces propriétés 
        /// </summary>
        /// <param name="_VisibilityLevel">Niveau de lumière du Ground</param>
        /// <param name="_Content">Contenu du Ground (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur le Ground</param>
        /// <param name="_Texture2D">Texture de le Ground</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Ground</param>
        /// <param name="_Velocity">vitesse du Ground</param>
        /// <param name="_SourceRectangle">Taille du Ground</param>
        /// <param name="_Color">Couleur du Ground</param>
        /// <param name="_Rotation">Orientation du Ground</param>
        /// <param name="_Origin">Position d'origin du Ground</param>
        /// <param name="_Scale">Mise à l'échelle du Ground</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Ground/param>
        public Ground(
            int _VisibilityLevel,
            Sprite _Content,
            bool _IsWalkable,
            Texture2D _Texture2D,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_GROUND_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            float _Scale = DEFAULT_SCALE,
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_VisibilityLevel, _Content, _IsWalkable, _Texture2D, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            // Code lors de la création d'un ground
        }
    }
}
