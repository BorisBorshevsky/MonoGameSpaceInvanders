using Infrastructure.ObjectModel.Screens;

namespace Infrastructure.Menu
{
    public class BackItem : MenuItem
    {
        public BackItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ExitScreen();
        }
    }
}
