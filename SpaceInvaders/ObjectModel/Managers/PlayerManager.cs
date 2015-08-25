using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel.Managers
{
    public class PlayersManager : RegisteredComponent
    {
        private readonly List<Player> r_Players = new List<Player>();
        private int m_PlayerCounter = 0;
        private int m_LostPlayerCount = 0;
        private IGameStateService m_GameStateService;

        public PlayersManager(Game i_Game)
            : base(i_Game)
        {
            createPlayers();
        }

        private void createPlayers()
        {
            r_Players.Add(initializePlayer1(m_PlayerCounter++));
            r_Players.Add(initializePlayer2(m_PlayerCounter++));
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
            if (++m_LostPlayerCount == m_PlayerCounter)
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
