using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueProject
{
    public class GameCore : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D Player_Tex;
        Vector2 Player_Pos;
        float AmongUs_Speed;

        Sprite m_Player;

        Texture2D Bulio_Tex;
        Vector2 Bulio_Pos;
        float Tree_Speed;
        
        public GameCore()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //change the screen size

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Abyssal Enigma: Rogue Requiem";

            Player_Pos = new Vector2(_graphics.PreferredBackBufferWidth/2,
                _graphics.PreferredBackBufferHeight/2);
            AmongUs_Speed = 350f;

            //Player_Tex = Content.Load<Texture2D>("HeroV2");
            /*m_Player = new Sprite(
                Content.Load<Texture2D>("HeroV2"),
                _spriteBatch,
                Player_Pos
               );
            */
            Bulio_Pos = new Vector2(200f,
                _graphics.PreferredBackBufferHeight);
            Tree_Speed = 350f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Player_Tex = Content.Load<Texture2D>("MissingTextureInventory");
            Bulio_Tex = Content.Load<Texture2D>("bastienbulioBaseV1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                Player_Pos.Y -= AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                Player_Pos.Y += AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                Player_Pos.X -= AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                Player_Pos.X += AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

            Bulio_Pos.Y += Tree_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Bulio_Pos.Y > _graphics.PreferredBackBufferHeight - Bulio_Tex.Height / 2)
            {
                Bulio_Pos.Y = 0f;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // # Initialisation des sprites
            m_Player = new Sprite(
                Player_Tex,
                _spriteBatch,
                Player_Pos
               );

            _spriteBatch.Begin();

            // # Implémentation des sprites dans la fenêtre.

            // ## Joueur
            m_Player.DefaultDraw();

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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}