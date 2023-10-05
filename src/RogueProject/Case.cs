using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RogueProject
{
    /// <summary>
    /// Classe représentant une case qui peut s'apparenter à une case d'un cadrillage (grid en anglais)
    /// Cette classe est abstraite donc au
    /// Cette classe hérite de la classe Sprite
    /// </summary>
    abstract class Case : Sprite
    {
        // Définition des constantes
        const float DEFAULT_CASE_VELOCITY = 0;

        // Définition des variables membres de la classe Case
        private int m_VisibilityLevel;
        private Sprite m_Content;
        private bool m_IsWalkable;


        /// <summary>
        /// Créer un objet de type case et instencie toutes ces propriétés 
        /// </summary>
        /// <param name="_VisibilityLevel">Niveau de lumière de la case</param>
        /// <param name="_Content">Contenu de la case (joueur, monstre, objet,...)</param>
        /// <param name="_IsWalkable">Définit si c'est possible de marcher sur la case</param>
        /// <param name="_Texture2D">Texture de la case</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) de la case</param>
        /// <param name="_Velocity">vitesse de la case</param>
        /// <param name="_SourceRectangle">Taille de la case</param>
        /// <param name="_Color">Couleur de la case</param>
        /// <param name="_Rotation">Orientation de la case</param>
        /// <param name="_Origin">Position d'origin de la case</param>
        /// <param name="_Scale">Mise à l'échelle de la case</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ de la case</param>
        public Case(
            int _VisibilityLevel,
            Sprite _Content,
            bool _IsWalkable,
            Texture2D _Texture2D,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_CASE_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            float _Scale = DEFAULT_SCALE,
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_Texture2D, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            this.SetIsWalkable(_IsWalkable);
            this.SetVisibilityLevel(_VisibilityLevel);
            this.SetContent(_Content);
        }

        /// <summary>
        /// Setter de m_VisibilityLevel
        /// </summary>
        /// <param name="_VisibilityLevel">Nouvelle valeur de m_VisibilityLevel</param>
        public void SetVisibilityLevel(int _VisibilityLevel)
        {
            this.m_VisibilityLevel = _VisibilityLevel;
        }

        /// <summary>
        /// Getter de m_VisibilityLevel
        /// </summary>
        /// <returns>Retourne un entier représentant la valeur de VisibilityLevel</returns>
        public int GetVisibilityLevel()
        {
            return this.m_VisibilityLevel;
        }

        /// <summary>
        /// Setter de m_Content
        /// </summary>
        /// <param name="_Content">Nouvelle valeur de m_Content</param>
        public void SetContent(Sprite _Content)
        {
            this.m_Content = _Content;
        }

        /// <summary>
        /// Getter de m_Content
        /// </summary>
        /// <returns>Retourne un sprite représentant le sprite contenu dans m_Content</returns>
        public Sprite GetContent()
        {
            return this.m_Content;
        }

        /// <summary>
        /// Setter de m_IsWalkable
        /// </summary>
        /// <param name="_IsWalkable">Nouvelle valeur de m_IsWalkable</param>
        public void SetIsWalkable(bool _IsWalkable)
        {
            this.m_IsWalkable = _IsWalkable;
        }


        /// <summary>
        /// Getter de m_IsWalkable
        /// </summary>
        /// <returns>Retourne un booléen représentant la valeur de m_IsWalkable</returns>
        public bool GetIsWalkable()
        {
            return this.m_IsWalkable;
        }
    }
}
