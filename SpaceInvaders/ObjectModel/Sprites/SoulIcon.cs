using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class SoulIcon : Sprite
    {
        public SoulIcon(string i_AssetName, GameScreen i_GameScreen)
            : base(i_AssetName, i_GameScreen)
        { }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
            Opacity = 0.5f;
            Scales = Vector2.One/2;
        }
    }
}
