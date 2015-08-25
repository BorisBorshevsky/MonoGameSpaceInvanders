using System;
using System.Text;
using System.Windows.Forms;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;
using System.Collections.Generic;

namespace SpaceInvaders.Services
{
    public class GameStateService : GameService, IGameStateService
    {
        private readonly List<Player> r_Players = new List<Player>();

        public GameStateService(Game i_Game)
            : base(i_Game)
        {
        }


        public void GameOver(string i_Msg = "Game Over")
        {
            if (Game.IsActive)
            {
                var message = createMessageContent();
                MessageBox.Show(message, i_Msg, MessageBoxButtons.OK);
                Game.Exit();
            }
        }

        public void AddPlayer(Player i_Player)
        {
            r_Players.Add(i_Player);
        }

        private string createMessageContent()
        {
            StringBuilder message = new StringBuilder();
            foreach (Player player in r_Players)
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