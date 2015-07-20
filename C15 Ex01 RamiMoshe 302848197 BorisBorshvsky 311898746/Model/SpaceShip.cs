using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceInvaders.Model
{
    class SpaceShip 
    {
        public Texture2D TextureEnemy { get; set; }

        public void LoadContent(ContentManager content)
        {
            TextureEnemy = content.Load<Texture2D>(@"Sprites\Enemy0101_32x32");
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
