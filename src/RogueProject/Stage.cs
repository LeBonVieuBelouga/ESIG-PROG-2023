using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueProject
{
    class Stage
    {

        List<Room> m_RoomList;
        List<Vector2> m_FreeSpace;

        public Stage() 
        { 
        
        }
        
        public void GenerateStage()
        {
            // Crée une liste de case disponible, supprimé les case déjà utilisé par une room
        }

        public void GenerateRoom()
        {

        }

        public void Draw(SpriteBatch _SpriteBatch)
        {

        }
    }
}
