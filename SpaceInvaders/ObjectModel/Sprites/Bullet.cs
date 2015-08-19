
using System;
using System.Threading;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    public class Bullet : PixelSensetiveSprite, ICollidablePixelBased
    {
        private const string k_AssteName = @"Sprites\Bullet";
        private readonly Random r_Random = new Random();
        private const int k_ChanceForSpaceShipBulletToBeDisposedAfterBulletsCollision = 33;
        public Bullet(Game i_Game)
            : base(k_AssteName, i_Game)
        {}

        public event EventHandler<EventArgs> OnCollision; 

        public override void Collided(ICollidable i_Collidable)
        {
            SpaceShip spaceShip = i_Collidable as SpaceShip;
            if (spaceShip != null)
            {
                if (Velocity.Y > 0)
                {
                    collisionDetected(i_Collidable);
                }
            }

            Invader invader = i_Collidable as Invader;
            if (invader != null)
            {
                if (Velocity.Y < 0 && !invader.IsDying)
                {
                    collisionDetected(i_Collidable);
                }
            }

            MotherShip motherShip = i_Collidable as MotherShip;
            if (motherShip != null)
            {
                if (Velocity.Y < 0 && !motherShip.IsDying)
                {
                    collisionDetected(i_Collidable);
                }
            }
            Sprites.Barrier barrier = i_Collidable as Sprites.Barrier;
            if (barrier != null)
            {
                //if (Velocity.Y < 0)
                {
                    collisionDetected(i_Collidable);
                }
            }


            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                // im spaceship bullet
                if (Velocity.Y < 0 && bullet.Velocity.Y > 0)
                {
                    collisionDetected(i_Collidable);
                }

                //im invader bullet
                if (Velocity.Y > 0 && bullet.Velocity.Y < 0)
                {
                    if (r_Random.Next(0, 100) < k_ChanceForSpaceShipBulletToBeDisposedAfterBulletsCollision)
                    {
                        collisionDetected(i_Collidable);
                    }
                }

            }




        }

        private void collisionDetected(ICollidable i_Collidable)
        {
            if (OnCollision != null)
            {
                OnCollision.Invoke(i_Collidable, EventArgs.Empty);
            }
            Dispose();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (IsOutOfBounts())
            {
                Dispose();
            }

            base.Update(i_GameTime);
        }
    }
}