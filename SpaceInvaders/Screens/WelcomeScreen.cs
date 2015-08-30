using System;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.Screens
{

    public class WelcomeScreen : GameScreen
    {
        private const float k_ScaleFactor = 1.16f;
        private readonly Sprite r_WelcomeMessage;
        private Background m_Background;
        private SpriteFont m_Font;
        private readonly Vector2 r_MsgPosition = new Vector2(100, 300);

        public WelcomeScreen(Game i_Game)
            : base(i_Game)
        {
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            r_WelcomeMessage = new Sprite(@"Sprites\WelcomeMessage", this);
            
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Font = ((IFontManager)Game.Services.GetService(typeof(IFontManager))).SpriteFont;

            r_WelcomeMessage.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, k_ScaleFactor, 0.7f));
            r_WelcomeMessage.Animations.Enabled = true;
            r_WelcomeMessage.PositionOrigin = r_WelcomeMessage.SourceRectangleCenter;
            r_WelcomeMessage.RotationOrigin = r_WelcomeMessage.SourceRectangleCenter; // for scale
            r_WelcomeMessage.Position = CenterOfViewPort;
            r_WelcomeMessage.TintColor = Color.Yellow;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.Enter))
            {
                ScreensManager.Remove(this);
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
            }

            if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }

            if (InputManager.KeyPressed(Keys.F6))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(Game));
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(m_Font, @"
Press 'Enter' to  Start Game
Press 'Esc'   to  End Game
Press 'F6'     for Main Menu

",
 r_MsgPosition,
 Color.White);

            SpriteBatch.End();
        }
    }
}
