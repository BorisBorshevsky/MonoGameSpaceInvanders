using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.Screens
{

    public class WelcomeScreen : GameScreen
    {
        private const float k_ScaleFactor = 1.16f;
        private Sprite m_WelcomeMessage;
        private Background m_Background;
        private SpriteFont m_Font;
        private Vector2 m_MsgPosition = new Vector2(70, 300);

        public WelcomeScreen(Game i_Game)
            : base(i_Game)
        {
            new ScoresBoard(this, 5, Color.Orange);
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            m_WelcomeMessage = new Sprite(@"Sprites\WelcomeMessage", this);
            

            this.DeactivationLength = TimeSpan.FromSeconds(1);
            this.UseFadeTransition = false;
            this.BlendState = BlendState.NonPremultiplied;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Font = ((IFontManager)Game.Services.GetService(typeof(IFontManager))).SpriteFont;

            m_WelcomeMessage.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, k_ScaleFactor, 0.7f));
            m_WelcomeMessage.Animations.Enabled = true;
            m_WelcomeMessage.PositionOrigin = m_WelcomeMessage.SourceRectangleCenter;
            m_WelcomeMessage.RotationOrigin = m_WelcomeMessage.SourceRectangleCenter; // for scale
            m_WelcomeMessage.Position = CenterOfViewPort;
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

            if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
            {
                m_Background.Opacity = this.TransitionPosition;
                m_WelcomeMessage.Opacity = this.TransitionPosition;
            }

            if (m_WelcomeMessage.Width == m_WelcomeMessage.WidthBeforeScale)
            {
                m_WelcomeMessage.TintColor = Color.Yellow;
            }
            else if (m_WelcomeMessage.Width == (float)(m_WelcomeMessage.WidthBeforeScale * k_ScaleFactor))
            {
                m_WelcomeMessage.TintColor = Color.Red;
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
 m_MsgPosition,
 Color.White);

            SpriteBatch.End();
        }
    }
}
