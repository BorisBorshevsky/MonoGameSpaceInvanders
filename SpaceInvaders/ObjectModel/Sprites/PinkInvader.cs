using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    internal class PinkInvader : Invader
    {

        public PinkInvader(Game i_Game)
            : base(i_Game, k_AssetName, Color.LightPink)
        {}
        
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