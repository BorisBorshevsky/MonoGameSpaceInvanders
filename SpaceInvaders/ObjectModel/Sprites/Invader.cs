using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.ObjectModel.Sprites
{
    public abstract class Invader : Sprite, ICollidable2D
    {
        private int m_MaxAmountOfBulletsSimultaniously = 1;
        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        private static readonly TimeSpan r_DyingAnimationTime = TimeSpan.FromSeconds(1.5);
        private const float k_BulletVelocity = 115f;
        private const int k_MinTimeBetweenShoots = 3000;
        private const int k_MaxTimeBetweenShoots = 10000;
        private static readonly Random sr_Random = new Random();
        private int m_TimeToNextShooting;
        private const int k_NumOfFrames = 2;
        private bool m_StartDieAnimationOnNextFrame = false;


        protected const string k_AssetName = @"Sprites\AllInvaders";
        protected bool isDying = false;
        private ISoundManager m_SoundManager;

        protected Invader(GameScreen i_GameScreen, String i_AssetName, Color i_EnemyColor)
            : base(i_AssetName, i_GameScreen)
        {
            m_TintColor = i_EnemyColor;
            m_TimeToNextShooting = sr_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
            PositionChanged += onJump;
        }

        private void onJump(object i_Sender)
        {
            int rectangleY = (SourceRectangle.Y + SourceRectangle.Height) % (SourceRectangle.Height * k_NumOfFrames);
            SourceRectangle = new Rectangle(SourceRectangle.X, rectangleY, SourceRectangle.Height, SourceRectangle.Width);
        }

        private void initAnimations()
        {
            Animations.Enabled = true;
        }

        private void onAnimationFinished(object i_Sender, EventArgs i_Args)
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
                        onInvaderDied();
                    }
                }
            }
        }

        private void onInvaderDied()
        {
            IsAlive = false;
            m_StartDieAnimationOnNextFrame = true;
            m_SoundManager.PlaySoundEffect(Sounds.k_EnemyKill);
            
            if (InvaderDied != null)
            {
                InvaderDied(this, EventArgs.Empty);
            }
        }

        private void startDyingAnimation()
        {
            isDying = true;
            RotateAnimator rotateAnimator = new RotateAnimator(MathHelper.TwoPi*5, r_DyingAnimationTime);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(r_DyingAnimationTime);

            CompositeAnimator deadAnimation = new CompositeAnimator("deadAnimation", r_DyingAnimationTime, this, shrinkAnimator, rotateAnimator);
            deadAnimation.Finished += onAnimationFinished;

            Animations.Add(deadAnimation);
        }

        public event EventHandler<EventArgs> InvaderDied;
        private ISettingsManager m_SettingsManager;

        public override void Initialize()
        {
            base.Initialize();
            m_SoundManager = Game.Services.GetService<ISoundManager>();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            m_MaxAmountOfBulletsSimultaniously += m_SettingsManager.GetGameLevelSettings().AdditionalInvadersBullets;
            initAnimations();
        }

        public event EventHandler<EventArgs> InvaderReachedBottom;

        void onInvaderReachedBottom()
        {
            if (InvaderReachedBottom != null)
            {
                InvaderReachedBottom.Invoke(this, EventArgs.Empty);
            }
        }


        public override void Update(GameTime i_GameTime)
        {
            if (m_StartDieAnimationOnNextFrame)
            {
                startDyingAnimation();
                m_StartDieAnimationOnNextFrame = false;
            }
            
            if (Bounds.Bottom > Game.GraphicsDevice.Viewport.Bounds.Bottom)
            {
                onInvaderReachedBottom();
            }

            CurrentElapsedTime += i_GameTime.ElapsedGameTime.TotalMilliseconds;
            r_Bullets.RemoveAll(i_Bullet => !i_Bullet.IsAlive);
            if (CurrentElapsedTime >= m_TimeToNextShooting && r_Bullets.Count < m_MaxAmountOfBulletsSimultaniously)
            {
                Shoot();
                CurrentElapsedTime = 0;
                m_TimeToNextShooting = sr_Random.Next(k_MinTimeBetweenShoots, k_MaxTimeBetweenShoots);
            }

            base.Update(i_GameTime);
        }

        protected void Shoot()
        {
            m_SoundManager.PlaySoundEffect(Sounds.k_EnemyGunShot);
            Bullet bullet = new Bullet(Screen);
            bullet.Initialize();
            bullet.TintColor = Color.OrangeRed;
            bullet.Position = new Vector2(Bounds.Left + Bounds.Width / 2 - bullet.Width / 2, Bounds.Bottom);
            bullet.Velocity = new Vector2(0, k_BulletVelocity);
            r_Bullets.Add(bullet);
        }

        protected override void Dispose(bool i_Disposing)
        {
            foreach (var bullet in r_Bullets)
            {
                bullet.Dispose();
            }
            
            base.Dispose(i_Disposing);
        }
    }
}