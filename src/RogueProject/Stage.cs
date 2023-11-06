using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;
using static RogueProject.Room;
using System.Reflection;

namespace RogueProject
{
    /// <summary>
    /// Représente l'étage (un "niveau") que le joueur doit parcourir à la recherche
    /// de récompense, d'objet, de monste à combattre et de la sortie vers le prochaine étage
    /// </summary>
    class Stage
    {
        enum ROOM_SIDE { 
            RIGHT,
            LEFT,
            UP,
            DOWN
        }
        // Constante de Stage
        const int MAX_WIDTH_ROOM = 15;
        const int MIN_WIDTH_ROOM = 6;
        const int MAX_HEIGHT_ROOM = 15;
        const int MIN_HEIGHT_ROOM = 6;
        const int MAX_SIDE_ROOM = 4;

        // Variable membre de Stage
        List<Room> m_ListRoom = new List<Room>();
        List<Vector2> m_ListFreeSpace = new List<Vector2>();

        Case[][] m_GridOfCase;

        int m_GridCol;
        int m_GridRow;
        int m_NumberOfRoom;

        Texture2D m_TextureRoomCorner;
        Texture2D m_TextureRoomStraight;
        Texture2D m_TextureRoomGround;
        Texture2D m_TextureRoomDoor;
        Texture2D m_TextureVoid;

        

        /// <summary>
        /// Constructeur du Stage, instancie les variables et crée le quadrillage, les salles,...
        /// </summary>
        /// <param name="_GridCol">Nombre de colonne du quadrillage</param>
        /// <param name="_GridRow">Nombre de ligne du quadrillage</param>
        /// <param name="_NumberOfRoom">Nombre de Room à générer dans le quadrillage</param>
        /// <param name="_TextureRoomCorner">Texture des coins de Room</param>
        /// <param name="_TextureRoomStraight">Texture des murs de Room</param>
        /// <param name="_TextureGround">Texture des sols des Room</param>
        /// <param name="_TextureVoid">Texture du Void</param>
        /// <param name="_TextureRoomDoor">Texture des portes des Rooms</param>
        /// <param name="_graphics">Permet d'avoir des informations graphique (taille de l'écran,...)</param>
        public Stage(int _GridCol, int _GridRow, int _NumberOfRoom, Texture2D _TextureRoomCorner, Texture2D _TextureRoomStraight, Texture2D _TextureGround, Texture2D _TextureVoid, Texture2D _TextureRoomDoor, GraphicsDeviceManager _graphics) 
        {
            this.SetGridCol(_GridCol);
            this.SetGridRow(_GridRow);
            this.SetNumberOfRoom(_NumberOfRoom);
            this.SetTextureCorner(_TextureRoomCorner);
            this.SetTextureStraight(_TextureRoomStraight);
            this.SetTextureGround(_TextureGround);
            this.SetTextureVoid(_TextureVoid);
            this.SetTextureDoor(_TextureRoomDoor);

            ResetStage(_graphics);
            GenerateStage();
        }

        /// <summary>
        /// Génère l'étage (crée les salles, les chemins,...)
        /// </summary>
        public void GenerateStage()
        {
            // Crée une liste de case disponible, supprimé les case déjà utilisé par une room

            // Crée une liste de Vector2 représentant les emplacement libre que les salles peuvent utilisé
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
        }

        /// <summary>
        /// Fonction qui génère une Room et la place dans le quadrillage
        /// </summary>
        public void GenerateRoom()
        {
            // Booléen qui passe à vrai si la Room est valide
            bool roomIsCreated = false;
            
            // Essaie de créer la Room jusqu'à ce que la configuration soit valide
            do
            {
                // Utilise un random pour avoir des informations aléatoirement représentant les différentes variables de la Room
                Random rand = new Random();

                Vector2 initialIndex = this.m_ListFreeSpace[rand.Next(m_ListFreeSpace.Count)];
                int sizeX = rand.Next(MIN_WIDTH_ROOM, MAX_WIDTH_ROOM);
                int sizeY = rand.Next(MIN_HEIGHT_ROOM, MAX_HEIGHT_ROOM);
                ROOM_TYPE roomType = ROOM_TYPE.EMPTY;

                bool isFree = true;

                // Vérifie si la case dépasse du quadrillage sur l'axe X
                if (initialIndex.X + sizeX > m_GridCol)
                {
                    // Déplace le point de départ de la case pour que la Room de dépasse plus le quadrillage
                    initialIndex.X = initialIndex.X - Math.Abs(m_GridCol - (initialIndex.X + sizeX));
                }

                // Vérifie si la case dépasse du quadrillage sur l'axe Y
                if (initialIndex.Y + sizeY > m_GridRow)
                {
                    // Déplace le point de départ de la case pour que la Room de dépasse plus le quadrillage
                    initialIndex.Y = initialIndex.Y - Math.Abs(m_GridRow - (initialIndex.Y + sizeY));
                }

                // Va vérifier toutes les futurs cases de la Room pour savoir si elle sont vides (qu'il n'y est pas déjà une Room)
                for (int i = (int)initialIndex.X; i < initialIndex.X + sizeX; i++)
                {
                    for (int j = (int)initialIndex.Y; j < initialIndex.Y + sizeY; j++)
                    {
                        // Si la case est d'un autre type que Void, alors la place n'est pas libre
                        if (this.m_GridOfCase[i][j].GetType().Name != "Void")
                        {
                            isFree = false;
                        }
                    }
                }

                // si toutes les cases sonf finalement libre
                if (isFree)
                {
                    // Crée la Room et l'ajoute à la liste de salle du Stage
                    m_ListRoom.Add(new Room(
                        new Vector2(initialIndex.X+1, initialIndex.Y+1),
                        sizeX-2,
                        sizeY-2,
                        ROOM_TYPE.EMPTY
                    ));

                    // Dessine la nouvelle Room dans le quadrillage
                    this.DrawRoom(this.m_ListRoom.Last());
                    roomIsCreated = true;
                }

            } while (!roomIsCreated);
        }

        /// <summary>
        /// Créer le quadrillage, le rempli de case vide (Void)
        /// </summary>
        /// <param name="_graphics">Permet d'obtenir des informations graphique (taille de l'écran, etc...)</param>
        public void ResetStage(GraphicsDeviceManager _graphics)
        {
            m_GridOfCase = null;
            
            m_ListRoom = new List<Room>();

            // Crée les colonnes tableau selon m_GridCol
            m_GridOfCase = new Case[m_GridCol][];
            
            // Selon la taille de l'écran, calcule pour centrer le quadrillage
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
                    // Crée une case vide (Void)
                    m_GridOfCase[i][j] = new Void(
                            1,
                            null,
                            true,
                            this.m_TextureVoid,
                            new Vector2(startX + this.m_TextureRoomGround.Width * i, startY + this.m_TextureRoomGround.Height * j)
                        );
                    // Met un filtre de couleur sur les cases
                    m_GridOfCase[i][j].DefaultValue();
                    Color color = new Color(255, 0, 255);
                    m_GridOfCase[i][j].SetColor(color);
                }
            }
        }

        /// <summary>
        /// Dessine la totalité du quadrillage qui est contenu dans GridOfCase
        /// </summary>
        /// <param name="_SpriteBatch">_SpriteBatch permet de dessiner les sprites à l'écran</param>
        public void Draw(SpriteBatch _SpriteBatch)
        {
            for (int i = 0; i <= this.m_GridOfCase.Length - 1; i++)
            {
                for (int j = 0; j <= this.m_GridOfCase[i].Length - 1; j++)
                {
                    this.m_GridOfCase[i][j].Draw(_SpriteBatch);
                }
            }
        }

        /// <summary>
        /// Converti une case du tableau par un autre type de case (Void, Ground, Wall, Door)
        /// la case qui sera converti est passé en paramètre et le type voulu est une chaîne de caractère du même nom que la classe souhaité
        /// </summary>
        /// <param name="_IndexCase">Vector2 représentant l'index de la case dans GridOfCase à convertir</param>
        /// <param name="_NewType">Chaine de caractère du nouveau type de la case (Ground, Void, Wall, Door)</param>
        /// <param name="_Rotation">Rotation qui va être apporter à la case en Radian</param>
        /// <param name="isCorner">Booléen utiliser uniquement si le _NewType, représentant si le mur est un mur droit ou un coin</param>
        public void ConvertCaseType(Vector2 _IndexCase, CASE_TYPE _NewType, float _Rotation = -1, bool isCorner = false)
        {
            // Si la rotation n'est pas spécifié, reprend l'ancienne rotation de la case
            if (_Rotation == -1)
            {
                _Rotation = m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetRotation();
            }

            // Selon le type dans lequel la case va être converti...
            switch (_NewType)
            {
                // Convertissement en classe Ground
                case CASE_TYPE.GROUND:
                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y] = new Ground(
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetVisibilityLevel(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetContent(),
                                    true,
                                    this.m_TextureRoomGround,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetPosition(),
                                    0,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetSourceRectangle(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetColor(),
                                    _Rotation,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetOrigin(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetScale(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetEffect(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetLayerDepth()
                        );
                    break;
                // Convertissement en classe Void
                case CASE_TYPE.VOID:
                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y] = new Void(
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetVisibilityLevel(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetContent(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetIsWalkable(),
                                    this.m_TextureVoid,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetPosition(),
                                    0,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetSourceRectangle(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetColor(),
                                    _Rotation,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetOrigin(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetScale(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetEffect(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetLayerDepth()
                        );
                    break;
                // Convertissement en classe Wall
                case CASE_TYPE.WALL:

                    Texture2D textureWall;

                    if (isCorner)
                    {
                        textureWall = m_TextureRoomCorner;
                    } 
                    else
                    {
                        textureWall = m_TextureRoomStraight;
                    }
                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y] = new Wall(
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetVisibilityLevel(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetContent(),
                                    false,
                                    textureWall,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetPosition(),
                                    0,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetSourceRectangle(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetColor(),
                                    _Rotation,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetOrigin(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetScale(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetEffect(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetLayerDepth()
                        );
                    break;
                // Convertissement en classe Door
                case CASE_TYPE.DOOR:
                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y] = new Door(
                                    false,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetVisibilityLevel(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetContent(),
                                    true,
                                    this.m_TextureRoomDoor,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetPosition(),
                                    0,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetSourceRectangle(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetColor(),
                                    _Rotation,
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetOrigin(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetScale(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetEffect(),
                                    m_GridOfCase[(int)_IndexCase.X][(int)_IndexCase.Y].GetLayerDepth()
                        );
                    break;
            }
        }
        
        /// <summary>
        /// S'occupe de changer les différentes cases dans le tableau m_GridOfCase pour créer la Room qui est passé en paramètre.
        /// La fonction change les cases du tableau par de nouvelle case pour représenter les murs, sols et portes d'une Room
        /// </summary>
        /// <param name="_RoomToDraw">Variable de type Room qu'on veut mettre dans m_GridOfCase (quadrillage)</param>
        public void DrawRoom(Room _RoomToDraw)
        {
            // Récupère la position initiale de la Room (représente le coin haut gauche de la Room)
            Vector2 roomInitialValue = _RoomToDraw.GetInitialIndex();

            Random random = new Random();

            int nbrMaxOfDoor = random.Next(1, 5);
            List<ROOM_SIDE>listOfSideFree = new List<ROOM_SIDE>();

            //Ajout le nombre de cote libre possible dans une salle
            for (int i = 0; i < MAX_SIDE_ROOM; i++) {
                listOfSideFree.Add((ROOM_SIDE)i);
            }

            // liste des portes de la salle courrante
            List<Vector2> listOfDoors = new List<Vector2>();
            for (int i = 0; i < nbrMaxOfDoor;i++) {

                ROOM_SIDE curr_RandSide = listOfSideFree[random.Next(0,listOfSideFree.Count)];
                int minDistanceFromCorner = 1;

                Vector2 randPosDoor = new Vector2();
                switch (curr_RandSide) {
                    case ROOM_SIDE.LEFT:
                        randPosDoor = new Vector2(
                            roomInitialValue.X, 
                            random.Next(
                                (int)roomInitialValue.Y + minDistanceFromCorner, 
                                (int)roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                            );
                        break;
                    case ROOM_SIDE.RIGHT:
                        randPosDoor = new Vector2(
                            roomInitialValue.X + _RoomToDraw.GetSizeX()-1,
                            random.Next(
                                (int)roomInitialValue.Y + minDistanceFromCorner, 
                                (int)roomInitialValue.Y + _RoomToDraw.GetSizeY()-1)
                            );
                        break;
                    case ROOM_SIDE.UP:
                        randPosDoor = new Vector2(
                            random.Next(
                                (int)roomInitialValue.X+minDistanceFromCorner,
                                (int)roomInitialValue.X + _RoomToDraw.GetSizeX()-1),
                            roomInitialValue.Y
                            );
                        break;
                    case ROOM_SIDE.DOWN :
                        randPosDoor = new Vector2(
                            random.Next(
                                (int)roomInitialValue.X + minDistanceFromCorner,
                                (int)roomInitialValue.X + _RoomToDraw.GetSizeX() - 1),
                            roomInitialValue.Y + _RoomToDraw.GetSizeY()-1);
                        break;
                }
                listOfDoors.Add(randPosDoor);
                listOfSideFree.Remove(curr_RandSide);
            }

            bool hasDoor = false;

            // Utilise deux boucles "for" pour parcourir toutes les cases que la Room va prendre
            for (int i = (int)roomInitialValue.X; i < roomInitialValue.X + _RoomToDraw.GetSizeX(); i++)
            {             
                for (int j = (int)roomInitialValue.Y; j < roomInitialValue.Y + _RoomToDraw.GetSizeY(); j++)
                {
                    int randSide = random.Next(3);//Nombre entre 0 et 3
                    CASE_TYPE curr_type = CASE_TYPE.WALL;
                    ROOM_SIDE curr_Side = ROOM_SIDE.UP;
                    
                    float rotation = -1f;
                    bool isCorner = false;
                   
                    // Vérifie les différents contour d'une salle pour y mettre les murs :
                    if (i == roomInitialValue.X)
                    {
                        curr_Side = ROOM_SIDE.LEFT;
                        isCorner = true;
                        //Par défaut on concidaire que c'est le mur de gauche de la Room
                        if (j == roomInitialValue.Y)
                        {
                            // Coin haut gauche de la Room
                            rotation = 90;
                        }
                        else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                        {
                            // Coin bas gauche de la Room
                            rotation = -1f; //<- angle par defaut de l'image
                        }
                        else {
                            isCorner = false;
                            rotation = 180;
                        }
                    }
                    else if (i == roomInitialValue.X + _RoomToDraw.GetSizeX() - 1)
                    {
                        //Par défaut c'est le mur à droit de la Room
                        curr_Side = ROOM_SIDE.RIGHT;
                        isCorner = true;
                        if (j == roomInitialValue.Y)
                        {
                            // Coin haut droite de la Room
                            rotation = 180;
                        }
                        else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                        {
                            // Coint bas droit de la Room
                            rotation = 270;                            
                        }
                        else {
                            isCorner = false;
                        }
                    }
                    else if (j == roomInitialValue.Y)
                    {
                        // Mur haut de la pièce
                        rotation = 270;
                        

                        curr_Side = ROOM_SIDE.UP;
                    }
                    else if (j == roomInitialValue.Y + _RoomToDraw.GetSizeY() - 1)
                    {
                        // Mur bas de la pièce
                        rotation = 90;

                        curr_Side = ROOM_SIDE.DOWN;
                    }
                    else {
                        // Si la case en cours de parcours n'a pas été changé en mur, elle devient un sol
                        curr_type = CASE_TYPE.GROUND;
                    }

                    //Verifie que la liste n'est pas vide et que la case en cours n'est pas un coin
                    if (listOfDoors.Count() > 0 && !isCorner)
                    {
                        //Parcourt la liste des portes
                        for (int index = listOfDoors.Count - 1; index >= 0; index--)
                        {
                            //Verifie que la case courrante est sur la même position que la porte en cours de lecture
                            if (listOfDoors[index] == new Vector2(i, j)) {
                                hasDoor = true; //Confirme qu'une porte a ete trouve
                                curr_type = CASE_TYPE.DOOR;
                                listOfDoors.Remove(new Vector2(i, j));
                            }
                        }
                    }
                    this.ConvertCaseType(new Vector2(i, j), curr_type, MathHelper.ToRadians(rotation), isCorner);
                }
            }

            if (!hasDoor)
            {
                //this.ConvertCaseType(new Vector2(roomInitialValue.X + 1, roomInitialValue.Y), CASE_TYPE.DOOR);
                
                //Parcourt la liste des portes
                foreach (Vector2 curr_Door in listOfDoors) {
                    this.ConvertCaseType(curr_Door, CASE_TYPE.DOOR);
                }
                /*for (int index = listOfDoors.Count - 1; index >= 0; index--)
                {
                    this.ConvertCaseType(listOfDoors[index], CASE_TYPE.DOOR);
                }*/
                Debug.WriteLine("PAS DE PORTE");
            }
        }

        /// <summary>
        /// Change la valeur de m_GridRow
        /// </summary>
        /// <param name="_GridCol">Texture2D étant la nouvelle valeur de m_GridRow</param>
        public void SetGridCol(int _GridCol)
        {
            this.m_GridCol = _GridCol;
        }

        /// <summary>
        /// Renvoie la valeur de m_GridCol
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_GridCol</returns>
        public int getGridCol()
        {
            return this.m_GridCol;
        }

        /// <summary>
        /// Change la valeur de m_GridRow
        /// </summary>
        /// <param name="_GridRaw">Texture2D étant la nouvelle valeur de m_GridRow</param>
        public void SetGridRow(int _GridRaw)
        {
            this.m_GridRow = _GridRaw;
        }

        /// <summary>
        /// Renvoie la valeur de m_GridRow
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_GridRow</returns>
        public int getGridRow()
        {
            return this.m_GridRow;
        }

        /// <summary>
        /// Change la valeur de m_TextureRoomCorner
        /// </summary>
        /// <param name="_TextureCorner">Texture2D étant la nouvelle valeur de m_TextureRoomCorner</param>
        public void SetTextureCorner(Texture2D _TextureCorner)
        {
            this.m_TextureRoomCorner = _TextureCorner;
        }

        /// <summary>
        /// Renvoie la valeur de m_TextureRoomCorner
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_TextureRoomCorner</returns>
        public Texture2D GetTextureCorner()
        {
            return this.m_TextureRoomCorner;
        }

        /// <summary>
        /// Change la valeur de m_TextureRoomStraight
        /// </summary>
        /// <param name="_TextureStraight">Texture2D étant la nouvelle valeur de m_TextureRoomStraight</param>
        public void SetTextureStraight(Texture2D _TextureStraight)
        {
            this.m_TextureRoomStraight = _TextureStraight;
        }

        /// <summary>
        /// Renvoie la valeur de m_TextureRoomStraight
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_TextureRoomStraight</returns>
        public Texture2D GetTextureStraight()
        {
            return this.m_TextureRoomStraight;
        }

        /// <summary>
        /// Change la valeur de m_TextureRoomGround
        /// </summary>
        /// <param name="_TextureGround">Texture2D étant la nouvelle valeur de m_TextureRoomGround</param>
        public void SetTextureGround(Texture2D _TextureGround)
        {
            this.m_TextureRoomGround = _TextureGround;
        }

        /// <summary>
        /// Renvoie la valeur de m_TextureRoomGround
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_TextureRoomGround</returns>
        public Texture2D GetTextureGround()
        {
            return this.m_TextureRoomGround;
        }

        /// <summary>
        /// Change la valeur de m_TextureVoid
        /// </summary>
        /// <param name="_TextureVoid">Texture2D étant la nouvelle valeur de m_TextureVoid</param>
        public void SetTextureVoid(Texture2D _TextureVoid)
        {
            this.m_TextureVoid = _TextureVoid;
        }

        /// <summary>
        /// Renvoie la valeur de m_TextureVoid
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_TextureVoid</returns>
        public Texture2D GetTextureVoid()
        {
            return this.m_TextureVoid;
        }

        /// <summary>
        /// Change la valeur de m_TextureRoomDoor
        /// </summary>
        /// <param name="_TextureRoomDoor">Texture2D étant la nouvelle valeur de m_TextureRoomDoor</param>
        public void SetTextureDoor(Texture2D _TextureRoomDoor)
        {
            this.m_TextureRoomDoor = _TextureRoomDoor;
        }

        /// <summary>
        /// Renvoie la valeur de m_TextureRoomDoor
        /// </summary>
        /// <returns>un Texture2D représentant la valeur de m_TextureRoomDoor</returns>
        public Texture2D GetTextureDoor()
        {
            return this.m_TextureRoomDoor;
        }

        /// <summary>
        /// Change la valeur de m_NumberOfRoom
        /// </summary>
        /// <param name="_NumberOfRoom">Entier étant la nouvelle valeur de m_NumberOfRoom</param>
        public void SetNumberOfRoom(int _NumberOfRoom)
        {
            this.m_NumberOfRoom = _NumberOfRoom;
        }

        /// <summary>
        /// Renvoie la valeur de m_NumberOfRoom
        /// </summary>
        /// <returns>un entier représentant la valeur de m_NumberOfRoom</returns>
        public int GetNumberOfRoom()
        {
            return this.m_NumberOfRoom;
        }

        /// <summary>
        /// Renvoie la valeur de m_GridOfCase, cette variable représente le quadrillage complet de l'étage en cours
        /// </summary>
        /// <returns>un tableau à 2 dimension représentant m_GridOfCase</returns>
        public Case[][] GetGridOfCase()
        {
            return this.m_GridOfCase;
        }
    }
}