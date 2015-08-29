using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ObjectModel.Managers
{
    class PlayerState : IPlayerState
    {
        public PlayerState(int i_Lives)
        {
            Lives = i_Lives;
            Enabled = true;
        }

        public int Score { get; set; }
        public int Lives { get; set; }
        public bool Enabled { get; set; }
      
    }
}
