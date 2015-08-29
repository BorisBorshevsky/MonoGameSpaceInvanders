//*** Guy Ronen © 2008-2011 ***//
using System;
using Infrastructure;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public class PauseScreen : GameScreen
    {
        private SpriteFont m_Font;
        private Vector2 m_MsgPosition = new Vector2(70, 300);

        public PauseScreen(GameScreen i_Game)
            : base(i_Game.Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.6f;
            this.UseFadeTransition = true;

            this.ActivationLength = TimeSpan.FromSeconds(0.5f);
            this.DeactivationLength = TimeSpan.FromSeconds(0.5f);
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
m_MsgPosition,
Color.White);

            SpriteBatch.End();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                this.ExitScreen();
            }

            m_MsgPosition.X = (float)Math.Pow(70, TransitionPosition);
        }
    }
}