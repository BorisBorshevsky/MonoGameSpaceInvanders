using System;
using System.Collections.Generic;
using Infrastructure.Animators;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;
using SpaceInvaders.Settings;

namespace SpaceInvaders.ObjectModel.Sprites
{
    abstract class Invader : Sprite, ICollidable2D
    {
        protected const string k_AssetName = @"Sprites\AllInvaders";
        private const float k_BulletVelocity = 115f;
        private const int k_MinTimeBetweenShoots = 3000;
        private const int k_MaxTimeBetweenShoots = 10000;
        private const int k_NumOfFrames = 2;

        private readonly List<Bullet> r_Bullets = new List<Bullet>();

        private static readonly TimeSpan r_DyingAnimationTime = TimeSpan.FromSeconds(1.5);
        private static readonly Random sr_Random = new Random();

        private int m_MaxAmountOfBulletsInBackground = 1;
        private int m_TimeToNextShooting;
        private bool m_StartDieAnimationOnNextFrame = false;
        private ISoundManager m_SoundManager;
        private ISettingsManager m_SettingsManager;

        protected bool m_IsDying = false;

        public abstract int Score { get; }
        public double CurrentElapsedTime { get; set; }

        public event EventHandler<EventArgs> InvaderDied;
        protected readonly int r_InitialTextureOffset;


        protected Invader(GameScreen i_GameScreen, String i_AssetName, Color i_EnemyColor, int i_InitialTextureOffset = 0)
            : base(i_AssetName, i_GameScreen)
        {
            r_InitialTextureOffset = i_InitialTextureOffset;
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
            m_IsDying = false;
            Dispose();
        }

        public bool IsDying
        {
            get { return m_IsDying; }
            protected set { m_IsDying = value; }
        }

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
            m_IsDying = true;
            RotateAnimator rotateAnimator = new RotateAnimator(MathHelper.TwoPi * 5, r_DyingAnimationTime);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(r_DyingAnimationTime);

            CompositeAnimator deadAnimation = new CompositeAnimator("deadAnimation", r_DyingAnimationTime, this, shrinkAnimator, rotateAnimator);
            deadAnimation.Finished += onAnimationFinished;

            Animations.Add(deadAnimation);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SoundManager = Game.Services.GetService<ISoundManager>();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            m_MaxAmountOfBulletsInBackground += m_SettingsManager.GetGameLevelSettings().AdditionalInvadersBullets;
            initializeSourceRectangle();
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
            if (CurrentElapsedTime >= m_TimeToNextShooting && r_Bullets.Count < m_MaxAmountOfBulletsInBackground)
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
        private void initializeSourceRectangle()
        {
            for (var i = 0; i < r_InitialTextureOffset; i++)
            {
                int rectangleY = (SourceRectangle.Y + SourceRectangle.Height) % (SourceRectangle.Height * k_NumOfFrames);
                SourceRectangle = new Rectangle(SourceRectangle.X, rectangleY, SourceRectangle.Height, SourceRectangle.Width);
            }
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