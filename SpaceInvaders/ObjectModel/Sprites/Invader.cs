using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public abstract class Invader : PixelSensitiveSprite, ICollidable2D
    {
        private const int k_MaxAmountOfBulletsSimultaniously = 0; //TODO: should be 2
        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        private static readonly TimeSpan k_dyingAnimationTime = TimeSpan.FromSeconds(1.5);
        private const float k_BulletVelocity = 115f;
        private const int k_MinTimeBetweenShoots = 3000;
        private const int k_MaxTimeBetweenShoots = 10000;
        private static readonly Random sr_Random = new Random();
        private int m_TimeToNextShooting;
        private const int k_NumOfFrames = 2;
        private bool m_startDieAnimationOnNextFrame = false;


        protected const string k_AssetName = @"Sprites\AllInvaders";
        protected bool isDying = false;

        protected Invader(Game i_Game, String i_AssetName, Color i_EnemyColor)
            : base(i_AssetName, i_Game)
        {
            m_TintColor = i_EnemyColor;
            m_TimeToNextShooting = sr_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
            this.PositionChanged += onJump;
        }

        private void onJump(object i_Sender, EventArgs i_Args)
        {
            int rectangleY = (SourceRectangle.Y + SourceRectangle.Height) % (SourceRectangle.Height * k_NumOfFrames);
            SourceRectangle = new Rectangle(SourceRectangle.X, rectangleY, SourceRectangle.Height, SourceRectangle.Width);
        }

        private void initAnimations()
        {
            Animations.Enabled = true;
        }

        private void onAnimationFinished(object sender, EventArgs e)
        {
            isDying = false;
            Dispose();
        }

        public bool IsDying
        {
            get { return isDying; }
            protected set { isDying = value; }
        }


        public abstract int Score { get; }
        public double CurrentElapsedTime { get; set; }

        public override void Collided(ICollidable i_Collidable)
        {
            if (IsAlive)
            {
                Bullet bullet = i_Collidable as Bullet;
                if (bullet != null)
                {
                    if (bullet.Velocity.Y < 0)
                    {
                        IsAlive = false;
                        m_startDieAnimationOnNextFrame = true;
                        if (OnInvaderDied != null)
                        {
                            OnInvaderDied(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void startDyingAnimation()
        {
            isDying = true;
            RotateAnimator rotateAnimator = new RotateAnimator(MathHelper.TwoPi*5, k_dyingAnimationTime);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(k_dyingAnimationTime);

            CompositeAnimator deadAnimation = new CompositeAnimator("deadAnimation", k_dyingAnimationTime, this, shrinkAnimator, rotateAnimator);
            deadAnimation.Finished += onAnimationFinished;

            Animations.Add(deadAnimation);
        }

        public event EventHandler<EventArgs> OnInvaderDied;

        public override void Initialize()
        {
            base.Initialize();
            initAnimations();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_startDieAnimationOnNextFrame)
            {
                startDyingAnimation();
                m_startDieAnimationOnNextFrame = false;
            }
            
            if (Bounds.Bottom > Game.GraphicsDevice.Viewport.Bounds.Bottom)
            {
                IGameStateService gameStateService = Game.Services.GetService(typeof(IGameStateService)) as IGameStateService;
                gameStateService.GameOver();
            }

            CurrentElapsedTime += i_GameTime.ElapsedGameTime.TotalMilliseconds;
            r_Bullets.RemoveAll(i_Bullet => !i_Bullet.IsAlive);
            if (CurrentElapsedTime >= m_TimeToNextShooting && r_Bullets.Count < k_MaxAmountOfBulletsSimultaniously)
            {
                Shoot();
                CurrentElapsedTime = 0;
                m_TimeToNextShooting = sr_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
            }

            base.Update(i_GameTime);
        }

        protected void Shoot()
        {
            Bullet bullet = new Bullet(Game);
            bullet.Initialize();
            bullet.TintColor = Color.OrangeRed;
            bullet.Position = new Vector2(Bounds.Left + Bounds.Width / 2 - bullet.Width / 2, Bounds.Bottom);
            bullet.Velocity = new Vector2(0, k_BulletVelocity);
            r_Bullets.Add(bullet);
        }
    }
}