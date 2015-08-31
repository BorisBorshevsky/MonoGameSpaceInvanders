using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager m_GraphicsMgr;
        private readonly InputManager m_InputManager;
        private readonly ScreensMananger m_ScreensMananger;
        private readonly FontManager m_FontManager;
        private readonly SoundManager m_SoundManager;
        private readonly SettingsManager m_SettingsManager;
        private readonly CollisionsManager m_CollisionsManager;

        public SpaceInvadersGame()
        {
            Content.RootDirectory = "Content";

            m_GraphicsMgr = new GraphicsDeviceManager(this);
            m_InputManager = new InputManager(this);
            m_SoundManager = new SoundManager(this);
            m_FontManager = new FontManager(this, @"Fonts\Arial");
            m_SettingsManager = new SettingsManager(this);
            m_CollisionsManager = new CollisionsManager(this);
            m_ScreensMananger = new ScreensMananger(this);
            m_ScreensMananger.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Space Invaders Game !!!";
            IsMouseVisible = true;
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