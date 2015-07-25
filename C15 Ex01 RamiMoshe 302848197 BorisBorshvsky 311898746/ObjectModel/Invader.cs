using System;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    public class Invader : Sprite
    {
        private const string k_AssteName = @"Sprites\Enemy0101_32x32";
        const float k_BulletVelocity = 115f;

        static Random random = new Random();
        private CollisionDetector m_CollisionDetector;

        public Invader(Game i_Game, Color i_EnemyColor)
            : base(k_AssteName, i_Game)
        {
            m_TintColor = i_EnemyColor;
        }

        public override void Initialize()
        {
            m_CollisionDetector = Game.Services.GetService(typeof (CollisionDetector)) as CollisionDetector;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (random.NextDouble() < 0.001)
            {
                Shoot();
            }
            
            base.Update(gameTime);
        }

        protected void Shoot()
        {
            InvaderBullet bullet = new InvaderBullet(this.Game);
            bullet.Initialize();
            bullet.TintColor = Color.Gold;
            bullet.Position = new Vector2(m_Position.X + m_Width / 2 - bullet.Width / 2, m_Position.Y + Height);
            bullet.Velocity = new Vector2(0, k_BulletVelocity);
            m_CollisionDetector.Add(bullet);

            //m_CollisionDetector.AddBullet(bullet);
        }
    }
 }
