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
        private SpriteBatch m_SpriteBatch; // Helper class pour dessiner le sprite dans la fenêtre.
        private Rectangle m_SpriteSheet_Size;

        private Texture2D m_Sprite_Tex2D;// Texture du Sprite
        public  Vector2 m_Sprite_Pos;// Position du Sprite dans l'environement
        private Rectangle? m_SourceRectangle; // Taille du Sprite ??
        //private Vector2 m_Sprite_Size; // Taille du Sprite 
        private Color m_Color; // Filtre appliqué sur le sprite
        private float m_Rotation; // Angle de rotation a appliquer au sprite
        private Vector2 m_Origin; //position d'orgine
        private Vector2 m_Scale;
        private SpriteEffects m_Effects;
        private float m_LayerDepth;

        
   
        //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth


        // Position de l'objet graphique (this.m_Sprite_Pos).
        // Aucun découpage de la texture (null).
        // Couleur de dessin (Color.White).
        // Rotation de l'objet (0 degrés).
        // Origine de rotation au centre de la texture.
        // Agrandissement (Vector2.One pour une taille inchangée).
        // Effets de sprite (SpriteEffects.None pour aucun effet).
        //



        float m_SpriteVelocity;
        bool m_IsSpriteSheet;

        public Sprite(
            Texture2D _Sprite_Tex2D, 
            SpriteBatch _SpriteBatch, 
            Vector2 _Sprite_Pos = new Vector2(),
            Vector2 _SpriteVelocity = new Vector2(),
            Rectangle? m_SourceRectangle = null,
            /*Color _Color = Color.White,*/
            float _Rotation = 0f, 
            Vector2 _Origin = new Vector2(),
            Vector2 m_Scale = new Vector2(),
            SpriteEffects _Effects = SpriteEffects.None,
            float m_LayerDepth = 0f) {
            
            this.SetTexture(_Sprite_Tex2D);
            this.SetPosition(_Sprite_Pos);
            this.SetSourceRectangle();
            //this.SetSize(_Sprite_Size);
            this.SetSpriteBatch(_SpriteBatch);
            


        }

        public void SetTexture(Texture2D _Sprite_Tex2D) {
            m_Sprite_Tex2D = _Sprite_Tex2D;

        }

        public void SetPosition(Vector2 _Sprite_Pos)
        {
            m_Sprite_Pos = _Sprite_Pos;
        }

        /*
        public void SetSize(Vector2 _Sprite_Size)
        {
            this.m_Sprite_Size = _Sprite_Size;
        }
        */

        public void SetSpriteBatch(SpriteBatch _SpriteBatch) { 
            m_SpriteBatch = _SpriteBatch;
        }


        // Setter pour m_SourceRectangle
        public void SetSourceRectangle(Rectangle? sourceRectangle)
        {
            m_SourceRectangle = sourceRectangle;
        }

        // Setter pour m_Color
        public void SetColor(Color _Color)
        {
            m_Color = _Color;
        }

        // Setter pour m_Rotation
        public void SetRotation(float _Rotation)
        {
            m_Rotation = _Rotation;
        }

        // Setter pour m_Origin
        public void SetOrigin(Vector2 _Origin)
        {
            m_Origin = _Origin;
        }

        // Setter pour m_Scale
        public void SetSpriteScale(Vector2 _Scale)
        {
            m_Scale = _Scale;
        }

        // Setter pour m_Effects
        public void SetEffects(SpriteEffects _Effects)
        {
            m_Effects = _Effects;
        }

        // Setter pour m_LayerDepth
        public void SetLayerDepth(float _layerDepth)
        {
            m_LayerDepth = _layerDepth;
        }

        /// <summary>
        /// Permet de dessiner un Sprite en un appel de fonction avec ou sans un SpriteBatch spécifié.
        /// </summary>
        /// <param name="_SpriteBatch"></param>
        public void Draw(SpriteBatch _SpriteBatch) {

            //Dessine le Sprite avec ces paramètres 
            _SpriteBatch.Draw(
                    this.m_Sprite_Tex2D,
                    this.m_Sprite_Pos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(this.m_Sprite_Tex2D.Width / 2, this.m_Sprite_Tex2D.Height / 2),
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
                    this.m_Sprite_Tex2D,
                    this.m_Sprite_Pos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(this.m_Sprite_Tex2D.Width / 2, this.m_Sprite_Tex2D.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
        }
    }
}





