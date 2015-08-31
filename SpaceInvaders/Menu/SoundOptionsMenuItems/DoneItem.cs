using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Menu.SoundOptionsMenuItems
{
    class DoneItem : SpaceInvaderMenuItem
    {
        public DoneItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ExitScreen();
        }
    }
}
