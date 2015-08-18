using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class Barrier : PixelSensetiveSprite, ICollidablePixelBased
    {
        private const float k_BulletHitPrasentage = 0.55f;
        private const int k_Velocity = 55;
        public Rectangle MovementBounds { get; set; }
        private float m_RangeToMove;
        private int leftBarrier { get; set; }
        private int rightBarrier { get; set; }


        private const String k_AssetName = @"Sprites\Barrier_44x32";
        public Barrier(Game i_Game)
            : base(k_AssetName, i_Game)
        {
        }

        protected override void InitBounds()
        {
            Velocity = new Vector2(k_Velocity, 0);
            base.InitBounds();
        }


        public event EventHandler<EventArgs> BarrierHit;

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y < 0)
                {
                    Color[] bulletPixels = bullet.Pixels;
                    Rectangle bulletHitRectangle = new Rectangle(bullet.Bounds.Left, (int)(bullet.Bounds.Top - k_BulletHitPrasentage * bullet.Bounds.Height), bullet.Bounds.Height, bullet.Bounds.Width);

                    Rectangle collisionRectange = Rectangle.Intersect(Bounds, bulletHitRectangle);
                    for (int yPixel = collisionRectange.Top; yPixel < collisionRectange.Bottom; yPixel++)
                    {
                        for (int xPixel = collisionRectange.Left; xPixel < collisionRectange.Right; xPixel++)
                        {
                            Color myPixl = Pixels[(xPixel - Bounds.X) + ((yPixel - Bounds.Y) * Texture.Width)];
                            Color otherPixel = bullet.Pixels[(xPixel - bulletHitRectangle.X) + ((yPixel - bulletHitRectangle.Y) * bulletHitRectangle.Width)];

                            if (myPixl.A != 0 && otherPixel.A != 0)
                            {
                                myPixl.A = 0;
                            }
                        }
                    }
                    
                }
            }
            onBarrierHit();
        }

        private void onBarrierHit()
        {
            if (BarrierHit != null)
            {
                BarrierHit(this, EventArgs.Empty);
            }

            Texture.SetData(Pixels);
        }


        public override void Initialize()
        {
            base.Initialize();
            m_RangeToMove = Bounds.Width/4;
            rightBarrier = (int) (Bounds.Right + Bounds.Width + m_RangeToMove);
            leftBarrier = (int)(Bounds.Left - m_RangeToMove);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (Bounds.Right >= rightBarrier)
            {
                Velocity *= -1;
                Position = new Vector2(rightBarrier - 1 - Bounds.Width, Position.Y);
            }

            if (Bounds.Left <= leftBarrier)
            {
                Velocity *= -1;
                Position = new Vector2(leftBarrier + 1, Position.Y);
            }
            
            
        }
    }
}
