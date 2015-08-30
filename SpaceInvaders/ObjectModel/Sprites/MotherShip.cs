using System;
using Infrastructure;
using Infrastructure.Animators;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private const int k_HorizontalVelocity = 110;
        private const int k_Score = 750;

        private bool m_StartAnimationOnNextFrame = false;
        private ISoundManager m_SoundManager;

        public event EventHandler<EventArgs> MotherShipDied;

        public bool IsDying { get; private set; }

        public int Score
        {
            get { return k_Score; }
        }


        public MotherShip(GameScreen i_GameScreen, bool i_Enable = false)
            : base(k_AssetName, i_GameScreen)
        {
            m_TintColor = Color.Red;
            Enabled = i_Enable;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y < 0)
                {
                    m_StartAnimationOnNextFrame = true;
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            initAnimations();
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }
        
        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        private void startDieAnimation()
        {
            IsDying = true;
            Animations.Restart();
        }

        private void onMotherShipDied()
        {
            m_SoundManager.PlaySoundEffect(Sounds.k_MotherShipKill);
            if (MotherShipDied != null)
            {
                MotherShipDied.Invoke(this, EventArgs.Empty);
            }
        }

        private void initAnimations()
        {
            TimeSpan animationLength = TimeSpan.FromSeconds(2);
            SpriteAnimator hitAnimation = new BlinkAnimator(TimeSpan.FromSeconds(0.2), animationLength);
            SpriteAnimator fadeAnimator = new FadeAnimator(0.25f, animationLength);
            SpriteAnimator shrinkAnimator = new ShrinkAnimator(animationLength);
            SpriteAnimator dieAnimator = new CompositeAnimator("Die", animationLength, this, shrinkAnimator, fadeAnimator, hitAnimation);
            dieAnimator.Finished += onDieAnimatorFinished;
            Animations.Add(dieAnimator);
        }

        private void onDieAnimatorFinished(object i_Sender, EventArgs i_Args)
        {
            Visible = false;
            Enabled = false;
            IsDying = false;
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2(Width * -1f + 1, Height * 1f);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_StartAnimationOnNextFrame)
            {
                startDieAnimation();
                onMotherShipDied();
                m_StartAnimationOnNextFrame = false;
            }
            
            base.Update(i_GameTime);
            if (IsOutOfBounts())
            {
                Stop();
            }
        }

        public void Stop()
        {
            Visible = false;
            Enabled = false;
            Velocity = Vector2.Zero;
        }

        public void Start()
        {
            InitBounds();
            Visible = true;
            Enabled = true;
            Velocity = new Vector2(k_HorizontalVelocity, 0);
        }
    }
}