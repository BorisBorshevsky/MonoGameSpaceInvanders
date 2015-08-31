using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.ScreenOptionsMenuItems
{
    class FullScreenModeItem : SpaceInvaderMenuItem
    {
        public FullScreenModeItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            r_SettingsManager.ToggleFullScreen();
            return r_SettingsManager.FullScreenMode ? "On" : "Off";
        }
    }
}
