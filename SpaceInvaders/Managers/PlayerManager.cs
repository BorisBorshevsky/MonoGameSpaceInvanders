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

namespace SpaceInvaders.Managers
{
    public static class PlayersDeployer
    {
        static readonly List<Player> Players = new List<Player>();
        private static int s_PlayerCounter = 0;

        public static void CreatePlayers(Game i_Game)
        {
            Players.Add(createPlayer1(i_Game, s_PlayerCounter++));
            Players.Add(createPlayer2(i_Game, s_PlayerCounter++));
        }
        
        
        private static Player createPlayer1(Game i_Game, int i_PlayerId)
        {
            SpaceShipConfiguration spaceShipConfiguration1 = new SpaceShipConfiguration
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
                }
            };
            return new Player(i_Game, spaceShipConfiguration1, i_PlayerId);
        }

        private static Player createPlayer2(Game i_Game, int i_PlayerId)
        {
            SpaceShipConfiguration spaceShipConfiguration2 = new SpaceShipConfiguration
            {
                SpaceShipKeyboardConfiguration = new SpaceShipKeyboardConfiguration
                {
                    ShootButton = new List<Keys> { Keys.W },
                    RightMoveButton = Keys.D,
                    LeftMoveButton = Keys.A
                }
            };
            return new Player(i_Game, spaceShipConfiguration2, i_PlayerId);
        }
    }
}
