using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders.Settings;

namespace SpaceInvaders.Menu
{
    class SpaceInvadersMenuScreen : MenuScreen
    {
        protected readonly ISettingsManager r_SettingsManager;

        public SpaceInvadersMenuScreen(Game i_Game, string i_MenuTitle, IMenuConfiguration i_MenuConfiguration)
            : base(i_Game, i_MenuTitle, i_MenuConfiguration)
        {
            r_SettingsManager = Game.Services.GetService<ISettingsManager>();
        }
    }
}
