using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RogueProject
{
    public class GameCore : Game
    {
        public const int WINDOW_WIDTH = 1920; // Largeur de la fenêtre
        public const int WINDOW_HEIGHT = 1080; // Hauteur de la fenêtre

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Case> ListCaseGround = new List<Case>();
        Random random = new Random();
        //Variable avec la méthodolgie de base pour créer un sprite
        Texture2D Bulio_Tex;
        Vector2 Bulio_Pos;
        float Bulio_Velocity;

        //Variable propre à la méthodolgie du projet
        Sprite m_Player;

        public GameCore()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //change the screen size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.IsBorderless = true;

            //change the screen size
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Abyssal Enigma: Rogue Requiem";

            m_Player = new Sprite(
                Content.Load<Texture2D>("MissingTextureInventory"),
                _spriteBatch
                );

            for (int i = 1; i < 80; i++)
            {
                for (int j = 1; j < 45; j++)
                {
                    // Met des grounds aléatoirement dans le tableau
                    if (random.Next(1, 3) == 1)
                    {
                        ListCaseGround.Add(new Ground(
                            1,
                            null,
                            false,
                            Content.Load<Texture2D>("square"),
                            _spriteBatch,
                            new Vector2(24 * i, 24 * j)
                        ));
                    }

                }
            }

            m_Player.SetPosition(new Vector2(_graphics.PreferredBackBufferWidth/2,
                _graphics.PreferredBackBufferHeight/2));
            m_Player.SetVelocity(350f);
            
            Bulio_Pos = new Vector2(200f,
                _graphics.PreferredBackBufferHeight);
            Bulio_Velocity = 350f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Initialisation des Sprites
            //m_Player.SetTexture(Content.Load<Texture2D>("MissingTextureInventory"));

            //m_Player.SetTexture(Player_Tex);
            Bulio_Tex = Content.Load<Texture2D>("bastienbulioBaseV1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Texture2D Player_Tex = m_Player.GetTexture();
            Vector2 Player_Pos = m_Player.GetPosition();
            float Player_Velocity = 350f;

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                //Debug.WriteLine(ListCaseGround[indexNTM].GetContent().GetType().Name);
                Player_Pos.Y -= Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //m_Player.SetPosition(new Vector2 (m_Player.GetPosition().X, (m_Player.GetPosition().Y - Player_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds)));
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

            Bulio_Pos.Y += Bulio_Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Bulio_Pos.Y > _graphics.PreferredBackBufferHeight - Bulio_Tex.Height / 2)
            {
                Bulio_Pos.Y = 0f;
            }

            m_Player.SetPosition(Player_Pos);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // # Implémentation des sprites dans la fenêtre.

            // ## Joueur
            //m_Player.Draw();

            // ## Bulio
            _spriteBatch.Draw(
                Bulio_Tex,
                Bulio_Pos,
                null,
                Color.White,
                0f,
                new Vector2(Bulio_Tex.Width / 2, Bulio_Tex.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            for (int i = 0; i < ListCaseGround.Count; i++)
            {
                ListCaseGround[i].DefaultDraw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}