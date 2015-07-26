using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    public class Background : Sprite
    {
        public Background(Game i_Game, string i_AssetName, float i_Opacity)
            : base(i_AssetName, i_Game)
        {
            this.Opacity = i_Opacity;
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            this.DrawOrder = int.MinValue;
        }
    }
}
