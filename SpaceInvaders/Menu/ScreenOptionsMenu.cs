using Microsoft.Xna.Framework;
using SpaceInvaders.Menu.ScreenOptionsMenuItems;
using SpaceInvaders.Menu.SoundOptionsMenuItems;

namespace SpaceInvaders.Menu
{
    class ScreenOptionsMenu : MenuScreen
    {
        private readonly WindowResizingItem r_WindowResizingItem;
        private readonly FullScreenModeItem r_FullScreenModeItem;
        private readonly MouseVisibilityItem r_MouseVisibilityItem;
        private readonly DoneItem r_DoneItem;

        public ScreenOptionsMenu(Game i_Game)
            : base(i_Game, "Screen Options")
        {
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