using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueProject
{
    internal class Room
    {
        private int m_SizeX;
        private int m_SizeY;
        private ROOM_TYPE m_RoomType;
        private Vector2 m_InitialIndex;

        public enum CASE_TYPE
        {
            WALL = 0,
            GROUND = 1,
            DOOR = 2, 
            VOID = 3      
            
        }

        /// <summary>
        /// Constructeur d'un object de type Room
        /// </summary>
        /// <param name="_InitialIndex">Position dans la liste de casede la case dans le coin haut gauche de la Room</param>
        /// <param name="_SizeX"></param>
        /// <param name="_SizeY"></param>
        /// <param name="_RoomType"></param>
        public Room(Vector2 _InitialIndex, int _SizeX, int _SizeY, ROOM_TYPE _RoomType) 
        {
            this.SetInitialIndex(_InitialIndex);
            this.SetSizeX(_SizeX);
            this.SetSizeY(_SizeY);
            this.SetRoomType(_RoomType);
        }

        /// <summary>
        /// Change la valeur de 
        /// </summary>
        /// <param name="_SizeX"></param>
        public void SetSizeX(int _SizeX)
        {
            this.m_SizeX = _SizeX;
        }

        /// <summary>
        /// Renvoie la valeur de m_SizeX
        /// </summary>
        /// <returns>un entier étant la valeur de m_SizeX</returns>
        public int GetSizeX()
        {
            return this.m_SizeX;
        }

        /// <summary>
        /// Change la valeur de m_SizeY
        /// </summary>
        /// <param name="_SizeY">Nouvelle valeur de m_SizeY</param>
        public void SetSizeY(int _SizeY)
        {
            this.m_SizeY = _SizeY;
        }

        /// <summary>
        /// Renvoie la valeur de m_SizeX
        /// </summary>
        /// <returns>un entier étant la valeur de m_Size</returns>
        public int GetSizeY()
        {
            return this.m_SizeY;
        }

        /// <summary>
        /// Change la valeur de m_RoomType
        /// </summary>
        /// <param name="_RoomType">nouvelle valeur de m_RoomType</param>
        public void SetRoomType(ROOM_TYPE _RoomType)
        {
            this.m_RoomType = _RoomType;
        }

        /// <summary>
        /// Renvoie la valeur de m_RoomType
        /// </summary>
        /// <returns>un ROOM_TYPE étant la valeur de m_RoomType</returns>
        public ROOM_TYPE GetRoomType()
        {
            return this.m_RoomType;
        }

        /// <summary>
        /// Renvoie la valeur m_InitialIndex
        /// </summary>
        /// <returns>un Vector2 étant la valeur de m_InitialIndex</returns>
        public Vector2 GetInitialIndex()
        {
            return this.m_InitialIndex;
        }

        /// <summary>
        /// Setter pour m_InitialIndex
        /// </summary>
        /// <param name="_InitialIndex">Nouvelle valeur de _InitialIndex</param>
        public void SetInitialIndex(Vector2 _InitialIndex)
        {
            this.m_InitialIndex = _InitialIndex;
        }
        
    }
}
