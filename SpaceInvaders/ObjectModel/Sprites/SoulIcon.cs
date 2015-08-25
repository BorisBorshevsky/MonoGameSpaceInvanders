using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class SoulIcon : Sprite
    {

        public SoulIcon(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {}

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

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            base.Draw(i_GameTime);
            m_SpriteBatch.End();
        }
    }
}
