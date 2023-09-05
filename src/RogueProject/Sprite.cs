using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueProject
{
    public class Sprite
    {
        const float DEFAULT_VELOCITY = 1f;

        private SpriteBatch m_SpriteBatch; // Helper class pour dessiner le sprite dans la fenêtre.
        private Rectangle m_SpriteSheet_Size;

        private Texture2D m_Tex2D;// Texture du Sprite
        private  Vector2 m_Pos;// Position du Sprite dans l'environement
        float m_Velocity; // Vitesse de déplacement du Sprite

        private Rectangle? m_SourceRectangle; // Taille du Sprite ??
        //private Vector2 m_Size; // Taille du Sprite 
        private Color m_Color; // Filtre appliqué sur le sprite
        private float m_Rotation; // Angle de rotation a appliquer au sprite
        private Vector2 m_Origin; //position d'orgine
        private Vector2 m_Scale;
        private SpriteEffects m_Effect;
        private float m_LayerDepth;

        //private int m_Sprite_Index { get; set; } // <-- Créer des setters et des getters. 

        
   
        //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth


        // Position de l'objet graphique (this.m_Sprite_Pos).
        // Aucun découpage de la texture (null).
        // Couleur de dessin (Color.White).
        // Rotation de l'objet (0 degrés).
        // Origine de rotation au centre de la texture.
        // Agrandissement (Vector2.One pour une taille inchangée).
        // Effets de sprite (SpriteEffects.None pour aucun effet).
        //

        bool m_IsSpriteSheet;

        public Sprite(
            Texture2D _Texture2D,
            SpriteBatch _SpriteBatch,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_VELOCITY,
            Rectangle? m_SourceRectangle = null,
            /*Color _Color = Color.White,*/
            float _Rotation = 0f, 
            Vector2 _Origin = new Vector2(),
            Vector2 _Scale = new Vector2(),
            SpriteEffects _Effect = SpriteEffects.None,
            float _LayerDepth = 0f) {
            
            this.SetTexture(_Texture2D);
            this.SetPosition(_Position);
            this.SetVelocity(_Velocity);
            this.SetSourceRectangle(m_SourceRectangle);
            this.SetRotation(_Rotation);
            this.SetOrigin(_Origin);
            this.SetScale(_Scale);
            this.SetEffect(_Effect);
            this.SetLayerDepth(_LayerDepth);
            
            this.SetSpriteBatch(_SpriteBatch);

        }//this.SetSize(_Sprite_Size);

        public void SetTexture(Texture2D _Sprite_Tex2D) {
            m_Tex2D = _Sprite_Tex2D;

        }
        public Texture2D GetTexture()
        {
            return m_Tex2D;
        }

        public void SetPosition(Vector2 _Sprite_Pos)
        {
            this.m_Pos = _Sprite_Pos;
        }
        public Vector2 GetPosition()
        {
            return m_Pos;
        }

        public void SetVelocity(float _Velocity)
        {
            this.m_Velocity = _Velocity;
        }

        public float GetVelocity()
        {
            return m_Velocity;
        }

        /*
        public void SetSize(Vector2 _Sprite_Size)
        {
            this.m_Sprite_Size = _Sprite_Size;
        }
        */

        /// <summary>
        /// Setter pour m_SpriteBatch
        /// </summary>
        /// <param name="_SpriteBatch"></param>
        public void SetSpriteBatch(SpriteBatch _SpriteBatch) { 
            m_SpriteBatch = _SpriteBatch;
        }
        /// <summary>
        /// Getter pour m_SpriteBatch
        /// </summary>
        /// <returns>m_SpriteBatch</returns>
        public SpriteBatch GetSpriteBatch()
        {
            return m_SpriteBatch;
        }

        /// <summary>
        /// Setter pour m_SourceRectangle
        /// </summary>
        /// <param name="sourceRectangle"></param>
        public void SetSourceRectangle(Rectangle? sourceRectangle)
        {
            m_SourceRectangle = sourceRectangle;
        }
        /// <summary>
        /// Getter pour m_SourceRectangle
        /// </summary>
        /// <returns>m_SourceRectangle</returns>
        public Rectangle? GetSourceRectangle()
        {
            return m_SourceRectangle;
        }



        /// <summary>
        /// Setter pour m_Color
        /// </summary>
        /// <param name="_Color"></param>
        public void SetColor(Color _Color)
        {
            this.m_Color = _Color; 
        }
        /// <summary>
        /// Getter pour m_Color
        /// </summary>
        /// <returns>m_Color</returns>
        public Color GetColor() {
            return this.m_Color;
        }

        /// <summary>
        /// Setter pour m_Rotation
        /// </summary>
        /// <param name="_Rotation"></param>
        public void SetRotation(float _Rotation)
        {
            this.m_Rotation = _Rotation;
        }
        /// <summary>
        /// Getter pour m_Rotation
        /// </summary>
        /// <returns>m_Rotation</returns>
        public float GetRotation()
        {
            return this.m_Rotation;
        }

        /// <summary>
        /// Setter pour m_Origin
        /// </summary>
        /// <param name="_Origin"></param>
        public void SetOrigin(Vector2 _Origin)
        {
            this.m_Origin = _Origin;
        }

        /// <summary>
        /// Getter pour m_Origin
        /// </summary>
        /// <returns>m_Origin</returns>
        public Vector2 GetOrigin()
        {
            return this.m_Origin;
        }

        /// <summary>
        /// Setter pour m_Scale
        /// </summary>
        /// <param name="_Scale"></param>
        public void SetScale(Vector2 _Scale)
        {
            this.m_Scale = _Scale;
        }

        /// <summary>
        /// Getter pour m_Scale
        /// </summary>
        /// <returns>m_Scale</returns>
        public Vector2 GetScale()
        {
            return this.m_Scale;
        }

        /// <summary>
        /// Setter pour m_Effect
        /// </summary>
        /// <param name="_Effect"></param>
        public void SetEffect(SpriteEffects _Effect)
        {
            this.m_Effect = _Effect;
        }
        /// <summary>
        /// Getter pour m_Effect
        /// </summary>
        /// <returns>m_Effect</returns>
        public SpriteEffects GetEffect()
        {
            return m_Effect;
        }

        /// <summary>
        /// Setter pour m_layerDepth
        /// </summary>
        /// <param name="_layerDepth"></param>
        public void SetLayerDepth(float _layerDepth)
        {
            this.m_LayerDepth = _layerDepth;
        }

        /// <summary>
        /// Getter pour m_LayerDepth
        /// </summary>
        /// <returns>m_LayerDepth</returns>
        public float GetLayerDepth()
        {
            return m_LayerDepth;
        }



        /// <summary>
        /// Permet de dessiner un Sprite en un appel de fonction avec ou sans un SpriteBatch spécifié.
        /// </summary>
        /// <param name="_SpriteBatch"></param>
        public void Draw(SpriteBatch _SpriteBatch) {

            //Dessine le Sprite avec ces paramètres 
            _SpriteBatch.Draw(
                    this.m_Tex2D,
                    this.m_Pos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(this.m_Tex2D.Width / 2, this.m_Tex2D.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
        }

        /// <summary>
        /// Permet de dessiner un Sprite en un appel de fonction avec ou sans un SpriteBatch spécifié.
        /// </summary>
        /// <param name="_SpriteBatch"></param>
        public void Draw()
        {

            //Dessine le Sprite avec ces paramètres 
           this.m_SpriteBatch.Draw(
                    this.m_Tex2D,
                    this.m_Pos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(this.m_Tex2D.Width / 2, this.m_Tex2D.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
        }
    }
}





