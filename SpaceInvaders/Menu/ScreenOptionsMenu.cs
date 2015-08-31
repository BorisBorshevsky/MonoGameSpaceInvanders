using Infrastructure.Menu;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu.ScreenOptionsMenuItems;

namespace SpaceInvaders.Menu
{
    class ScreenOptionsMenu : SpaceInvadersMenuScreen
    {
        private readonly WindowResizingItem r_WindowResizingItem;
        private readonly FullScreenModeItem r_FullScreenModeItem;
        private readonly MouseVisibilityItem r_MouseVisibilityItem;
        private readonly BackItem r_BackItem;

        public ScreenOptionsMenu(Game i_Game, IMenuConfiguration i_MenuConfiguration)
            : base(i_Game, "Screen Options", i_MenuConfiguration)
        {
            r_MouseVisibilityItem = new MouseVisibilityItem("Mouse Visibility: ", this, i_MenuConfiguration);
            r_MouseVisibilityItem.TitleValue = r_SettingsManager.IsMouseVisible ? "Visible" : "Invisible";
            AddMenuItem(r_MouseVisibilityItem);

            r_FullScreenModeItem = new FullScreenModeItem("Full Screen Mode: ", this, i_MenuConfiguration);
            r_FullScreenModeItem.TitleValue = r_SettingsManager.FullScreenMode ? "On" : "Off";
            AddMenuItem(r_FullScreenModeItem);

            r_WindowResizingItem = new WindowResizingItem("Allow Window Resizing: ", this, i_MenuConfiguration);
            r_WindowResizingItem.TitleValue = (r_SettingsManager.AllowWindowResizing) ? "On" : "Off";
            AddMenuItem(r_WindowResizingItem);

            r_BackItem = new BackItem("Done", this, i_MenuConfiguration);
            AddMenuItem(r_BackItem);
        }
    }
}