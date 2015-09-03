using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class LightBlueInvader : Invader
    {
        public LightBlueInvader(GameScreen i_GameScreen, int i_InitialTextureOffset = 0)
            : base(i_GameScreen, k_AssetName, Color.LightBlue, i_InitialTextureOffset)
        { }

        protected override void InitSourceRectangle()
        {
            SourceRectangle = new Rectangle(64, 0, 32, 32);
        }

        public override int Score
        {
            get { return 180; }
        }
    }
}