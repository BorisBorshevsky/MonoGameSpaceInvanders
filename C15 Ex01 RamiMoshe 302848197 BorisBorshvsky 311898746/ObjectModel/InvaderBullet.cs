using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    class InvaderBullet : Bullet
    {
        public InvaderBullet(Game i_Game) : base(i_Game)
        {
            TintColor = Color.Gold;
        }
    }
}
