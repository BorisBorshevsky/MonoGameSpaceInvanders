using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    internal class PinkInvader : Invader
    {
        //private const string k_AssteName = @"Sprites\Enemy0101_32x32";

        public PinkInvader(Game i_Game)
            : base(i_Game, k_AssetName, Color.LightPink)
        {

        }


        protected override void InitSourceRectangle()
        {
            SourceRectangle = new Rectangle(0, 0, 32, 32);
        }



        public override int Score
        {
            get { return 250; }
        }
    }
}