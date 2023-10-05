using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RogueProject
{
    class Door : Case
    {
        const float DEFAULT_DOOR_VELOCITY = 0;

        private bool m_IsLocked;

        /// <summary>
        /// Créer un objet de type Door et instencie toutes ces propriétés 
        /// </summary>
        /// <param name="_IsLocked">Booléen représentant si la porte est fermé ou non</param>
        /// <param name="_VisibilityLevel">Niveau de lumière du Door</param>
        /// <param name="_Content">Contenu du Door (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur le Door</param>
        /// <param name="_Texture2D">Texture de le Door</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Door</param>
        /// <param name="_Velocity">vitesse du Door</param>
        /// <param name="_SourceRectangle">Taille du Door</param>
        /// <param name="_Color">Couleur du Door</param>
        /// <param name="_Rotation">Orientation du Door</param>
        /// <param name="_Origin">Position d'origin du Door</param>
        /// <param name="_Scale">Mise à l'échelle du Door</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Door/param>
        public Door(
            bool _IsLocked,
            int _VisibilityLevel,
            Sprite _Content,
            bool _IsWalkable,
            Texture2D _Texture2D,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_DOOR_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            float _Scale = DEFAULT_SCALE,
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_VisibilityLevel, _Content, _IsWalkable, _Texture2D, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            // Code lors de la création d'une Door
            this.SetIsLocked(_IsLocked);
        }

        public void SetIsLocked(bool _IsLocked)
        {
            this.m_IsLocked = _IsLocked;
        }
        public bool GetIsLocked()
        {
            return this.m_IsLocked;
        }
    }
}
