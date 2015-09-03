using System;
using System.Text;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.Menu;
using SpaceInvaders.ObjectModel.Sprites;
using SpaceInvaders.Settings;

namespace SpaceInvaders.Screens
{
    class GameOverScreen : GameScreen
    {
        private readonly Vector2 r_MessagePosition = new Vector2(70, 300);
        private readonly ISettingsManager r_SettingsManager;
        private readonly Sprite r_GameOverMessage;
        private readonly Background r_Background;

        private SpriteFont m_FontArial;
        
        public GameOverScreen(Game i_Game)
            : base(i_Game)
        {
            r_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            r_Background.TintColor = Color.DarkOrange;
    
            r_SettingsManager = Game.Services.GetService<ISettingsManager>();
            r_GameOverMessage = new Sprite(@"Sprites\GameOverMessage", this);
        }

        public override void Initialize()
        {
            base.Initialize();

            m_FontArial = Game.Services.GetService<IFontManager>().SpriteFont;
            r_GameOverMessage.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.5f, 0.3f));
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
                r_SettingsManager.ResetGameSettings();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game));                
            }

            if (InputManager.KeyPressed(Keys.F6))
            {
                r_SettingsManager.ResetGameSettings();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new MainMenuScreen(Game, new MenuConfiguration()));  
            }
        }

        private string createWinningMessage()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Game Over:");
            for (int i = 0; i < r_SettingsManager.NumOfPlayers; i++)
            {
                message.AppendLine(string.Format("Player {0}: Final Score is {1}", i, r_SettingsManager.PlayersData[i].Score));
            }

            return message.ToString();
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);

            SpriteBatch.Begin();
            SpriteBatch.DrawString(m_FontArial, 
@"Press 'Esc'   to  End Game
Press 'P'     for New Game
Press 'F6'   for Main Menu"
            , r_MessagePosition,Color.White);

            SpriteBatch.DrawString(m_FontArial, createWinningMessage(), new Vector2(40, 70), Color.LightBlue);
            SpriteBatch.End();
        }
    }
}