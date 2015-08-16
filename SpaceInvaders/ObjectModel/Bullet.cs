
using System;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    public class Bullet : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\Bullet";

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
                if (Velocity.Y < 0)
                {
                    collisionDetected(i_Collidable);
                }
            }

            MotherShip motherShip = i_Collidable as MotherShip;
            if (motherShip != null)
            {
                if (Velocity.Y < 0)
                {
                    collisionDetected(i_Collidable);
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