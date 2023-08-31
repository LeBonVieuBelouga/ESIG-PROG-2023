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
        private Texture2D m_Sprite_Tex2D;
        private Vector2 m_Sprite_Pos;
        private Vector2 m_Sprite_Size;
        private Color m_Color;

        private SpriteBatch m_SpriteBatch;

        private Rectangle m_SpriteSheet_Size;


        float m_SpriteVelocity;
        bool m_IsSpriteSheet;

        public Sprite(Texture2D _Sprite_Tex2D, Vector2 _Sprite_Pos, float SpriteVelocity, Vector2 _Sprite_Size, bool m_IsSpriteSheet = false) {
            
            this.SetTexture(_Sprite_Tex2D);
            this.SetPosition(_Sprite_Pos);
            this.SetSize(_Sprite_Size);

        }

        public void SetTexture(Texture2D _Sprite_Tex2D) {
            m_Sprite_Tex2D = _Sprite_Tex2D;

        }

        public void SetPosition(Vector2 _Sprite_Pos)
        {
            m_Sprite_Pos = _Sprite_Pos;
        }

        public void SetSize(Vector2 _Sprite_Size)
        {
            m_Sprite_Size = _Sprite_Size;
        }

        public void Draw(SpriteBatch _SpriteBatch) {

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
    }
}





