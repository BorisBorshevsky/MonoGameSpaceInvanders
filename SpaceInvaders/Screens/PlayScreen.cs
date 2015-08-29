using System;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            new ScoresBoard(this, 1, Color.White);
            
            
            m_PauseScreen = new PauseScreen(this);

            this.UseFadeTransition = true; 
            this.BlendState = BlendState.NonPremultiplied;
            this.SpritesSortMode = SpriteSortMode.Deferred;
        }



        private void onGameLost(object i_Sender, EventArgs i_EventArgs)
        {
            //todo add sound lose
            this.ExitScreen();
            this.Dispose();
            ScreensManager.SetCurrentScreen(new GameOverScreen(Game));
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
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
            m_SettingsManager.IncrementLevel();
            //todo add sound win
            this.ExitScreen();
            this.Dispose();
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }


    }
}
