using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class GameLogic 
    {
        public Game Game { get; set; }

        public GameLogic(Game i_Game)
        {
            Game = i_Game;
        }


    }
}
