using System;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{


    public class GameOverScreen : GameScreen
    {
        private readonly Sprite r_GameOverMessage;

        private readonly Background r_Background;
        private SpriteFont m_FontArial;
        private Vector2 m_MsgPosition = new Vector2(70, 300);
        private readonly ISettingsManager m_SettingsManager;

        public GameOverScreen(Game i_Game)
            : base(i_Game)
        {
            r_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            r_Background.TintColor = Color.DarkOrange;
    
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            r_GameOverMessage = new Sprite(@"Sprites\GameOverMessage", this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_FontArial = Game.Services.GetService<IFontManager>().SpriteFont;
            r_GameOverMessage.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.05f, 0.7f));
            r_GameOverMessage.Animations.Enabled = true;
            r_GameOverMessage.PositionOrigin = r_GameOverMessage.SourceRectangleCenter;
            r_GameOverMessage.RotationOrigin = r_GameOverMessage.SourceRectangleCenter;
            r_GameOverMessage.Position = CenterOfViewPort;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }

            if (InputManager.KeyPressed(Keys.P))
            {
                m_SettingsManager.ResetScores();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game));                
            }

            if (InputManager.KeyPressed(Keys.F6))
            {
                m_SettingsManager.ResetGameSettings();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new MainMenuScreen(Game));  
            }
        }

        private string winningMsg()
        {
            string msg = "Game Over:\n";

            for (int i = 0; i < m_SettingsManager.NumOfPlayers; i++)
            {
                msg += string.Format("Player {0}: Final Score is {1}\n", i, m_SettingsManager.PlayersData[i].Score);
            }


            return msg;
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(m_FontArial, 
@"Press 'Esc'   to  End Game
Press 'P'     for New Game
Press 'F6'   for Main Menu"
            , m_MsgPosition,Color.White);

            SpriteBatch.DrawString(m_FontArial, winningMsg(), new Vector2(40, 70), Color.LightBlue);
            SpriteBatch.End();
        }
    }
}
