using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    public class Enemy : Sprite
    {
        private const string k_AssteName = @"Sprites\Enemy0101_32x32";

        public Enemy(Game i_Game, Color i_EnemyColor)
            : base(k_AssteName, i_Game)
        {
            m_TintColor = i_EnemyColor;
        }

        protected override void InitBounds()
        {
            m_Width = Texture.Width;
            m_Height = Texture.Height;
            //m_Position
        }
    }
}
