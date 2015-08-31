using System;
using Infrastructure;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class Barrier : PixelSensitiveSprite, ICollidablePixelBased
    {
        private const float k_BulletHitPercentage = 0.55f;
        private const int k_Velocity = 55;
        private const String k_AssetName = @"Sprites\Barrier_44x32";

        private float m_RangeToMove;
        private ISoundManager m_SoundManager;
        private ISettingsManager m_SettingsManager;

        public event EventHandler<EventArgs> BarrierHit;

        private int LeftBarrier { get; set; }
        private int RightBarrier { get; set; }
        public Rectangle MovementBounds { get; set; }
        
        public Barrier(GameScreen i_GameScreen)
            : base(k_AssetName, i_GameScreen)
        { }

        protected override void InitBounds()
        {
            Velocity = new Vector2(k_Velocity, 0);
            base.InitBounds();
        }

        public override void Collided(ICollidable i_Collidable)
        {
            PixelSensitiveSprite pixelSensitiveSpriteHitRectangle = i_Collidable as PixelSensitiveSprite;
            if (pixelSensitiveSpriteHitRectangle != null && pixelSensitiveSpriteHitRectangle is Bullet)
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
                m_SoundManager.PlaySoundEffect(Sounds.k_BarrierHit);
                onBarrierHit();
            }

            Invader invader = i_Collidable as Invader;
            if (invader != null)
            {
                RemoveFromTexture(invader.Bounds);
                onBarrierHit();
            }

            Texture.SetData(Pixels);
        }

        private void handleSpriteCollision(PixelSensitiveSprite i_PixelSensitiveSprite, Rectangle i_HitRectangle)
        {
            Rectangle collisionRectangle = Rectangle.Intersect(Bounds, i_HitRectangle);
            for (int yPixel = collisionRectangle.Top; yPixel < collisionRectangle.Bottom; yPixel++)
            {
                for (int xPixel = collisionRectangle.Left; xPixel < collisionRectangle.Right; xPixel++)
                {
                    Color myPixel = Pixels[(xPixel - Bounds.X) + ((yPixel - Bounds.Y) * Texture.Width)];
                    Color otherPixel = i_PixelSensitiveSprite.Pixels[(xPixel - i_HitRectangle.X) + ((yPixel - i_HitRectangle.Y) * i_HitRectangle.Width)];
                    if (myPixel.A != 0 && otherPixel.A != 0)
                    {
                        Pixels[(xPixel - Bounds.Left) + ((yPixel - Bounds.Top) * Bounds.Width)] = new Color(0, 0, 0, 0);
                    }
                }
            }
        }

        private void onBarrierHit()
        {
            if (BarrierHit != null)
            {
                BarrierHit.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            m_RangeToMove = (float)Bounds.Width / 4;
            RightBarrier = (int)(Bounds.Right + Bounds.Width + m_RangeToMove);
            LeftBarrier = (int)(Bounds.Left - m_RangeToMove);
            m_SoundManager = Game.Services.GetService<ISoundManager>();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();

            if (!m_SettingsManager.GetGameLevelSettings().BarrierShouldMove)
            {
                Velocity = Vector2.Zero;
            }

            Velocity += m_SettingsManager.GetGameLevelSettings().AdditionalBarrierSpeedPercent * Velocity;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (Bounds.Right >= RightBarrier)
            {
                Velocity *= -1;
                Position = new Vector2(RightBarrier - 1 - Bounds.Width, Position.Y);
            }

            if (Bounds.Left <= LeftBarrier)
            {
                Velocity *= -1;
                Position = new Vector2(LeftBarrier + 1, Position.Y);
            }
        }
    }
}
