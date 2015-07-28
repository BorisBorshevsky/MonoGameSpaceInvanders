﻿using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    internal class YellowInvader : Invader
    {
        private const string k_AssteName = @"Sprites\Enemy0301_32x32";

        public YellowInvader(Game i_Game)
            : base(i_Game, k_AssteName, Color.LightYellow)
        {
        }

        public override int Score
        {
            get { return 120; }
        }
    }
}