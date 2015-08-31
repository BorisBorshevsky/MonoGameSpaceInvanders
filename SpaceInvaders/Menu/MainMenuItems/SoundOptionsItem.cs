using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class SoundOptionsItem : SpaceInvaderMenuItem
    {
        public SoundOptionsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new SoundOptionsMenu(i_GameScreen.Game));
        }
    }
}
