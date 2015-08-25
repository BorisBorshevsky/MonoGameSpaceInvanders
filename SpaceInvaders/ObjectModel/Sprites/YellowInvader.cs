using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    internal class YellowInvader : Invader
    {
        //private const string k_AssteName = @"Sprites\Enemy0301_32x32";



        public YellowInvader(Game i_Game)
            : base(i_Game, k_AssetName, Color.LightYellow)
        {
        }

        protected override void InitSourceRectangle()
        {
            SourceRectangle = new Rectangle(32, 0, 32, 32);
        }


        public override int Score
        {
            get { return 120; }
        }
    }
}