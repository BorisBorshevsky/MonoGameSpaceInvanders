using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    public class Background : Sprite
    {
        public Background(GameScreen i_GameScreen, string i_AssetName, float i_Opacity)
            : base(i_AssetName, i_GameScreen)
        {
            Opacity = i_Opacity;
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            DrawOrder = int.MinValue;
        }

      }
}