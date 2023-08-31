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


    }
}





