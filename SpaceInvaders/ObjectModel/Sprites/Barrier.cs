﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class Barrier : PixelSensitiveSprite, ICollidablePixelBased
    {
        private const float k_BulletHitPercentage = 0.55f;
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
            PixelSensitiveSprite pixelSensitiveSpriteHitRectangle = i_Collidable as PixelSensitiveSprite;
            if (pixelSensitiveSpriteHitRectangle != null && pixelSensitiveSpriteHitRectangle as Bullet != null)
            {
                Rectangle bulletHitRectangle;
                if (pixelSensitiveSpriteHitRectangle.Velocity.Y < 0)
                {
                    bulletHitRectangle = new Rectangle(pixelSensitiveSpriteHitRectangle.Bounds.Left,
                        (int)(pixelSensitiveSpriteHitRectangle.Bounds.Top - k_BulletHitPercentage * pixelSensitiveSpriteHitRectangle.Bounds.Height), pixelSensitiveSpriteHitRectangle.Bounds.Width,
                        (int)(pixelSensitiveSpriteHitRectangle.Bounds.Height));
                }
                else
                {
                    bulletHitRectangle = new Rectangle(pixelSensitiveSpriteHitRectangle.Bounds.Left,
                        (int)(pixelSensitiveSpriteHitRectangle.Bounds.Top + k_BulletHitPercentage * pixelSensitiveSpriteHitRectangle.Bounds.Height), pixelSensitiveSpriteHitRectangle.Bounds.Width,
                        (int)(pixelSensitiveSpriteHitRectangle.Bounds.Height));
                }

                handleSpriteCollision(pixelSensitiveSpriteHitRectangle, bulletHitRectangle);
            }

            var invader = pixelSensitiveSpriteHitRectangle as Invader;
            if (pixelSensitiveSpriteHitRectangle != null && invader != null)
            {
                handleSpriteCollision(pixelSensitiveSpriteHitRectangle, pixelSensitiveSpriteHitRectangle.Bounds);
            }

            Texture.SetData(Pixels);
            onBarrierHit();
        }

        private void handleSpriteCollision(PixelSensitiveSprite i_PixelSensitiveSprite, Rectangle i_HitRectangle)
        {
            Rectangle collisionRectangle = Rectangle.Intersect(Bounds, i_HitRectangle);
            for (int yPixel = collisionRectangle.Top; yPixel < collisionRectangle.Bottom; yPixel++)
            {
                for (int xPixel = collisionRectangle.Left; xPixel < collisionRectangle.Right; xPixel++)
                {
                    Color myPixel = Pixels[(xPixel - Bounds.X) + ((yPixel - Bounds.Y)*Texture.Width)];
                    Color otherPixel =
                        i_PixelSensitiveSprite.Pixels[
                            (xPixel - i_HitRectangle.X) + ((yPixel - i_HitRectangle.Y) * i_HitRectangle.Width)];

                    if (myPixel.A != 0 && otherPixel.A != 0)
                    {
                        Pixels[(xPixel - Bounds.Left) + ((yPixel - Bounds.Top)*Bounds.Width)] = new Color(0, 0, 0, 0);
                    }
                }
            }
        }

        private void onBarrierHit()
        {
            if (BarrierHit != null)
            {
                BarrierHit(this, EventArgs.Empty);
            }
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