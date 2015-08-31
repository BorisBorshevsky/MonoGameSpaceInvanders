//*** Guy Ronen © 2008-2011 ***//

using System;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Screens
{
    class PauseScreen : GameScreen
    {
        private SpriteFont m_Font;
        private Vector2 m_MessagePosition = new Vector2(70, 300);

        public PauseScreen(GameScreen i_Game)
            : base(i_Game.Game)
        {
            IsModal = true;
            IsOverlayed = true;
            UseGradientBackground = true;
            BlackTintAlpha = 0.6f;
            UseFadeTransition = true;

            ActivationLength = TimeSpan.FromSeconds(0.5f);
            DeactivationLength = TimeSpan.FromSeconds(0.5f);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Font = ((IFontManager)Game.Services.GetService(typeof(IFontManager))).SpriteFont;
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(
                m_Font, 
@"
[ Game Paused ]
Press 'R' to Resume Game",
m_MessagePosition,
Color.White);

            SpriteBatch.End();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }

            m_MessagePosition.X = (float)Math.Pow(70, TransitionPosition);
        }
    }
}