using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    class SoulIcon : Sprite
    {

        public SoulIcon(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {}

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
//            Texture = Game.Content.Load<Texture2D>(AssetName);
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
