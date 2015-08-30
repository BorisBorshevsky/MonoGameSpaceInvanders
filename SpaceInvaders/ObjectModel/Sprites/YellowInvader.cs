using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class YellowInvader : Invader
    {
        public YellowInvader(GameScreen i_GameScreen)
            : base(i_GameScreen, k_AssetName, Color.LightYellow)
        { }

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