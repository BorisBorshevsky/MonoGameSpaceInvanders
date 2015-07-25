using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;

namespace SpaceInvaders
{
    class MotherShipManager : RegisteredComponent
    {
        private MotherShip motherShip;
        Random Random = new Random();
        private CollisionDetector collisionDetector;

        public MotherShipManager(Game game) : base(game)
        {
            
        }
        
        public override void Initialize()
        {
            collisionDetector = Game.Services.GetService(typeof(CollisionDetector)) as CollisionDetector;
            base.Initialize();
        }


        
        public override void Update(GameTime gameTime)
        {
            


            if (motherShip == null || !motherShip.IsAlive)
            {
                if (Random.NextDouble() < 0.1)
                {
                    motherShip = new MotherShip(Game);
                    collisionDetector.Add(motherShip);
                }

            }
            else
            {
                if (motherShip.IsOutOfBounts())
                {
                    motherShip.Remove();
                }
            }
            base.Update(gameTime);

        }

    }
}
