using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Color = Microsoft.Xna.Framework.Color;
using System.Diagnostics.CodeAnalysis;

namespace RogueProject
{
    public enum DIRECTION
    {
        UP,     // 0
        DOWN,   // 1
        RIGHT,  // 2
        LEFT,   // 3
        NONE    // 4
    }
    public enum ROOM_TYPE
    {
        EMPTY,      // 0
        HOSTILE,    // 1
        ITEM,       // 2
        START,      // 3
        END         // 4
    }
    public class GameCore : Game
    {
        // Constantes
        const int COL_GRID = 50;
        const int ROW_GRID = 30;

        float intervalEnemy = 0.5f;
        float timerEnemy = 0f;

        float intervalNightClub = 1.5f;
        float timerNightClub = 0f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private bool EnterKeyHold = false;
        private bool SpaceKeyHold = false;
        private bool DKeyHold = false;
        private bool NightClubMode = false;
        Random random = new Random(); 

        //Variable propre à la méthodolgie du projet
        Player m_Player;
        Room m_Room;
        Stage m_Stage;

        Texture2D m_TextureRoomCorner;
        Texture2D m_TextureRoomStraight;
        Texture2D m_TextureRoomDoor;
        Texture2D m_TextureVoid;

        Sprite m_TombOfPlayer;

        Enemy m_Enemy;

        Texture2D CaseTex;

        private List<Entity> m_entitiesL;

        public GameCore()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //change the screen size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
         
            //Window.IsBorderless = true;
            
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Abyssal Enigma: Rogue Requiem";        

            Texture2D CaseTex = Content.Load<Texture2D>("groundCaseV1");

            m_TextureRoomCorner = Content.Load<Texture2D>("CornerWallV2");
            m_TextureRoomStraight = Content.Load<Texture2D>("StraightWallV2");
            m_TextureRoomDoor = Content.Load<Texture2D>("OpenDoorV1");
            m_TextureVoid = Content.Load<Texture2D>("VoidCaseV1");

            m_Stage = new Stage(
                COL_GRID, ROW_GRID, 
                7, 
                m_TextureRoomCorner, 
                m_TextureRoomStraight, 
                CaseTex, 
                m_TextureVoid, 
                m_TextureRoomDoor, 
                _graphics
                );

            Texture2D Player_Tex2D = Content.Load<Texture2D>("playerV5");

            // Calcule la position du joueur pour le centrer dans les cases
            float centerPosX = m_Stage.GetGridOfCase()[0][0].GetPosition().X - Player_Tex2D.Width / 2;
            float centerPosY = m_Stage.GetGridOfCase()[0][0].GetPosition().Y - Player_Tex2D.Height / 2;



            // Création du joueur
            m_Player = new Player(
                new Vector2(0, 0),
                m_Stage.GetGridOfCase(),
                Player_Tex2D,
                1000000,
                1,
                1
            );

            m_Player.SetPosition(new Vector2 (centerPosX, centerPosY));

            //Création de l'enemy
            Texture2D Enemy_Tex2D = Content.Load<Texture2D>("enemyV1");
            m_Enemy = new Enemy(
                new Vector2(10, 10),
                m_Stage.GetGridOfCase(),
                Enemy_Tex2D,
                1,
                12,
                1
                
            );


            //m_Room = new Room(
            //    new Vector2(10, 10),
            //    10,
            //    12,
            //    ROOM_TYPE.EMPTY
            //);


            m_TombOfPlayer = new Sprite(
                Content.Load<Texture2D>("MorbiusV1"),
                m_Player.GetPosition()
                ) ;

            // Calcule la position de l'enemy pour le centrer dans les cases
            centerPosX = m_Stage.GetGridOfCase()[(int)m_Enemy.GetIndex().X][(int)m_Enemy.GetIndex().Y].GetPosition().X - Enemy_Tex2D.Width / 2;
            centerPosY = m_Stage.GetGridOfCase()[(int)m_Enemy.GetIndex().X][(int)m_Enemy.GetIndex().Y].GetPosition().Y - Enemy_Tex2D.Height / 2;
            m_Enemy.SetPosition(new Vector2(centerPosX, centerPosY));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

          
        }

        protected override void Update(GameTime gameTime)
        {

           

            // Récupère les inputs clavier
            var kstate = Keyboard.GetState();


            if (kstate.IsKeyDown(Keys.G) && !EnterKeyHold) {
                Texture2D CaseTex = Content.Load<Texture2D>("groundCase");
                m_Stage.ResetStage(_graphics);
                m_Stage.GenerateStage();

            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!(m_Player.GetHealthPoint() <= 0))
            {

                // Utilise la fonction Update du joueur,
                // Cette fonction s'occupe de ses diverses interactions (déplacer, attaquer, ouvrir inventaire...)
                if (m_Player.Update(gameTime, kstate, m_Stage.GetGridOfCase()))
                {

                    m_Enemy.Update(gameTime, m_Stage.GetGridOfCase());
                }


                // Permet de récupérer tous les entité sur une case et d'avoir leur position
                if (kstate.IsKeyDown(Keys.Enter) && !EnterKeyHold)
                {
                    // Empêche le code de ce ré-exécuter tant que la touche enter est appuyé
                    EnterKeyHold = true;

                    for (int i = 0; i <= m_Stage.GetGridOfCase().Length - 1; i++)
                    {
                        for (int j = 0; j <= m_Stage.GetGridOfCase()[i].Length - 1; j++)
                        {
                            Sprite currentContent = m_Stage.GetGridOfCase()[i][j].GetContent();
                            if (currentContent != null)
                            {
                                Debug.WriteLine("Sprite trouvé à : " + i + ";" + j + "\n" + "Ce Sprite est de type : " + m_Stage.GetGridOfCase()[i][j].GetContent().GetType().Name);
                            }
                        }
                    }
                }

                // Night club mode
                if (NightClubMode)
                {
                    for (int i = 0; i <= m_Stage.GetGridOfCase().Length - 1; i++)
                    {



                        for (int j = 0; j <= m_Stage.GetGridOfCase()[i].Length - 1; j++)
                        {
                            Color RandBow = new Color(random.Next(255), random.Next(255), random.Next(255));
                            m_Stage.GetGridOfCase()[i][j].SetColor(RandBow);
                        }
                    }
                }

                if (kstate.IsKeyDown(Keys.Space) && !SpaceKeyHold)
                {
                    NightClubMode = !NightClubMode;
                    SpaceKeyHold = true;

                }

                // Si la touche Enter est relâcher, permet de refaire le code de vérification des entités
                if (kstate.IsKeyUp(Keys.Enter))
                {
                    EnterKeyHold = false;
                }

                // Si la touche Espace est relâcher, permet de mettre le mode discoooooooooooooooooooooo
                if (kstate.IsKeyUp(Keys.Space))
                {
                    SpaceKeyHold = false;
                }

                base.Update(gameTime);
            }
            else
            {
                // Mettez à jour le compteur de temps
                timerNightClub += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Vérifiez si le temps écoulé est supérieur à l'intervalle
                if (timerNightClub >= intervalNightClub)
                {
                    m_Enemy.Update(gameTime, m_Stage.GetGridOfCase());
                    timerNightClub = 0f;
                }
                

            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);// Couleur de la fenetre

            _spriteBatch.Begin();

            m_Stage.Draw(_spriteBatch);
            //m_Stage.DrawRoom(_spriteBatch, m_Room, new Vector2(10, 10));

 
            if (m_Player.GetHealthPoint() <= 0)
            {
                m_TombOfPlayer.SetPosition(m_Player.GetPosition());
                m_TombOfPlayer.Draw(_spriteBatch);
            }
            else {
                m_Player.Draw(_spriteBatch);
            }
            
            m_Enemy.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
