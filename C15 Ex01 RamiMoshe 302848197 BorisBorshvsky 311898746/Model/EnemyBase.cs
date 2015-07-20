using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Model
{
    class EnemyBase 
    {
        Vector2 PositionEnemy;

        public Texture2D TextureEnemy { get; set; }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {

            TextureEnemy = content.Load<Texture2D>(@"Sprites\Enemy0101_32x32");

            initPosition(graphicsDevice);
        }

        private void initPosition(GraphicsDevice graphicsDevice)
        {
            // 2. Init the enemy position
            float x = (float)graphicsDevice.Viewport.Width / 2;
            float y = 50;

            // Offset:
            x -= TextureEnemy.Width / 2;

            PositionEnemy = new Vector2(x, y);

        }

        public void Update(GameTime gameTime)
        {
            PositionEnemy.X += 80 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureEnemy, PositionEnemy, Color.LightPink); // purple ship
        }
    }
}
