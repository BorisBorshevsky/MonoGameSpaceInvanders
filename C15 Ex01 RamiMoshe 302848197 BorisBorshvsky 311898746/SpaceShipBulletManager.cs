using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;

namespace SpaceInvaders
{
    class SpaceShipBulletManager : RegisteredComponent
    {
        List<Bullet> m_Bullets = new List<Bullet>();
        //todo: 2
        private int capacity = 200;
        private CollisionDetector m_CollisionDetector;



        public override void Initialize()
        {           
            base.Initialize();
            m_CollisionDetector = Game.Services.GetService(typeof(CollisionDetector)) as CollisionDetector;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_Bullets.RemoveAll(i_Bullet => !i_Bullet.IsAlive);
        }

        public void ShootIfPossible(Rectangle SpaceShipRectangle, Vector2 velovity)
        {
            if (m_Bullets.Count < capacity)
            {
                Shoot(SpaceShipRectangle, velovity);
            }
        }

        private void Shoot(Rectangle SpaceShipRectangle, Vector2 velovity)
        {
            SpaceShipBullet bullet = new SpaceShipBullet(this.Game);
            bullet.Initialize();
            bullet.Position = new Vector2(SpaceShipRectangle.X + SpaceShipRectangle.Width / 2 - bullet.Width / 2, SpaceShipRectangle.Y - bullet.Height); ;
            bullet.Velocity = velovity;
            m_CollisionDetector.Add(bullet);
            m_Bullets.Add(bullet);
        }


        public SpaceShipBulletManager(Game i_Game)
            : base(i_Game)
        {
        }
    }
}
