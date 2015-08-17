using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Managers;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SapceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager r_Graphics;
        private SpriteBatch m_SpriteBatch;

        public SapceInvadersGame()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //services
            new InputManager(this);
            new GameStateManager(this);
            new CollisionsManager(this);


            //sprites
            new Background(this, @"Sprites\BG_Space01_1024x768", 0.3f);

            //new SpaceShip(this);
            new PlayersManager(this);
            new MotherShipDeployer(this);
            new InvaderGrid(this);

            /*
             * Color[] allpixels = new Color[texture height * texture width]
             * texture.getDate(allpixels)
             * if (allpixel[i + j * width].A != 0)
             * {
             * 
             * }
             *
             * texture.SetDate(allpixels) 
             *
             */


        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), m_SpriteBatch);

            base.LoadContent();
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void Draw(GameTime i_GameTime)
        {
            r_Graphics.GraphicsDevice.Clear(Color.Black);

            m_SpriteBatch.Begin();
            base.Draw(i_GameTime);
            m_SpriteBatch.End();
        }
    }
}