using Infrastructure.Common;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    public class Bullet : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\Bullet";

        public Bullet(Game i_Game)
            : base(k_AssteName, i_Game)
        {
        }

        public override void Collided(ICollidable2D i_Collidable)
        {
            SpaceShip spaceShip = i_Collidable as SpaceShip;
            if (spaceShip != null)
            {
                if (Velocity.Y > 0)
                {
                    Remove();
                    Dispose();
                }
            }

            Invader invader = i_Collidable as Invader;
            if (invader != null)
            {
                if (Velocity.Y < 0)
                {
                    Remove();
                    Dispose();
                }
            }

            MotherShip motherShip = i_Collidable as MotherShip;
            if (motherShip != null)
            {
                if (Velocity.Y < 0)
                {
                    Remove();
                    Dispose();
                }
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            if (IsOutOfBounts())
            {
                Remove();
            }

            base.Update(i_GameTime);
        }
    }
}