using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.ScreenOptionsMenuItems
{
    class MouseVisibilityItem : SpaceInvaderMenuItem
    {
        public MouseVisibilityItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            r_SettingsManager.ToggleMouseVisibility();
            return r_SettingsManager.IsMouseVisible ? "Visible" : "Invisible";
        }
    }
}
