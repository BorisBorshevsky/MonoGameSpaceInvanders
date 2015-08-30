using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Managers;


namespace SpaceInvaders
{
    public class WindowResizingItem : MenuItem
    {
        public WindowResizingItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            SettingsManager settingsManager = (SettingsManager)i_GameScreen.Game.Services.GetService(typeof(ISettingsManager));
            bool allowResizing = !settingsManager.AllowWindowResizing;
            settingsManager.AllowWindowResizing = allowResizing;
            return allowResizing ? "On" : "Off";
        }
    }

    public class FullScreenModeItem : MenuItem
    {
        public FullScreenModeItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager settingsManager = (ISettingsManager)i_GameScreen.Game.Services.GetService(typeof(ISettingsManager));
            settingsManager.ToggleFullScreen();
            return settingsManager.FullScreenMode ? "On" : "Off";
        }
    }

    public class MouseVisabilityItem : MenuItem
    {
        public MouseVisabilityItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager settingsManager = (ISettingsManager)i_GameScreen.Game.Services.GetService(typeof(ISettingsManager));

            settingsManager.ToggleMouseVisibility(); 
            return settingsManager.IsMouseVisible ? "Visible" : "Invisible";
        }
    }
}
