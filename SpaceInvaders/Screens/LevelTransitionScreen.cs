using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private readonly TimeSpan r_TimeForActivation = TimeSpan.FromSeconds(3);
        private Background m_Background;
        private SpriteFont m_Font;
        private TimeSpan m_Count;
        private Vector2 m_MsgPosition;
        private string m_Msg;
        private ISettingsManager m_SettingsManager;

        public LevelTransitionScreen(Game i_Game)
            : base(i_Game)
        {
            ActivationLength = r_TimeForActivation;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            m_Count = r_TimeForActivation;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            m_Font = Game.Services.GetService<IFontManager>().SpriteFont;
            m_MsgPosition = new Vector2(CenterOfViewPort.X - 30, CenterOfViewPort.Y);
            m_Msg = string.Empty;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            m_Count -= i_GameTime.ElapsedGameTime;
            
            m_Msg = string.Format("Level {0} in {1}", m_SettingsManager.GameLevel, m_Count.Seconds + 1);
            
            if (m_Count <= TimeSpan.Zero)
            {
                ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game));
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(m_Font, m_Msg, m_MsgPosition, Color.White);

            SpriteBatch.End();
        }
    }
}
