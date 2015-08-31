using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.Screens;
using SpaceInvaders.Settings;

namespace SpaceInvaders
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager r_GraphicsMgr;
        private readonly IInputManager r_InputManager;
        private readonly IScreensMananger r_ScreensMananger;
        private readonly IFontManager r_FontManager;
        private readonly ISoundManager r_SoundManager;
        private readonly ISettingsManager r_SettingsManager;
        private readonly ICollisionsManager r_CollisionsManager;

        public SpaceInvadersGame()
        {
            Content.RootDirectory = "Content";

            r_GraphicsMgr = new GraphicsDeviceManager(this);
            r_InputManager = new InputManager(this);
            r_SoundManager = new SoundManager(this);
            r_FontManager = new FontManager(this, @"Fonts\Arial");
            r_SettingsManager = new SettingsManager(this);
            r_CollisionsManager = new CollisionsManager(this);
            r_ScreensMananger = new ScreensMananger(this);
            r_ScreensMananger.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Space Invaders Game !!!";
            IsMouseVisible = true;
            r_SoundManager.PlayBackgroundMusic(Sounds.k_BgMusic);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            r_GraphicsMgr.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (r_InputManager.KeyPressed(Keys.M))
            {
                r_SoundManager.ToggleMute();
            }
        }
    }
}