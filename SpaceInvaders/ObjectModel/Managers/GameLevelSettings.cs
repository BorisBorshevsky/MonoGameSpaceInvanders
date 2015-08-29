using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ObjectModel.Managers
{
    public class GameLevelSettings
    {
        public bool BarrierShouldMove { get; set; }

        public int AdditionalInvadersColoumns { get; set; }

        public int AdditionalInvadersPoints { get; set; }

        public float AdditionalBarrierSpeedPrecent { get; set; }

        public int AdditionalInvadersBullets { get; set; }
    }
}
