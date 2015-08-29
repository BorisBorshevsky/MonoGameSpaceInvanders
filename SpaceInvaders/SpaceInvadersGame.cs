
using Infrastructure;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SapceInvadersGame : Game
    {
        private GraphicsDeviceManager m_GraphicsMgr;
        private InputManager m_InputManager;
        private ScreensMananger m_ScreensMananger;
        private FontManager m_FontManager;
        private SoundManager m_SoundManager;
        private SettingsManager m_SettingsManager;

        public SapceInvadersGame()
        {
            m_GraphicsMgr = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            m_InputManager = new InputManager(this);
            m_SoundManager = new SoundManager(this);
            m_FontManager = new FontManager(this, @"Fonts\Arial");
            m_SettingsManager = new SettingsManager(this);
            
            new CollisionsManager(this);

            m_ScreensMananger = new ScreensMananger(this);
            m_ScreensMananger.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.Window.Title = "Space Invaders Game !!!";
            this.IsMouseVisible = true;
            m_SoundManager.PlayBackgroundMusic(Sounds.k_BgMusic);
        }


        protected override void Draw(GameTime i_GameTime)
        {
            m_GraphicsMgr.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_InputManager.KeyPressed(Keys.M))
            {
                m_SoundManager.ToggleMute();
            }
        }
    }
}