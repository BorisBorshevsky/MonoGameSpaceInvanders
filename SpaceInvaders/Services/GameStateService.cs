using System;
using System.Windows.Forms;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

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

        public void LoseLife()
        {
            AddToScore(k_LoosingLifeScorePanalty);
            if (--m_Lives == 0)
            {
                GameOver();
            }
        }

        public void AddToScore(int i_Score)
        {
            Score = MathHelper.Clamp(Score + i_Score, 0, int.MaxValue);
        }

        public void GameOver(string i_Msg = "Game Over")
        {
            if (Game.IsActive)
            {
                MessageBox.Show(String.Format("Score: {0}", Score), i_Msg, MessageBoxButtons.OK);
                Game.Exit();
            }
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IGameStateService), this);
        }
    }
}