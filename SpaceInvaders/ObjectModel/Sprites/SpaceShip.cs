using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.Animators;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel.Sprites
{
    class SpaceShip : Sprite, ICollidable2D
    {
        private const int k_MaxAmountOfBulletsInBackground = 2;
        private const float k_Velocity = 150f;
        private const float k_BulletVelocity = 115f;

        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        private readonly SpaceShipConfiguration r_SpaceShipConfiguration;
        private readonly int r_InitialOffsetMultiplayer;

        private bool m_IsHitable = true;
        private ISoundManager m_SoundManager;

        public event EventHandler<EventArgs> BulletCollided;
        public event EventHandler<EventArgs> SpaceShipHit;
        public event EventHandler<EventArgs> Died;

        public IInputManager InputManager { get; set; }

        public SpaceShip(GameScreen i_GameScreen, SpaceShipConfiguration i_SpaceShipConfiguration, int i_Id)
            : base(i_SpaceShipConfiguration.AssetName, i_GameScreen)
        {
            r_SpaceShipConfiguration = i_SpaceShipConfiguration;
            r_InitialOffsetMultiplayer = i_Id;
        }


        public override void Collided(ICollidable i_Collidable)
        {
            if (m_IsHitable)
            {
                Bullet bullet = i_Collidable as Bullet;
                if (bullet != null)
                {
                    if (bullet.Velocity.Y > 0)
                    {
                        onSpaceShipHit();
                    }
                }

                Invader invader = i_Collidable as Invader;
                if (invader != null)
                {
                    onDied();
                }
            }
        }

        private void onSpaceShipHit()
        {
            m_SoundManager.PlaySoundEffect(Sounds.k_LifeDie);

            if (SpaceShipHit != null)
            {
                SpaceShipHit.Invoke(this, EventArgs.Empty);
            }
        }

        private void onDied()
        {
            if (Died != null)
            {
                Died(this, EventArgs.Empty);
            }
        }

        public void StartHitAnimation()
        {
            m_IsHitable = false;
            Animations["Hit"].Restart();
        }

        public void StartDieAnimation()
        {
            m_IsHitable = false;
            Animations["Die"].Restart();
        }

        public override void Initialize()
        {
            base.Initialize();
            InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            initAnimations();
        }

        public event EventHandler<EventArgs> DieAnimationFinished;

        private void initAnimations()
        {
            BlinkAnimator hitAnimation = new BlinkAnimator("Hit", TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(2));
            Animations.Add(hitAnimation);
            hitAnimation.Finished += onHitAnimationFinished;

            SpriteAnimator rotateAnimator = new RotateAnimator(4 * MathHelper.TwoPi, TimeSpan.FromSeconds(2));
            SpriteAnimator fadeAnimator = new FadeAnimator(0.25f, TimeSpan.FromSeconds(2));
            SpriteAnimator dieAnimator = new CompositeAnimator("Die", TimeSpan.FromSeconds(2), this, rotateAnimator, fadeAnimator);
            dieAnimator.Finished += onDieAnimationFinished;

            Animations.Add(dieAnimator);

            Animations.Enabled = true;
            dieAnimator.Enabled = false;
            hitAnimation.Enabled = false;
        }

        private void onHitAnimationFinished(object i_Sender, EventArgs i_EventArgs)
        {
            m_IsHitable = true;
        }

        private void onDieAnimationFinished(object i_Sender, EventArgs i_EventArgs)
        {
            Visible = false;
            Enabled = false;
            if (DieAnimationFinished != null)
            {
                DieAnimationFinished.Invoke(this, EventArgs.Empty);
            }
        }

        public void ResetPosition()
        {
            Position = new Vector2(r_InitialOffsetMultiplayer * Bounds.Width, (int)(GraphicsDevice.Viewport.Height - Bounds.Height * 1.25));
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            ResetPosition();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            handleInputs();
            m_Position.X = MathHelper.Clamp(m_Position.X, 0, Game.GraphicsDevice.Viewport.Width - Bounds.Width);
        }


        private void handleMouse()
        {
            if (r_SpaceShipConfiguration.SpaceShipMouseConfiguration != null)
            {
                m_Velocity.X = InputManager.MousePositionDelta.X * 60;
                if (InputManager.ButtonPressed(r_SpaceShipConfiguration.SpaceShipMouseConfiguration.ShootButton))
                {
                    ShootIfPossible();
                }
            }
        }

        private void handleInputs()
        {
            if (r_SpaceShipConfiguration.SpaceShipKeyboardConfiguration != null)
            {
                if (InputManager.KeyboardState.IsKeyDown(r_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.LeftMoveButton))
                {
                    m_Velocity.X = k_Velocity*-1;
                }
                else if (InputManager.KeyboardState.IsKeyDown(r_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.RightMoveButton))
                {
                    m_Velocity.X = k_Velocity;
                }
                else
                {
                    m_Velocity.X = 0;
                    handleMouse();
                }

                if (isKeyPressed(r_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.ShootButton))
                {
                    ShootIfPossible();
                }
            }
        }

        private bool isKeyPressed(IEnumerable<Keys> i_Keys)
        {
            return i_Keys.Any(i_Key => InputManager.KeyPressed(i_Key));
        }

        protected void ShootIfPossible()
        {
            r_Bullets.RemoveAll(i_Bullet => !i_Bullet.IsAlive);
            if (r_Bullets.Count < k_MaxAmountOfBulletsInBackground)
            {
                m_SoundManager.PlaySoundEffect(Sounds.k_SsGunShot);
                
                Bullet bullet = new Bullet(Screen);
                bullet.Initialize();
                bullet.Position = new Vector2(Bounds.Left + Bounds.Width/2 - bullet.Width/2,Bounds.Top - bullet.Height);
                bullet.Velocity = new Vector2(0, -k_BulletVelocity);
                bullet.CollisionDetected += onBulletCollided;
                r_Bullets.Add(bullet);
            }
        }

        private void onBulletCollided(object i_Sender, EventArgs i_EventArgs)
        {
            if (BulletCollided != null)
            {
                BulletCollided.Invoke(i_Sender, i_EventArgs);
            }
        }

        protected override void Dispose(bool i_Disposing)
        {
            if (i_Disposing)
            {
                foreach (Bullet bullet in r_Bullets)
                {
                    bullet.Dispose();
                }
            }

            base.Dispose(i_Disposing);
        }
    }
}