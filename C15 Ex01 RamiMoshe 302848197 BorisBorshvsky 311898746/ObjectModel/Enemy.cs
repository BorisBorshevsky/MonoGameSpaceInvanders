using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    public class Enemy : Sprite
    {
        private const string k_AssteName = @"Sprites\Enemy01_32x32";

        public Enemy(SapceInvadersGame i_Game, Color i_EnemyColor)
            : base(k_AssteName, i_Game)
        {
            m_TintColor = i_EnemyColor;
        }

        protected override void InitBounds()
        {
            base.InitBounds();

            // put in bottom center of view port:
            // get the bottom and center
            float x = (float)GraphicsDevice.Viewport.Width / 2;
            float y = 50;

            // offset:
            x -= m_Width / 2;

            m_Position = new Vector2(x, y);
        }
    }
}
