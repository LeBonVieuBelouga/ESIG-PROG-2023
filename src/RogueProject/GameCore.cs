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
    public enum DIRECTION
    {
        UP,     // 0
        DOWN,   // 1
        RIGHT,  // 2
        LEFT    // 3
    }

    public class GameCore : Game
    {
        // Constantes
        const int TAB2D_WIDTH = 80;
        const int TAB2D_HEIGHT = 45;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Case[][] Tab2DCaseGround = new Case[TAB2D_WIDTH][];

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

            //Parcourt les colonnes du tableau2D
            for (int i = 0; i <= Tab2DCaseGround.Length - 1; i++)
            {
                //Définit la hauteur maximal du tableau      
                Tab2DCaseGround[i] = new Case[TAB2D_HEIGHT];

                //Parcourt les lignes du tableau2D
                for (int j = 0; j <= Tab2DCaseGround[i].Length - 1; j++)
                {
                    Tab2DCaseGround[i][j] = new Ground(
                            1,
                            null,
                            false,
                            Content.Load<Texture2D>("square"),
                            _spriteBatch,
                            new Vector2(24 * i, 24 * j)
                        );
                }
            }

            m_Player = new Player(
                new Vector2(0, 0),
                Content.Load<Texture2D>("player"),
                _spriteBatch,
                1,
                1,
                1,
                Tab2DCaseGround[0][0].GetPosition()
            );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Initialisation des Sprites

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


            m_Player.Update(gameTime, kstate, Tab2DCaseGround);

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

            //Dessine le quadrillage du niveau
            for (int i = 0; i < Tab2DCaseGround.Length-1; i++)
            {
                for (int j = 0; j < Tab2DCaseGround[i].Length-1; j++)
                {
                    Tab2DCaseGround[i][j].DefaultDraw(_spriteBatch); 
                }
            }

            m_Player.DefaultDraw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}