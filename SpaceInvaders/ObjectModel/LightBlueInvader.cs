using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    internal class LightBlueInvader : Invader
    {
        private const string k_AssteName = @"Sprites\Enemy0201_32x32";

        public LightBlueInvader(Game i_Game)
            : base(i_Game, k_AssteName, Color.LightBlue)
        {
        }

        public override int Score
        {
            get { return 180; }
        }
    }
}