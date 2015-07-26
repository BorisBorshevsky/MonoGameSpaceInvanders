using System;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    public abstract class Invader : Sprite
    {
        
        const float k_BulletVelocity = 115f;

        static Random random = new Random();
        private CollisionDetector m_CollisionDetector;
        private GameStateManagerService m_GameStateManagerService
            
            ;

        public Invader(Game i_Game, String i_AssteName, Color i_EnemyColor)
            : base(i_AssteName, i_Game)
        {
            m_TintColor = i_EnemyColor;
        }


        public override void Initialize()
        {
            m_CollisionDetector = Game.Services.GetService(typeof (CollisionDetector)) as CollisionDetector;
            m_GameStateManagerService = Game.Services.GetService(typeof(GameStateManagerService)) as GameStateManagerService;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (BoundingRect.Bottom > Game.GraphicsDevice.Viewport.Bounds.Bottom)
            {
                m_GameStateManagerService.GameOver();
            }

            //todo: depend to gametime
            if (random.NextDouble() < 0.001)
            {
                Shoot();
            }
            
            base.Update(gameTime);
        }

        protected void Shoot()
        {
            //todo: extract to bulletmanager;
            InvaderBullet bullet = new InvaderBullet(this.Game);
            bullet.Initialize();
            bullet.TintColor = Color.Gold;
            bullet.Position = new Vector2(m_Position.X + m_Width / 2 - bullet.Width / 2, m_Position.Y + Height);
            bullet.Velocity = new Vector2(0, k_BulletVelocity);
            m_CollisionDetector.Add(bullet);
        }

                



        public abstract int Score { get; }
    }
 }
