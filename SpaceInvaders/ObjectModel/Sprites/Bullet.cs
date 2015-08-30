using System;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel.Sprites
{
    public class Bullet : PixelSensitiveSprite, ICollidablePixelBased
    {
        private const string k_AssstName = @"Sprites\Bullet";
        private const int k_ChanceForSpaceShipBulletToBeDisposedAfterBulletsCollision = 33;

        private readonly Random r_Random = new Random();
        
        public Bullet(GameScreen i_GameScreen)
            : base(k_AssstName, i_GameScreen)
        { }

        public event EventHandler<EventArgs> CollisionDetected; 

        public override void Collided(ICollidable i_Collidable)
        {
            SpaceShip spaceShip = i_Collidable as SpaceShip;
            if (spaceShip != null)
            {
                if (Velocity.Y > 0)
                {
                    onCollisionDetected(i_Collidable);
                }
            }

            Invader invader = i_Collidable as Invader;
            if (invader != null)
            {
                if (Velocity.Y < 0 && !invader.IsDying)
                {
                    onCollisionDetected(i_Collidable);
                }
            }

            MotherShip motherShip = i_Collidable as MotherShip;
            if (motherShip != null)
            {
                if (Velocity.Y < 0 && !motherShip.IsDying)
                {
                    onCollisionDetected(i_Collidable);
                }
            }
            
            Barrier barrier = i_Collidable as Barrier;
            if (barrier != null)
            {
                onCollisionDetected(i_Collidable);
            }

            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                // im spaceship bullet
                if (Velocity.Y < 0 && bullet.Velocity.Y > 0)
                {
                    onCollisionDetected(i_Collidable);
                }

                //im invader bullet
                if (Velocity.Y > 0 && bullet.Velocity.Y < 0)
                {
                    if (r_Random.Next(0, 100) < k_ChanceForSpaceShipBulletToBeDisposedAfterBulletsCollision)
                    {
                        onCollisionDetected(i_Collidable);
                    }
                }
            }
        }

        private void onCollisionDetected(ICollidable i_Collidable)
        {
            if (CollisionDetected != null)
            {
                CollisionDetected.Invoke(i_Collidable, EventArgs.Empty);
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