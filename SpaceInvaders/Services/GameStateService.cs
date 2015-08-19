using System;
using System.Text;
using System.Windows.Forms;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;
using System.Collections.Generic;

namespace SpaceInvaders.Services
{
    public class GameStateManager : GameService, IGameStateService
    {
        private const int k_InitialAmountOfLives = 3;
        private const int k_LoosingLifeScorePanalty = -1000;
        private int m_Lives = k_InitialAmountOfLives;

        public GameStateManager(Game i_Game)
            : base(i_Game)
        {
            Score = 0;
        }

        private int Score { get; set; }

        public void GameOver(List<Player> players, string i_Msg = "Game Over")
        {
            if (Game.IsActive)
            {
                var message = createMessageContent(players);
                MessageBox.Show(message, i_Msg, MessageBoxButtons.OK);
                Game.Exit();
            }
        }

        private static string createMessageContent(List<Player> players)
        {
            StringBuilder message = new StringBuilder();
            foreach (var player in players)
            {
                message.AppendLine(String.Format("Player: {0}, Score: {1}", player.ScoresBoard.PlayerNumber,
                    player.ScoresBoard.Score));
            }

            return message.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IGameStateService), this);
        }
    }
}