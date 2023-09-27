using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace RogueProject
{
    class Stage
    {

        List<Room> m_ListRoom = new List<Room>();
        List<Vector2> m_ListFreeSpace = new List<Vector2>();


        Case[][] m_GridOfCase;
        int m_GridCol;
        int m_GridRow;

        Texture2D m_TextureRoomCorner;
        Texture2D m_TextureRoomStraight;
        Texture2D m_TextureRoomGround;
        Texture2D m_TextureVoid;
        int m_NumberOfRoom;

        public Stage(int _GridCol, int _GridRow, int _NumberOfRoom, Texture2D _TextureRoomCorner, Texture2D _TextureRoomStraight, Texture2D _TextureGround, Texture2D _TextureVoid, GraphicsDeviceManager _graphics) 
        {
            this.SetGridCol(_GridCol);
            this.SetGridRow(_GridRow);
            this.SetNumberOfRoom(_NumberOfRoom);
            this.SetTextureCorner(_TextureRoomCorner);
            this.SetTextureStraight(_TextureRoomStraight);
            this.SetTextureGround(_TextureGround);
            this.SetTextureVoid(_TextureVoid);

            ResetStage(_graphics);
            GenerateStage();
        }
        
        public void GenerateStage()
        {
            // Crée une liste de case disponible, supprimé les case déjà utilisé par une room

            for (int i = 0; i <= this.m_GridOfCase.Length - 1; i++)
            {
                for (int j = 0; j <= this.m_GridOfCase[i].Length - 1; j++)
                {
                    m_ListFreeSpace.Add(new Vector2(i, j));
                }
            }



            do
            {
                this.GenerateRoom();
            } while (m_ListRoom.Count != m_NumberOfRoom);

            for (int i = 0;i < m_ListRoom.Count;i++)
            {
                // utiliser la fonction DrawRoom pour chaque room de la liste
                // Ne pas oublier de changer et retirer les cases valide pour les rooms et uniquement utiliser celle libre
            }

            //m_ListRoom.Add(new Room(
            //    m_ListFreeSpace[rand.Next(m_ListFreeSpace.Count)],
            //    3,
            //    3,
            //    ROOM_TYPE.EMPTY
            //));
        }

        public void GenerateRoom()
        {
            bool roomIsCreated = false;

            do
            {
                Random rand = new Random();

                Vector2 initialIndex = this.m_ListFreeSpace[rand.Next(m_ListFreeSpace.Count)];
                int sizeX = rand.Next(4, 10);
                int sizeY = rand.Next(2, 10);
                ROOM_TYPE roomType = ROOM_TYPE.EMPTY;

                bool isFree = true;
                if (initialIndex.X + sizeX > m_GridCol)
                {
                    initialIndex.X = initialIndex.X - Math.Abs(initialIndex.X - sizeX);
                }

                if (initialIndex.Y + sizeY > m_GridRow)
                {
                    initialIndex.Y = initialIndex.Y - Math.Abs(initialIndex.Y - sizeY);
                }

                for (int i = (int)initialIndex.X; i <= initialIndex.X + sizeX; i++)
                {
                    for (int j = (int)initialIndex.Y; j <= initialIndex.Y + sizeY; j++)
                    {
                        // Faire la classe void, les salles peuvent se poser uniquement sur des cases void, si c'est autre chose (ground ou mur) alors la salle est invalide
                        if (this.m_GridOfCase[i][j].GetType().Name != "Void")
                        {
                            isFree = false;
                        }
                    }
                }

                if (isFree)
                {
                    m_ListRoom.Add(new Room(
                        initialIndex,
                        sizeX,
                        sizeY,
                        ROOM_TYPE.EMPTY
                    ));
                    roomIsCreated = true;
                }

            } while (!roomIsCreated);


        }

        public void ResetStage(GraphicsDeviceManager _graphics)
        {
            m_GridOfCase = new Case[m_GridCol][];
            
            int GridSizeWidth = m_GridCol * this.m_TextureRoomGround.Width;
            int GridSizeHeight = this.m_GridRow * this.m_TextureRoomGround.Height;

            int startX = (_graphics.PreferredBackBufferWidth - GridSizeWidth) / 2;
            int startY = (_graphics.PreferredBackBufferHeight - GridSizeHeight) / 2;

            //Parcourt les colonnes du tableau2D
            for (int i = 0; i <= m_GridCol - 1; i++)
            {
                //Définit la hauteur maximal du tableau2D      
                m_GridOfCase[i] = new Case[this.m_GridRow];

                //Parcourt les lignes du tableau2D
                for (int j = 0; j <= this.m_GridRow - 1; j++)
                {
                    m_GridOfCase[i][j] = new Void(
                            1,
                            null,
                            true,
                            this.m_TextureVoid,
                            new Vector2(startX + this.m_TextureRoomGround.Width * i, startY + this.m_TextureRoomGround.Height * j)
                        );
                    m_GridOfCase[i][j].DefaultValue();
                    Color color = new Color(255, 0, 255);
                    m_GridOfCase[i][j].SetColor(color);
                }
            }

        }

        public void Draw(SpriteBatch _SpriteBatch)
        {
            for (int i = 0; i <= this.m_GridOfCase.Length - 1; i++)
            {
                for (int j = 0; j <= this.m_GridOfCase[i].Length - 1; j++)
                {
                    this.m_GridOfCase[i][j].Draw(_SpriteBatch);
                }
            }
            //for (int i = 0;i < m_ListRoom.Count;i++)
            //{
            //    this.DrawRoom(_SpriteBatch, m_ListRoom[i], new Vector2(m_ListRoom[i].GetInitialIndex().X, m_ListRoom[i].GetInitialIndex().Y));
            //}
        }

        public void DrawRoom(SpriteBatch _SpriteBatch, Room _RoomToDraw, Vector2 _Index)
        {
            Vector2 roomInitialValue = _RoomToDraw.GetInitialIndex();

            for (int i = (int)roomInitialValue.X; i < roomInitialValue.X + _RoomToDraw.GetSizeX(); i++)
            {
                for (int j = (int)roomInitialValue.Y; j < roomInitialValue.Y + _RoomToDraw.GetSizeY(); j++)
                {
                    //GridOfCase[i][j].SetTexture(m_TextureRoomStraight);

                    if (i == roomInitialValue.X)
                    {

                        if (j == roomInitialValue.Y)
                        {
                            // Coin haut gauche
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomCorner);

                            this.m_GridOfCase[i][j].SetRotation(MathHelper.ToRadians(90));
                            this.m_GridOfCase[i][j].SetIsWalkable(false);


                        }
                        else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                        {
                            // Coin bas gauche
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomCorner);
                            this.m_GridOfCase[i][j].SetIsWalkable(false);
                        }
                        else
                        {
                            // Ligne droite (mur gauche de la pièce)
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomStraight);
                            this.m_GridOfCase[i][j].SetIsWalkable(false);
                        }

                    }
                    else if (i == roomInitialValue.X + _RoomToDraw.GetSizeX() - 1)
                    {
                        if (j == roomInitialValue.Y)
                        {
                            // Coin haut droite
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomCorner);
                            this.m_GridOfCase[i][j].SetRotation(MathHelper.ToRadians(180));
                            this.m_GridOfCase[i][j].SetIsWalkable(false);
                        }
                        else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                        {
                            // Coint bas droit
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomCorner);
                            this.m_GridOfCase[i][j].SetRotation(MathHelper.ToRadians(270));
                            this.m_GridOfCase[i][j].SetIsWalkable(false);
                        }
                        else
                        {
                            // ligne droite (mur droite de la pièce)
                            this.m_GridOfCase[i][j].SetTexture(m_TextureRoomStraight);
                            this.m_GridOfCase[i][j].SetIsWalkable(false);
                        }
                    }
                    else if (j == roomInitialValue.Y)
                    {
                        // Ligne droite (mur haut de la pièce
                        this.m_GridOfCase[i][j].SetTexture(m_TextureRoomStraight);
                        this.m_GridOfCase[i][j].SetRotation(MathHelper.ToRadians(90));
                        this.m_GridOfCase[i][j].SetIsWalkable(false);

                    }
                    else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                    {
                        // Ligne droite (mur bas de la pièce
                        this.m_GridOfCase[i][j].SetTexture(m_TextureRoomStraight);
                        this.m_GridOfCase[i][j].SetRotation(MathHelper.ToRadians(90));
                        this.m_GridOfCase[i][j].SetIsWalkable(false);
                    }

                    if (i == roomInitialValue.X + 1 && j == roomInitialValue.Y)
                    {
                        this.m_GridOfCase[i][j].SetColor(Color.Black);
                        this.m_GridOfCase[i][j].SetIsWalkable(true);
                    }

                    this.m_GridOfCase[i][j].Draw(_SpriteBatch);

                }
            }
        }

        public void SetGridCol(int _GridCol)
        {
            this.m_GridCol = _GridCol;
        }

        public int getGridCol()
        {
            return this.m_GridCol;
        }

        public void SetGridRow(int _GridRaw)
        {
            this.m_GridRow = _GridRaw;
        }

        public int getGridRow()
        {
            return this.m_GridRow;
        }

        public void SetTextureCorner(Texture2D _TextureCorner)
        {
            this.m_TextureRoomCorner = _TextureCorner;
        }

        public Texture2D GetTextureCorner()
        {
            return this.m_TextureRoomCorner;
        }

        public void SetTextureStraight(Texture2D _TextureStraight)
        {
            this.m_TextureRoomStraight = _TextureStraight;
        }

        public Texture2D GetTextureStraight()
        {
            return this.m_TextureRoomStraight;
        }
        public void SetTextureGround(Texture2D _TextureGround)
        {
            this.m_TextureRoomGround = _TextureGround;
        }

        public Texture2D GetTextureGround()
        {
            return this.m_TextureRoomGround;
        }

        public void SetTextureVoid(Texture2D _TextureVoid)
        {
            this.m_TextureVoid = _TextureVoid;
        }

        public Texture2D GetTextureVoid()
        {
            return this.m_TextureVoid;
        }

        public void SetNumberOfRoom(int _NumberOfRoom)
        {
            this.m_NumberOfRoom = _NumberOfRoom;
        }

        public int GetNumberOfRoom()
        {
            return this.m_NumberOfRoom;
        }

        public Case[][] GetGridOfCase()
        {
            return this.m_GridOfCase;
        }


    }
}
