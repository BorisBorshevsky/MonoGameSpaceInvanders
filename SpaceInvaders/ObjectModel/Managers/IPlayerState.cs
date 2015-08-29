using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ObjectModel.Managers
{
    public interface IPlayerState
    {
        int Score { get; set; }

        int Lives { get; set; }

        bool Enabled { get; set; }
    }
}
