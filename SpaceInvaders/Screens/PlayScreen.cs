using System;
using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.Screens
{
    class PlayScreen : GameScreen
    {
        private readonly BarrierComposer r_BarrierComposer;
        private readonly MotherShipDeployer r_MotherShipDeployer;
        private readonly InvaderGrid r_InvaderGrid;
        private readonly PauseScreen m_PauseScreen;
        private readonly PlayersManager r_playersManager;

        private Background r_Background;
        private ISettingsManager m_SettingsManager;
        private SpriteFont m_FontArial;


        public PlayScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1f);
            r_playersManager = new PlayersManager(this);
            r_playersManager.AllPlayersDied += onGameLost;
            
            r_BarrierComposer = new BarrierComposer(this);
            r_MotherShipDeployer = new MotherShipDeployer(this);

            r_InvaderGrid = new InvaderGrid(this);
            r_InvaderGrid.InvaderReachedBottom += onGameLost;
            r_InvaderGrid.AllEnemiesDied += onAllEnemiesDied;

            
            m_PauseScreen = new PauseScreen(this);

            this.UseFadeTransition = true; 
            this.BlendState = BlendState.NonPremultiplied;
            this.SpritesSortMode = SpriteSortMode.Deferred;
        }



        private void onGameLost(object i_Sender, EventArgs i_EventArgs)
        {
            m_SoundManager.PlaySoundEffect(Sounds.k_GameOver);
            this.ExitScreen();
            this.Dispose();
            ScreensManager.SetCurrentScreen(new GameOverScreen(Game));
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            m_SoundManager = Game.Services.GetService<ISoundManager>();
            m_FontArial = Game.Services.GetService<IFontManager>().SpriteFont;
        }


        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(m_PauseScreen);
            }

            if(InputManager.KeyPressed(Keys.T))
            {
                onAllEnemiesDied(null, null);
            }

            if (InputManager.KeyPressed(Keys.U))
            {
                onGameLost(null, null);
            }

        }

        private void onAllEnemiesDied(object i_Sender, EventArgs i_Args)
        {

            m_SoundManager.PlaySoundEffect(Sounds.k_LevelWin);
            m_SettingsManager.IncrementLevel();
            this.ExitScreen();
            this.Dispose();
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
         
            r_playersManager.Players.ForEach(i_Player => i_Player.ScoresBoard.Draw(i_GameTime));
        }
    }
}
