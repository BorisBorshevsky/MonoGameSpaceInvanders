using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel
{
    public class PixelSensitiveSprite : Sprite
    {
        public Color[] Pixels { get; protected set; }


        public PixelSensitiveSprite(string i_AssetName, GameScreen i_GameScreen, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_GameScreen, i_UpdateOrder, i_DrawOrder)
        {
        }

        public PixelSensitiveSprite(string i_AssetName, GameScreen i_GameScreen, int i_CallsOrder)
            : base(i_AssetName, i_GameScreen, i_CallsOrder)
        {
        }

        public PixelSensitiveSprite(string i_AssetName, GameScreen i_GameScreen)
            : base(i_AssetName, i_GameScreen)
        {
        }

        protected void RemoveFromTexture(Rectangle i_HitRectangle)
        {
            Rectangle collisionRectangle = Rectangle.Intersect(Bounds, i_HitRectangle);
            for (int yPixel = collisionRectangle.Top; yPixel < collisionRectangle.Bottom; yPixel++)
            {
                for (int xPixel = collisionRectangle.Left; xPixel < collisionRectangle.Right; xPixel++)
                {
                    Pixels[(xPixel - Bounds.Left) + ((yPixel - Bounds.Top) * Bounds.Width)] = new Color(0, 0, 0, 0);
                }
            }
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
            bool collided = false;

            if (base.CheckCollision(i_Source))
            {
                ICollidablePixelBased pixelSource = i_Source as ICollidablePixelBased;
                if (pixelSource != null)
                {
                    Rectangle collisionRectangle = Rectangle.Intersect(Bounds, pixelSource.Bounds);
                    for (int yPixel = collisionRectangle.Top; yPixel < collisionRectangle.Bottom; yPixel++)
                    {
                        for (int xPixel = collisionRectangle.Left; xPixel < collisionRectangle.Right; xPixel++)
                        {
                            Color myPixl = Pixels[(xPixel - Bounds.X) + ((yPixel - Bounds.Y) * Texture.Width)];
                            Color otherPixel = pixelSource.Pixels[(xPixel - pixelSource.Bounds.X) + ((yPixel - pixelSource.Bounds.Y) * pixelSource.Bounds.Width)];

                            if (myPixl.A != 0 && otherPixel.A != 0)
                            {
                                collided = true;
                                break;
                            }

                        }
                        if (collided)
                        {
                            break;
                        }
                    }
                }
                else if (i_Source is ICollidable2D)
                {
                    collided = true;
                }
                
            }

            return collided;
        }
    }
}
        


    

