using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class PlayItem : SpaceInvaderMenuItem
    {
        public PlayItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new LevelTransitionScreen(i_GameScreen.Game));
        }
    }
}
