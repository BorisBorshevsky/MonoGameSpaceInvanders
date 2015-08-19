using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel
{
    public class PixelSensitiveSprite : Sprite
    {
        public Color[] Pixels { get; protected set; }


        public PixelSensitiveSprite(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
        {
        }

        public PixelSensitiveSprite(string i_AssetName, Game i_Game, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_CallsOrder)
        {
        }

        public PixelSensitiveSprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game)
        {
        }


        protected override void LoadContent()
        {
            base.LoadContent();
            Pixels = new Color[Texture.Width * Texture.Height];
            Texture.GetData(Pixels);
        }

        public override void Initialize()
        {
            base.Initialize();

            Texture2D textureClone = new Texture2D(Game.GraphicsDevice, Texture.Width, Texture.Height);
            textureClone.SetData(Pixels);
            Texture = textureClone;
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collidedPerPixel = false;

            if (base.CheckCollision(i_Source))
            {
                ICollidablePixelBased source = i_Source as ICollidablePixelBased;
                if (source != null)
                {
                    Rectangle collisionRectange = Rectangle.Intersect(Bounds, source.Bounds);
                    for (int yPixel = collisionRectange.Top; yPixel < collisionRectange.Bottom; yPixel++)
                    {
                        for (int xPixel = collisionRectange.Left; xPixel < collisionRectange.Right; xPixel++)
                        {
                            Color myPixl = Pixels[(xPixel - Bounds.X) + ((yPixel - Bounds.Y) * Texture.Width)];
                            Color otherPixel =source.Pixels[(xPixel - source.Bounds.X) + ((yPixel - source.Bounds.Y) * source.Bounds.Width)];

                            if (myPixl.A != 0 && otherPixel.A != 0)
                            {
                                collidedPerPixel = true;
                                break;
                            }

                        }
                        if (collidedPerPixel)
                        {
                            break;
                        }
                    }

                }
            }
            
            return collidedPerPixel;
        }
    }
}
        


    

