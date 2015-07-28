using System.Collections.Generic;
using Infrastructure.Common;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class SpaceShip : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\Ship01_32x32";
        private const int k_MaxAmountOfBulletsSimultaniously = 2;
        private const float k_Velocity = 115f;
        private const float k_BulletVelocity = 115f;
        private readonly List<Bullet> r_Bullets = new List<Bullet>();
        private IGameStateManager m_GameStateManager;
        private IInputManager m_InputManager;

        public SpaceShip(SapceInvadersGame i_Game)
            : base(k_AssteName, i_Game)
        {
        }

        public override void Collided(ICollidable2D i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y > 0)
                {
                    m_GameStateManager.LoseLife();
                    InitBounds();
                }
            }

            Invader invader = i_Collidable as Invader;
            if (invader != null)
            {
                m_GameStateManager.GameOver();
            }
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof (IInputManager)) as IInputManager;
            m_GameStateManager = Game.Services.GetService(typeof (IGameStateManager)) as IGameStateManager;
            base.Initialize();
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            Position = new Vector2(0, GraphicsDevice.Viewport.Height - 30 - m_Height/2);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Left))
            {
                m_Velocity.X = k_Velocity*-1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Right))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = m_InputManager.MousePositionDelta.X*60;
            }

            m_Position.X = MathHelper.Clamp(m_Position.X, 0, Game.GraphicsDevice.Viewport.Width - m_Width);

            if (m_InputManager.KeyPressed(Keys.Enter) || m_InputManager.ButtonPressed(eInputButtons.Left))
            {
                ShootIfPossible();
            }
        }

        protected void ShootIfPossible()
        {
            r_Bullets.RemoveAll(i_Bullet => !i_Bullet.IsAlive);
            if (r_Bullets.Count < k_MaxAmountOfBulletsSimultaniously)
            {
                Bullet bullet = new Bullet(Game);
                bullet.Initialize();
                bullet.Position = new Vector2(BoundingRect.Left + BoundingRect.Width/2 - bullet.Width/2,
                    BoundingRect.Top - bullet.Height);
                bullet.Velocity = new Vector2(0, -k_BulletVelocity);
                r_Bullets.Add(bullet);
            }
        }
    }
}