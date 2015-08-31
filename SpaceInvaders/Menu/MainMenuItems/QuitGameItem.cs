using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class QuitGameItem : SpaceInvaderMenuItem
    {
        public QuitGameItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.Game.Exit();
        }
    }
}
