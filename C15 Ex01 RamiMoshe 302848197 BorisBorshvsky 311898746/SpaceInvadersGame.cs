using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Model;
using System;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpaceInvadersGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D m_TextureShip;
        Texture2D m_TextureEnemy;
        Texture2D m_TextureBackground;

        Vector2 m_PositionBackground;
        Vector2 m_PositionEnemy;
        Vector2 m_PositionShip;

        Color m_TintBackground = Color.White;

        float m_ShipDirection = 1f;

        EnemyBase enemy;

        public SpaceInvadersGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            enemy = new EnemyBase();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemy.LoadContent(Content, GraphicsDevice);
            m_TextureBackground = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_TextureShip = Content.Load<Texture2D>(@"Sprites\Ship01_32x32");


            InitPositions();
        }

        private void InitPositions()
        {
            // 1. init the ship position
            // Get the bottom and center:
            float x = (float)GraphicsDevice.Viewport.Width / 2;
            float y = (float)GraphicsDevice.Viewport.Height;

            // Offset:
            x -= m_TextureShip.Width / 2;
            y -= m_TextureShip.Height / 2;

            // Put it a little bit higher:
            y -= 30;

            m_PositionShip = new Vector2(x, y);


            // 3. Init the bg position:
            m_PositionBackground = Vector2.Zero;

            //create an alpah channel for background:
            Vector4 bgTint = Vector4.One;
            bgTint.W = 0.4f; // set the alpha component to 0.2
            m_TintBackground = new Color(bgTint);
        }

        MouseState? m_PrevMouseState;

        private Vector2 GetMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;

            MouseState currState = Mouse.GetState();

            if (m_PrevMouseState != null)
            {
                retVal.X = (currState.X - m_PrevMouseState.Value.X);
                retVal.Y = (currState.Y - m_PrevMouseState.Value.Y);
            }

            m_PrevMouseState = currState;

            return retVal;
        }



        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            enemy.Update(gameTime);
            // TODO: Add your update logic here
            // get the current input devices state:
            GamePadState currGamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState currKeyboardState = Keyboard.GetState();

            // Allows the game to exit by GameButton 'back' button or Esc:
            if (currGamePadState.Buttons.Back == ButtonState.Pressed
                || currKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // move the ship using the GamePad left thumb stick and set viberation according to movement:
            m_PositionShip.X += currGamePadState.ThumbSticks.Left.X * 120 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            GamePad.SetVibration(PlayerIndex.One, 0, Math.Abs(currGamePadState.ThumbSticks.Left.X));

            // move the ship using the mouse:
            m_PositionShip.X += GetMousePositionDelta().X;

            // clam the position between screen boundries:
            m_PositionShip.X = MathHelper.Clamp(m_PositionShip.X, 0, this.GraphicsDevice.Viewport.Width - m_TextureShip.Width);

            // if we hit the wall, lets change direction:
            if (m_PositionShip.X == 0 || m_PositionShip.X == this.GraphicsDevice.Viewport.Width - m_TextureShip.Width)
            {
                m_ShipDirection *= -1f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            enemy.Draw(spriteBatch);
            spriteBatch.Draw(m_TextureBackground, m_PositionBackground, m_TintBackground); // tinting with alpha channel
            spriteBatch.Draw(m_TextureShip, m_PositionShip, Color.White); //no tinting
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
