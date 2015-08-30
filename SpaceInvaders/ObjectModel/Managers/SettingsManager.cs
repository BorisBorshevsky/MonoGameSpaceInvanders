using System.Collections.Generic;
using System.Linq;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Managers
{
    class SettingsManager : GameService, ISettingsManager
    {
        private const int k_MaxAmountOfPlayers = 2;
        private const int k_InitialLivesPerPlayer = 3;

        private int m_GameLevel = 1;
        private GraphicsDeviceManager m_GraphicsDeviceManager;
        readonly List<IPlayerState> r_PlayersData = new List<IPlayerState>();

        public override void Initialize()
        {
            m_GraphicsDeviceManager = Game.Services.GetService(typeof(IGraphicsDeviceManager)) as GraphicsDeviceManager;
            base.Initialize();
        }

        public SettingsManager(Game i_Game)
            : base(i_Game)
        {
            for (int i = 0; i < k_MaxAmountOfPlayers; i++)
            {
                r_PlayersData.Add(new PlayerState(k_InitialLivesPerPlayer));
            }
        }

        public List<IPlayerState> PlayersData
        {
            get { return r_PlayersData; } 
        }

        public int GameLevel
        {
            get { return m_GameLevel; }
            private set { m_GameLevel = value; }
        }

        public void IncrementLevel()
        {
            GameLevel++;
        }

        public int NumOfPlayers
        {
            get { return r_PlayersData.Count(i_Player => i_Player.Enabled); }
        }

        public bool AllowWindowResizing
        {
            get { return Game.Window.AllowUserResizing; }
            set { Game.Window.AllowUserResizing = value; }
        }

        public bool FullScreenMode
        {
            get { return m_GraphicsDeviceManager.IsFullScreen; }
        }

        public void ToggleFullScreen()
        {
            m_GraphicsDeviceManager.ToggleFullScreen();
        }

        public bool IsMouseVisible
        {
            get { return Game.IsMouseVisible; }
        }

        public void ToggleMouseVisibility()
        {
            Game.IsMouseVisible = !Game.IsMouseVisible;
        }

        public int InitialLivesPerPlayer
        {
            get { return k_InitialLivesPerPlayer; }
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ISettingsManager), this);
        }

        public void ResetLives()
        {
            foreach (var playerState in PlayersData)
            {
                playerState.Lives = k_InitialLivesPerPlayer;
            }
        }

        public void ResetScores()
        {
            foreach (var playerState in PlayersData)
            {
                playerState.Score = 0;
            }
        }

        public void ResetGameSettings()
        {
            ResetScores();
            ResetLives();
            ResetGameLevel();
        }

        protected void ResetGameLevel()
        {
            GameLevel = 1;
        }

        public GameLevelSettings GetGameLevelSettings()
        {
            switch (GameLevel % 5)
            {
                case 1:
                    return new GameLevelSettings
                    {
                        BarrierShouldMove = false,
                        AdditionalInvadersColumns = 0,
                        AdditionalInvadersPoints = 0,
                        AdditionalBarrierSpeedPercent = 0,
                        AdditionalInvadersBullets = 0,
                    };
                case 2:
                {
                    return new GameLevelSettings
                    {
                        BarrierShouldMove = true,
                        AdditionalInvadersColumns = 1,
                        AdditionalInvadersPoints = 30,
                        AdditionalBarrierSpeedPercent = 0f,
                        AdditionalInvadersBullets = 0,
                    };
                }
                case 3:
                {
                    return new GameLevelSettings
                    {
                        BarrierShouldMove = true,
                        AdditionalInvadersColumns = 2,
                        AdditionalInvadersPoints = 30 * 2,
                        AdditionalBarrierSpeedPercent = 0.05f,
                        AdditionalInvadersBullets = 1,
                    };
                }
                case 4:
                {
                    return new GameLevelSettings
                    {
                        BarrierShouldMove = true,
                        AdditionalInvadersColumns = 3,
                        AdditionalInvadersPoints = 30 * 3,
                        AdditionalBarrierSpeedPercent = 0.05f * 2,
                        AdditionalInvadersBullets = 1,
                    };
                }
                default: // Level 5
                {
                    return new GameLevelSettings
                    {
                        BarrierShouldMove = true,
                        AdditionalInvadersColumns = 4,
                        AdditionalInvadersPoints = 30 * 4,
                        AdditionalBarrierSpeedPercent = 0.05f * 3,
                        AdditionalInvadersBullets = 20,
                    };
                }
            }
        }
    }
}