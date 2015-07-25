using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace Infrastructure.Managers
{
    public class SpriteCollisionCoordinator : GameService
    {
        //SortedList<Sprite, List<Sprite>>  spritelist = new SortedList<>; 

        
        public SpriteCollisionCoordinator(Game i_Game) : base(i_Game)
        {
        
        }
    }
}