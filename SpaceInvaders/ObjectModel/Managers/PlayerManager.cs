using Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders.Managers
{
    public class PlayersManager : RegisteredComponent
    {
        static readonly List<Player> Players = new List<Player>();
        private int s_PlayerCounter = 0;
        private int s_LostPlayerCount = 0;
        private IGameStateService m_GameStateService;

        public PlayersManager(Game i_Game)
            : base(i_Game)
        {
            createPlayers();
        }

        private void createPlayers()
        {
            Players.Add(initializePlayer1(s_PlayerCounter++));
            Players.Add(initializePlayer2(s_PlayerCounter++));
        }

        public override void Initialize()
        {
            base.Initialize();

            m_GameStateService = Game.Services.GetService(typeof(IGameStateService)) as IGameStateService;
        }

        private Player initializePlayer1(int i_PlayerId)
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
                AssteName = @"Sprites\Ship01_32x32"
            };

            return createPlayer(i_PlayerId, spaceShipConfiguration);
        }

        private Player createPlayer(int i_PlayerId, SpaceShipConfiguration i_SpaceShipConfiguration)
        {
            Player player = new Player(Game, i_SpaceShipConfiguration, i_PlayerId);
            player.PlayerLost += playerOnPlayerLost;

            return player;
        }

        private void playerOnPlayerLost(object i_Sender, EventArgs i_EventArgs)
        {
            if (++s_LostPlayerCount == s_PlayerCounter)
            {
                m_GameStateService.GameOver();
            }
        }

        private Player initializePlayer2(int i_PlayerId)
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
                AssteName = @"Sprites\Ship02_32x32"
            };


            return createPlayer(i_PlayerId, spaceShipConfiguration);
        }
    }
}
