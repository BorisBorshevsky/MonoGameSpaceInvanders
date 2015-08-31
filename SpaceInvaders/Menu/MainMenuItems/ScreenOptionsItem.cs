using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class ScreenOptionsItem : SpaceInvaderMenuItem
    {
        public ScreenOptionsItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new ScreenOptionsMenu(i_GameScreen.Game, r_MenuConfiguration));
        }
    }
}
