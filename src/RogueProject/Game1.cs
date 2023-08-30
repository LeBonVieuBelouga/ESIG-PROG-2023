using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D AmongUsTexture;
        Vector2 AmongUs_Pos;
        float AmongUs_Speed;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            AmongUs_Pos = new Vector2(_graphics.PreferredBackBufferWidth/2,
                _graphics.PreferredBackBufferHeight/2);
            AmongUs_Speed = 350f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AmongUsTexture = Content.Load<Texture2D>("BasicPoseV4");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                AmongUs_Pos.Y -= AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                AmongUs_Pos.Y += AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                AmongUs_Pos.X -= AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                AmongUs_Pos.X += AmongUs_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Pour ne pas sortir de la zone

            if (AmongUs_Pos.X > _graphics.PreferredBackBufferWidth - AmongUsTexture.Width / 2)
            {
                AmongUs_Pos.X = _graphics.PreferredBackBufferWidth - AmongUsTexture.Width / 2;
            }
            else if (AmongUs_Pos.X < AmongUsTexture.Width / 2)
            {
                AmongUs_Pos.X = AmongUsTexture.Width / 2;
            }

            if (AmongUs_Pos.Y > _graphics.PreferredBackBufferHeight - AmongUsTexture.Height / 2)
            {
                AmongUs_Pos.Y = _graphics.PreferredBackBufferHeight - AmongUsTexture.Height / 2;
            }
            else if (AmongUs_Pos.Y < AmongUsTexture.Height / 2)
            {
                AmongUs_Pos.Y = AmongUsTexture.Height / 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                AmongUsTexture,
                AmongUs_Pos,
                null,
                Color.White,
                0f,
                new Vector2(AmongUsTexture.Width / 2, AmongUsTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}