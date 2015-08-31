using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class ScreenOptionsItem : SpaceInvaderMenuItem
    {
        public ScreenOptionsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new ScreenOptionsMenu(i_GameScreen.Game));
        }
    }
}
