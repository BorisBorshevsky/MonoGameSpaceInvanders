using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class YellowInvader : Invader
    {
        public YellowInvader(GameScreen i_GameScreen, int i_InitialTextureOffset = 0)
            : base(i_GameScreen, k_AssetName, Color.LightYellow, i_InitialTextureOffset)
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