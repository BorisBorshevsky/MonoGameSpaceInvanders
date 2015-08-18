
using System;
using Infrastructure.Animators;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\MotherShip_32x120";
        private const int k_HorizontalVelocity = 110;
        private readonly int m_Score = 750;
        public bool IsDying { get; private set; }
        private bool m_StartAnimationOnNextFrame = false;

        
        public int Score
        {
            get { return m_Score; }
        }


        public MotherShip(Game i_Game, bool i_Enable = true)
            : base(k_AssteName, i_Game)
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
        }
        
        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            //            Texture = Game.Content.Load<Texture2D>(AssetName);
            base.LoadContent();
        }


        private void startDieAnimation()
        {
            IsDying = true;
            Animations.Restart();
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

        private void onDieAnimatorFinished(object sender, EventArgs e)
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

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            base.Draw(i_GameTime);
            m_SpriteBatch.End();
        }


    }
}