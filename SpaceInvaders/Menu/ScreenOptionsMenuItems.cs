using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.Menu
{
    class WindowResizingItem : MenuItem
    {
        public WindowResizingItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager settingsManager = i_GameScreen.Game.Services.GetService<ISettingsManager>();

            bool allowResizing = !settingsManager.AllowWindowResizing;
            settingsManager.AllowWindowResizing = allowResizing;
            return allowResizing ? "On" : "Off";
        }
    }

    class FullScreenModeItem : MenuItem
    {
        public FullScreenModeItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager settingsManager = i_GameScreen.Game.Services.GetService<ISettingsManager>();

            settingsManager.ToggleFullScreen();
            return settingsManager.FullScreenMode ? "On" : "Off";
        }
    }

    class MouseVisibilityItem : MenuItem
    {
        public MouseVisibilityItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager settingsManager = i_GameScreen.Game.Services.GetService<ISettingsManager>();

            settingsManager.ToggleMouseVisibility(); 
            return settingsManager.IsMouseVisible ? "Visible" : "Invisible";
        }
    }
}