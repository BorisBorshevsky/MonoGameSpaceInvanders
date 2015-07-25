using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;

namespace SpaceInvaders
{
    class CollisionDetector : GameService
    {
        public List<Bullet> SpaceShipBullets { get; set; }
        public List<Bullet> InvaderBullets { get; set; } 
        public MotherShip MotherShip { get; set; }
        public SpaceShip SpaceShip { get; set; }


        public InvaderGrid InvaderGrid { get; set; }

        public CollisionDetector(Game game) : base(game)
        {
            SpaceShipBullets = new List<Bullet>();
            InvaderBullets = new List<Bullet>();
        }

        public override void Update(GameTime gameTime)
        {
            OnEnemyBulletHitsSpaceShip();
            OnSpaceShipBulletHitsEnemy();
            OnSpaceShipBulletHitsMotherShip();
            //Detect Collision
            base.Update(gameTime);
        }

        private void OnEnemyBulletHitsSpaceShip()
        {
            foreach (var bullet in InvaderBullets)
            {
                if (bullet.IsAlive)
                {
                    if (bullet.BoundingRect.Intersects(SpaceShip.BoundingRect))
                    {
                        bullet.Remove();
                        SpaceShip.NotifyOnHit();
                        SpaceShipBullets.Remove(bullet);
                        

                        break;
                        //AddScore();
                    }
                }
            }
        }


        private void OnSpaceShipBulletHitsEnemy()
        {
            foreach (var invader in InvaderGrid.Invades)
            {
                if (invader.IsAlive)
                {
                    foreach (var bullet in SpaceShipBullets)
                    {
                        if (bullet.BoundingRect.Intersects(invader.BoundingRect))
                        {
                            bullet.Remove();
                            invader.Remove();
                            SpaceShipBullets.Remove(bullet);
                            InvaderGrid.NotifyOnDeadInvader();
                            
                            break;
                            //AddScore();
                        }
                    }
                }
            }
        }
        private void OnSpaceShipBulletHitsMotherShip()
        {

            if (MotherShip != null && MotherShip.IsAlive)
            { 
                foreach (var bullet in SpaceShipBullets)
                {
                    if (bullet.BoundingRect.Intersects(MotherShip.BoundingRect))
                    {
                        bullet.Remove();
                        MotherShip.Remove();
                        SpaceShipBullets.Remove(bullet);
                        break;
                        //AddScore();
                    }
                }
            }
        }



        public void Add(InvaderGrid enemyMatrix)
        {
            InvaderGrid = enemyMatrix;
        }

        public void Add(SpaceShipBullet bullet)
        {
            SpaceShipBullets.Add(bullet);
        }

        public void Add(InvaderBullet bullet)
        {
            InvaderBullets.Add(bullet);
        }

        public void Add(MotherShip MotherShip)
        {
            this.MotherShip = MotherShip;
        }

        public void Add(SpaceShip SpaceShip)
        {
            this.SpaceShip = SpaceShip;
        }

    }
}
