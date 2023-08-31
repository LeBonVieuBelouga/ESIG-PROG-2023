using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueProject
{
    public class GameCore : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D AmongUs_Tex;
        Vector2 AmongUs_Pos;
        float AmongUs_Speed;

        Texture2D Tree_Tex;
        Vector2 Tree_Pos;
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

            AmongUs_Pos = new Vector2(_graphics.PreferredBackBufferWidth/2,
                _graphics.PreferredBackBufferHeight/2);
            AmongUs_Speed = 350f;

            Tree_Pos = new Vector2(200f,
                _graphics.PreferredBackBufferHeight);
            Tree_Speed = 350f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AmongUs_Tex = Content.Load<Texture2D>("BasicPoseV4");
            Tree_Tex = Content.Load<Texture2D>("bastienbulioBaseV1");
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

            if (AmongUs_Pos.X > _graphics.PreferredBackBufferWidth - AmongUs_Tex.Width / 2)
            {
                AmongUs_Pos.X = _graphics.PreferredBackBufferWidth - AmongUs_Tex.Width / 2;
            }
            else if (AmongUs_Pos.X < AmongUs_Tex.Width / 2)
            {
                AmongUs_Pos.X = AmongUs_Tex.Width / 2;
            }

            if (AmongUs_Pos.Y > _graphics.PreferredBackBufferHeight - AmongUs_Tex.Height / 2)
            {
                AmongUs_Pos.Y = _graphics.PreferredBackBufferHeight - AmongUs_Tex.Height / 2;
            }
            else if (AmongUs_Pos.Y < AmongUs_Tex.Height / 2)
            {
                AmongUs_Pos.Y = AmongUs_Tex.Height / 2;
            }

            Tree_Pos.Y += Tree_Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Tree_Pos.Y > _graphics.PreferredBackBufferHeight - Tree_Tex.Height / 2)
            {
                Tree_Pos.Y = 0f;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                AmongUs_Tex,
                AmongUs_Pos,
                null,
                Color.White,
                0f,
                new Vector2(AmongUs_Tex.Width / 2, AmongUs_Tex.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            _spriteBatch.Draw(
                Tree_Tex,
                Tree_Pos,
                null,
                Color.White,
                0f,
                new Vector2(Tree_Tex.Width / 2, Tree_Tex.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}