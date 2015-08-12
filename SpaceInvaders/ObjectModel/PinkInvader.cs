using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    internal class PinkInvader : Invader
    {
        private const string k_AssteName = @"Sprites\Enemy0101_32x32";

        public PinkInvader(Game i_Game)
            : base(i_Game, k_AssteName, Color.LightPink)
        {
        }

        public override int Score
        {
            get { return 250; }
        }
    }
}