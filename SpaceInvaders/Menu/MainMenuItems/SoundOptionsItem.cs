using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class SoundOptionsItem : SpaceInvaderMenuItem
    {
        public SoundOptionsItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new SoundOptionsMenu(i_GameScreen.Game, r_MenuConfiguration));
        }
    }
}
