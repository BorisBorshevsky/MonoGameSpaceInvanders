using System;
using Infrastructure.Common;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public abstract class Invader : Sprite, ICollidable2D
    {
        private const float k_BulletVelocity = 115f;
        private const int k_MinTimeBetweenShoots = 3000;
        private const int k_MaxTimeBetweenShoots = 10000;
        private static readonly Random r_Random = new Random();
        private IGameStateManager m_GameStateManagerService;
        private int m_TimeToNextShooting;

        protected Invader(Game i_Game, String i_AssteName, Color i_EnemyColor)
            : base(i_AssteName, i_Game)
        {
            m_TintColor = i_EnemyColor;
            m_TimeToNextShooting = r_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
        }

        public abstract int Score { get; }
        public double CurrentElapsedTime { get; set; }

        public override void Collided(ICollidable2D i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y < 0)
                {
                    Remove();
                    Dispose();
                    m_GameStateManagerService.AddToScore(Score);
                    if (InvaderDied != null)
                    {
                        InvaderDied(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler<EventArgs> InvaderDied;

        public override void Initialize()
        {
            m_GameStateManagerService = Game.Services.GetService(typeof (IGameStateManager)) as IGameStateManager;
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (BoundingRect.Bottom > Game.GraphicsDevice.Viewport.Bounds.Bottom)
            {
                m_GameStateManagerService.GameOver();
            }

            CurrentElapsedTime += i_GameTime.ElapsedGameTime.TotalMilliseconds;
            if (CurrentElapsedTime >= m_TimeToNextShooting)
            {
                Shoot();
                CurrentElapsedTime = 0;
                m_TimeToNextShooting = r_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
            }

            base.Update(i_GameTime);
        }

        protected void Shoot()
        {
            Bullet bullet = new Bullet(Game);
            bullet.Initialize();
            bullet.TintColor = Color.OrangeRed;
            bullet.Position = new Vector2(BoundingRect.Left + BoundingRect.Width/2 - bullet.Width/2, BoundingRect.Bottom);
            bullet.Velocity = new Vector2(0, k_BulletVelocity);
        }
    }
}