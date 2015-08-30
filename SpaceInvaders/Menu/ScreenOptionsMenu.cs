using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders
{
    public class ScreenOptionsMenu : MenuScreen
    {
        private readonly WindowResizingItem r_WindowResizingItem;
        private readonly FullScreenModeItem r_FullScreenModeItem;
        private readonly MouseVisibilityItem r_MouseVisibilityItem;
        private readonly DoneItem r_DoneItem;
        private readonly ISettingsManager r_SettingsManager;

        public ScreenOptionsMenu(Game i_Game)
            : base(i_Game, "Screen Options")
        {
            r_SettingsManager = i_Game.Services.GetService<ISettingsManager>();

            r_MouseVisibilityItem = new MouseVisibilityItem("Mouse Visibility: ", this);
            r_MouseVisibilityItem.TitleValue = r_SettingsManager.IsMouseVisible ? "Visible" : "Invisible";
            AddMenuItem(r_MouseVisibilityItem);

            r_FullScreenModeItem = new FullScreenModeItem("Full Screen Mode: ", this);
            r_FullScreenModeItem.TitleValue = r_SettingsManager.FullScreenMode ? "On" : "Off";
            AddMenuItem(r_FullScreenModeItem);

            r_WindowResizingItem = new WindowResizingItem("Allow Window Resizing: ", this);
            r_WindowResizingItem.TitleValue = (r_SettingsManager.AllowWindowResizing) ? "On" : "Off";
            AddMenuItem(r_WindowResizingItem);

            r_DoneItem = new DoneItem("Done", this);
            AddMenuItem(r_DoneItem);
        }
    }
}