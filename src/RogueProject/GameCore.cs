using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Generic;

namespace RogueProject
{
    public enum DIRECTION
    {
        UP,     // 0
        DOWN,   // 1
        RIGHT,  // 2
        LEFT    // 3
    }

    public class GameCore : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Case> m_ListCaseGround = new List<Case>();
        Random random = new Random();



        private bool releaseUpKey = false;
        private bool releaseDownKey = false;
        private bool releaseRightKey = false;
        private bool releaseLeftKey = false;


        //Variable propre à la méthodolgie du projet
        Player m_Player;

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

            m_Player = new Player(
                Content.Load<Texture2D>("player"),
                _spriteBatch,
                1,
                1,
                1,
                new Vector2(24,24)
                );

            for (int i = 1; i < 80; i++)
            {
                for (int j = 1; j < 45; j++)
                {
                    // Met des grounds aléatoirement dans le tableau
                    //if (random.Next(1, 3) == 1)
                    //{
                    // tableau(1-> CASE, [i,j], 2->Case, [i,j+1])
                        m_ListCaseGround.Add(new Ground(
                            1,
                            null,
                            false,
                            Content.Load<Texture2D>("square"),
                            _spriteBatch,
                            new Vector2(24 * i, 24 * j)
                        ));
                    //}

                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Initialisation des Sprites
            //m_Player.SetTexture(Content.Load<Texture2D>("MissingTextureInventory"));

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

            m_Player.Update(gameTime, kstate, m_ListCaseGround);

            //Debug.WriteLine(m_Player.GetTexture().Width);
            //Debug.WriteLine(m_ListCaseGround[1].GetTexture().Width);


            // Permet d'avoir le nom de la classe d'un objet
            //Debug.WriteLine(m_ListCaseGround[index].GetContent().GetType().Name);


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

            //m_Player.SetPosition(Player_Pos);

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < m_ListCaseGround.Count; i++)
            {
                m_ListCaseGround[i].DefaultDraw(_spriteBatch);
            }
            m_Player.DefaultDraw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}