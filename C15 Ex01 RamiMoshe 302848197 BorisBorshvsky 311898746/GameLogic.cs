using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public class GameStateManagerService : GameService
    {
        private ScoreManagerService m_ScoreManagerService;

        public override void Initialize()
        {
            m_ScoreManagerService = Game.Services.GetService(typeof(ScoreManagerService)) as ScoreManagerService;

            base.Initialize();
        }

        public GameStateManagerService(Game i_Game)
            : base(i_Game)
        {
        }

        public void GameOver()
        {
            if (Game.IsActive)
            {
                MessageBox.Show(String.Format("Score: {0}", m_ScoreManagerService.Score), "Game Over", MessageBoxButtons.OK);
                Game.Exit();
            }
        }

    }
}
