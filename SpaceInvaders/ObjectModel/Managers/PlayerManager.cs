using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.ObjectModel.Managers
{
    class PlayersManager : RegisteredComponent
    {
        private readonly List<Player> r_Players = new List<Player>();

        private int m_PlayerCounter = 0;
        private int m_LostPlayerCount = 0;
        private ISettingsManager m_SettingsManager;

        public List<Player> Players
        {
            get { return r_Players; }
        }

        public PlayersManager(GameScreen i_GameScreen)
            : base(i_GameScreen)
        { }

        private void createPlayers(IList<IPlayerState> i_PlayersState)
        {
            if (i_PlayersState[0].Enabled)
            {
                r_Players.Add(initializePlayer1(m_PlayerCounter++, i_PlayersState[0]));
            }

            if (i_PlayersState[1].Enabled)
            {
                r_Players.Add(initializePlayer2(m_PlayerCounter++, i_PlayersState[1]));
            }
        }

        public event EventHandler<EventArgs> AllPlayersDied;

        void onAllPlayersDied()
        {
            if (AllPlayersDied != null)
            {
                AllPlayersDied.Invoke(this,EventArgs.Empty);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            m_SettingsManager = Game.Services.GetService(typeof(ISettingsManager)) as ISettingsManager;
            m_SettingsManager.ResetLives();

            createPlayers(m_SettingsManager.PlayersData);
        }

        private Player initializePlayer1(int i_PlayerId, IPlayerState i_PlayerState)
        {
            SpaceShipConfiguration spaceShipConfiguration = new SpaceShipConfiguration
            {
                SpaceShipMouseConfiguration = new SpaceShipMouseConfiguration
                {
                    ShootButton = eInputButtons.Left
                },
                SpaceShipKeyboardConfiguration = new SpaceShipKeyboardConfiguration
                {
                    ShootButton = new List<Keys> { Keys.LeftControl, Keys.RightControl },
                    RightMoveButton = Keys.Right,
                    LeftMoveButton = Keys.Left
                },
                TextColor = Color.Blue,
                AssetName = @"Sprites\Ship01_32x32"
            };

            return createPlayer(i_PlayerId, spaceShipConfiguration, i_PlayerState);
        }

        private Player createPlayer(int i_PlayerId, SpaceShipConfiguration i_SpaceShipConfiguration, IPlayerState i_PlayerState)
        {
            Player player = new Player(Screen, i_SpaceShipConfiguration, i_PlayerId, i_PlayerState);
            player.PlayerLost += onPlayerLost;

            return player;
        }

        private void onPlayerLost(object i_Sender, EventArgs i_EventArgs)
        {
            if (++m_LostPlayerCount == m_PlayerCounter)
            {
                onAllPlayersDied();
            }
        }

        private Player initializePlayer2(int i_PlayerId, IPlayerState i_PlayerState)
        {
            SpaceShipConfiguration spaceShipConfiguration = new SpaceShipConfiguration
            {
                SpaceShipKeyboardConfiguration = new SpaceShipKeyboardConfiguration
                {
                    ShootButton = new List<Keys> { Keys.W },
                    RightMoveButton = Keys.D,
                    LeftMoveButton = Keys.A
                },
                TextColor = Color.Green,
                AssetName = @"Sprites\Ship02_32x32"
            };

            return createPlayer(i_PlayerId, spaceShipConfiguration, i_PlayerState);
        }

        protected override void Dispose(bool i_Disposing)
        {
            if (i_Disposing)
            {
                foreach (var player in r_Players)
                {
                    player.Dispose();
                }
            }

            base.Dispose(i_Disposing);
        }
    }
}