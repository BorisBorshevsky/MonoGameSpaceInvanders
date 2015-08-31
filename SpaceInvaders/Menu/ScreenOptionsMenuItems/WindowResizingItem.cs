using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.ScreenOptionsMenuItems
{
    class WindowResizingItem : SpaceInvaderMenuItem
    {
        public WindowResizingItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            bool allowResizing = !r_SettingsManager.AllowWindowResizing;
            r_SettingsManager.AllowWindowResizing = allowResizing;
            return allowResizing ? "On" : "Off";
        }
    }
}
