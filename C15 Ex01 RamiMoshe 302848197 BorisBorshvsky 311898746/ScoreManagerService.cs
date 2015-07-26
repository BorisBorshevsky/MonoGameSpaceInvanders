using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    class ScoreManagerService : GameService
    {
        public int Score { get; private set; }
        
        public ScoreManagerService(Game i_Game) : base(i_Game)
        {
        }

        public void AddToScore(int score)
        {
            this.Score = MathHelper.Clamp(this.Score + score, 0, int.MaxValue);
        }
    }
}
