using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
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
    public class GameCore : Game
    {
        // Constantes
        const int COL_GRID = 50;
        const int ROW_GRID = 30;

        float intervalEnemy = 0.5f;
        float timerEnemy = 0f;

        float intervalNightClub = 0.0f;
        float timerNightClub = 0f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Case[][] GridOfCase = new Case[COL_GRID][];
        private bool EnterKeyHold = false;
        private bool SpaceKeyHold = false;
        private bool NightClubMode = false;
        Random random = new Random(); 

        //Variable propre à la méthodolgie du projet
        Player m_Player;
        Texture2D tombOfPlayer;

        Enemy m_Enemy;

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

            Texture2D CaseTex = Content.Load<Texture2D>("groundCase");
           

            int GridSizeWidth = COL_GRID * CaseTex.Width;
            int GridSizeHeight = ROW_GRID * CaseTex.Height;

            int startX = (_graphics.PreferredBackBufferWidth - GridSizeWidth) / 2;
            int startY = (_graphics.PreferredBackBufferHeight - GridSizeHeight) / 2;

            //Parcourt les colonnes du tableau2D
            for (int i = 0; i <= COL_GRID-1; i++)
            {
                //Définit la hauteur maximal du tableau2D      
                GridOfCase[i] = new Case[ROW_GRID];

                //Parcourt les lignes du tableau2D
                for (int j = 0; j <= ROW_GRID-1; j++)
                {
                    GridOfCase[i][j] = new Ground(
                            1,
                            null,
                            false,
                            CaseTex,
                            new Vector2(startX + CaseTex.Width * i, startY+ CaseTex.Height * j)
                        );
                    GridOfCase[i][j].DefaultValue();
                    Color color = new Color(255, 0, 255);
                    GridOfCase[i][j].SetColor(color);
                }
            }

            // Création du joueur
            Texture2D Player_Tex2D = Content.Load<Texture2D>("playerV5");
            m_Player = new Player(
                new Vector2(0, 0),
                GridOfCase,
                Player_Tex2D,
                100,
                1,
                1
            );

            // Calcule la position du joueur pour le centrer dans les cases
            float centerPosX = GridOfCase[(int)m_Player.GetIndex().X][(int)m_Player.GetIndex().Y].GetPosition().X - Player_Tex2D.Width / 2;
            float centerPosY = GridOfCase[(int)m_Player.GetIndex().X][(int)m_Player.GetIndex().Y].GetPosition().Y - Player_Tex2D.Height / 2;
            m_Player.SetPosition(new Vector2 (centerPosX, centerPosY));

            //Création de l'enemy
            Texture2D Enemy_Tex2D = Content.Load<Texture2D>("enemyV1");
            m_Enemy = new Enemy(
                new Vector2(10, 10),
                GridOfCase,
                Enemy_Tex2D,
                1,
                12,
                1
                
            );

            // Calcule la position de l'enemy pour le centrer dans les cases
            centerPosX = GridOfCase[(int)m_Enemy.GetIndex().X][(int)m_Enemy.GetIndex().Y].GetPosition().X - Enemy_Tex2D.Width / 2;
            centerPosY = GridOfCase[(int)m_Enemy.GetIndex().X][(int)m_Enemy.GetIndex().Y].GetPosition().Y - Enemy_Tex2D.Height / 2;
            m_Enemy.SetPosition(new Vector2(centerPosX, centerPosY));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tombOfPlayer = Content.Load<Texture2D>("MorbiusV1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            // Récupère les inputs clavier
            var kstate = Keyboard.GetState();

            // Utilise la fonction Update du joueur,
            // Cette fonction s'occupe de ses diverses interactions (déplacer, attaquer, ouvrir inventaire...)
            if (m_Player.Update(gameTime, kstate, GridOfCase)) {

                m_Enemy.Update(gameTime, GridOfCase);
            }


            // Permet de récupérer tous les entité sur une case et d'avoir leur position
            if (kstate.IsKeyDown(Keys.Enter) && !EnterKeyHold)
            {
                // Empêche le code de ce ré-exécuter tant que la touche enter est appuyé
                EnterKeyHold = true;
                
                for (int i = 0; i <= GridOfCase.Length - 1; i++)
                {
                    for (int j = 0; j <= GridOfCase[i].Length - 1; j++)
                    {
                        Sprite currentContent = GridOfCase[i][j].GetContent();
                        if (currentContent != null)
                        {
                            Debug.WriteLine("Sprite trouvé à : " + i + ";" + j + "\n" + "Ce Sprite est de type : " + GridOfCase[i][j].GetContent().GetType().Name);
                        }
                    }
                }
            }

            // Night club mode
            if (NightClubMode) 
            {
                // Mettez à jour le compteur de temps
                timerNightClub += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Vérifiez si le temps écoulé est supérieur à l'intervalle
                if (timerNightClub >= intervalNightClub)
                {
                    for (int i = 0; i <= GridOfCase.Length - 1; i++)
                    {

                        for (int j = 0; j <= GridOfCase[i].Length - 1; j++)
                        {
                            Color RandBow = new Color(random.Next(255), random.Next(255), random.Next(255));
                            GridOfCase[i][j].SetColor(RandBow);
                        }
                    }
                    timerNightClub = 0f;
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

            if (m_Player.GetHealthPoint() <= 0) {
                m_Player.SetTexture(tombOfPlayer);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);// Couleur de la fenetre

            _spriteBatch.Begin();

            //Dessine le quadrillage du niveau
            for (int i = 0; i <= GridOfCase.Length-1; i++)
            {
                for (int j = 0; j <= GridOfCase[i].Length-1; j++)
                {
                    GridOfCase[i][j].Draw(_spriteBatch); 
                }
            }
            m_Player.Draw(_spriteBatch);
            m_Enemy.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
