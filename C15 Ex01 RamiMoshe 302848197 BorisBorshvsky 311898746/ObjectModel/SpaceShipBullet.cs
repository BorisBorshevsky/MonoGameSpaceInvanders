using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    class SpaceShipBullet : Bullet
    {
        public SpaceShipBullet(Game i_Game) : base(i_Game)
        {
            this.TintColor = Color.Red;
        }
    }
}
