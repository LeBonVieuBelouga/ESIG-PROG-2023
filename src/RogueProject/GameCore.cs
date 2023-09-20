using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace RogueProject
{
    public class GameCore : Game
    {
        // Constantes
        const int COL_GRID = 50;
        const int RAW_GRID = 30;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Case[][] GridOfCase = new Case[COL_GRID][];

        Random random = new Random();

        private Case[][] cases = new Case[80][];   


        private bool releaseUpKey = false;
        private bool releaseDownKey = false;
        private bool releaseRightKey = false;
        private bool releaseLeftKey = false;

        //Variable propre à la méthodolgie du projet
        Sprite m_Player;

        private List<Entity> m_entitiesL;

        public GameCore()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //change the screen size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
         
            Window.IsBorderless = true;
            
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Abyssal Enigma: Rogue Requiem";

            Texture2D Player_Tex2D = Content.Load<Texture2D>("MissingTextureInventory");

            m_Player = new Sprite(
                Player_Tex2D,
                _spriteBatch
                );
            m_Player.DefaultValue();

            Texture2D CaseTex = Content.Load<Texture2D>("MissingTexture32x32");

            int GridSizeWidth = COL_GRID * CaseTex.Width;
            int GridSizeHeight = RAW_GRID * CaseTex.Height;

            int startX = (_graphics.PreferredBackBufferWidth - GridSizeWidth) / 2;
            int startY = (_graphics.PreferredBackBufferHeight - GridSizeHeight) / 2;

            //Parcourt les colonnes du tableau2D
            for (int i = 0; i <= COL_GRID-1; i++)
            {
                //Définit la hauteur maximal du tableau2D      
                GridOfCase[i] = new Case[RAW_GRID];

                //Parcourt les lignes du tableau2D
                for (int j = 0; j <= RAW_GRID-1; j++)
                {
                    GridOfCase[i][j] = new Ground(
                            1,
                            null,
                            false,
                            CaseTex,
                            _spriteBatch,
                            new Vector2(startX + CaseTex.Width * i, startY+ CaseTex.Height * j)
                        );
                    GridOfCase[i][j].DefaultValue();
                }
            }

            m_Player.SetPosition(new Vector2(_graphics.PreferredBackBufferWidth/2,
                _graphics.PreferredBackBufferHeight/2));
            m_Player.SetVelocity(800f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Initialisation des Sprites
            m_Player.SetTexture(Content.Load<Texture2D>("MissingTextureInventory"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Texture2D Player_Tex = m_Player.GetTexture();
            Vector2 Player_Pos = m_Player.GetPosition();
            float Player_Velocity = m_Player .GetVelocity();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                //Debug.WriteLine(ListCaseGround[indexNTM].GetContent().GetType().Name);
                Player_Pos.Y -= Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                
            }
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                Player_Pos.Y += Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                Player_Pos.X -= Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                Player_Pos.X += Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Pour ne pas sortir de la zone
            if (Player_Pos.X > _graphics.PreferredBackBufferWidth - Player_Tex.Width / 2)
            {
                Player_Pos.X = _graphics.PreferredBackBufferWidth - Player_Tex.Width / 2;
            }
            else if (Player_Pos.X < Player_Tex.Width / 2)
            {
                Player_Pos.X = Player_Tex.Width / 2;
            }

            if (Player_Pos.Y > _graphics.PreferredBackBufferHeight - Player_Tex.Height / 2)
            {
                Player_Pos.Y = _graphics.PreferredBackBufferHeight - Player_Tex.Height / 2;
            }
            else if (Player_Pos.Y < Player_Tex.Height / 2)
            {
                Player_Pos.Y = Player_Tex.Height / 2;
            }

            m_Player.SetPosition(Player_Pos);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Firebrick);// Couleur de la fenetre

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
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
