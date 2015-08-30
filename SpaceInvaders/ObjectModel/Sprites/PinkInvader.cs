using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class PinkInvader : Invader
    {
        public PinkInvader(GameScreen i_Game)
            : base(i_Game, k_AssetName, Color.LightPink)
        { }
        
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