using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Animators;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class SpaceShip : Sprite, ICollidable2D
    {
        private const int k_MaxAmountOfBulletsSimultaniously = 2;
        private const float k_Velocity = 150f;
        private const float k_BulletVelocity = 115f;
        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        
        private readonly SpaceShipConfiguration m_SpaceShipConfiguration;
        private readonly int r_InitialOffsetMultiplayer;
        private bool isHitable = true;

        public event EventHandler<EventArgs> OnBulletCollision;
        public event EventHandler<EventArgs> OnSpaceShipHit;
        public event EventHandler<EventArgs> OnDie;

        public IInputManager InputManager { get; set; }

        public SpaceShip(Game i_Game, SpaceShipConfiguration i_SpaceShipConfiguration, int i_Id)
            : base(i_SpaceShipConfiguration.AssteName, i_Game)
        {
            m_SpaceShipConfiguration = i_SpaceShipConfiguration;
            r_InitialOffsetMultiplayer = i_Id;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (isHitable)
            {
                Bullet bullet = i_Collidable as Bullet;
                if (bullet != null)
                {
                    if (bullet.Velocity.Y > 0)
                    {
                        if (OnSpaceShipHit != null)
                        {
                            OnSpaceShipHit(this, EventArgs.Empty);
                        }
                    }
                }

                Invader invader = i_Collidable as Invader;
                if (invader != null)
                {
                    //m_GameStateManager.GameOver();
                    if (OnDie != null)
                    {
                        OnDie(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void StartHitAnimation()
        {
            isHitable = false;
            Animations["Hit"].Restart();
        }

        public void StartDieAnimation()
        {
            isHitable = false;
            Animations["Die"].Restart();
        }

        public override void Initialize()
        {
            base.Initialize();
            InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
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
            dieAnimator.Finished += onDieAnimatorFinished;

            Animations.Add(dieAnimator);

            Animations.Enabled = true;
            dieAnimator.Enabled = false;
            hitAnimation.Enabled = false;
        }

        private void onHitAnimationFinished(object i_Sender, EventArgs i_EventArgs)
        {
            isHitable = true;
        }

        private void onDieAnimatorFinished(object i_Sender, EventArgs i_EventArgs)
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
            Position = new Vector2(r_InitialOffsetMultiplayer * Bounds.Width, GraphicsDevice.Viewport.Height - 30 - Bounds.Height / 2);
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
            if (m_SpaceShipConfiguration.SpaceShipMouseConfiguration != null)
            {
                m_Velocity.X = InputManager.MousePositionDelta.X * 60;
                if (InputManager.ButtonPressed(m_SpaceShipConfiguration.SpaceShipMouseConfiguration.ShootButton))
                {
                    ShootIfPossible();
                }
            }
        }

        private void handleInputs()
        {
            if (m_SpaceShipConfiguration.SpaceShipKeyboardConfiguration != null)
            {
                if (InputManager.KeyboardState.IsKeyDown(m_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.LeftMoveButton))
                {
                    m_Velocity.X = k_Velocity*-1;
                }
                else if (InputManager.KeyboardState.IsKeyDown(m_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.RightMoveButton))
                {
                    m_Velocity.X = k_Velocity;
                }
                else
                {
                    m_Velocity.X = 0;
                    handleMouse();
                }

                if (isKeyPressed(m_SpaceShipConfiguration.SpaceShipKeyboardConfiguration.ShootButton))
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
            if (r_Bullets.Count < k_MaxAmountOfBulletsSimultaniously)
            {
                Bullet bullet = new Bullet(Game);
                bullet.Initialize();
                bullet.Position = new Vector2(Bounds.Left + Bounds.Width/2 - bullet.Width/2,Bounds.Top - bullet.Height);
                bullet.Velocity = new Vector2(0, -k_BulletVelocity);
                bullet.OnCollision += bulletCollision;
                bullet.Disposed -= bulletCollision;
                r_Bullets.Add(bullet);
            }
        }

        private void bulletCollision(object i_Sender, EventArgs i_E)
        {
            if (OnBulletCollision != null)
            {
                OnBulletCollision.Invoke(i_Sender, i_E);
            }
        }
    }
}