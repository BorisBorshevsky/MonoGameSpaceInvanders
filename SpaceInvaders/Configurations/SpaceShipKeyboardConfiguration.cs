using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Configurations
{
    public class SpaceShipKeyboardConfiguration
    {
        public Keys LeftMoveButton { get; set; }

        public Keys RightMoveButton { get; set; }

        public List<Keys> ShootButton { get; set; }
    }
}