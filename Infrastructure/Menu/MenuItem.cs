using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.Menu
{
    public class MenuItem : IMenuItem
    {
        protected readonly IMenuConfiguration r_MenuConfiguration;
        public MenuItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
        {
            TitleValue = string.Empty;
            Title = i_Title;
            GameScreen = i_GameScreen;
            r_MenuConfiguration= i_MenuConfiguration;
        }

        public string Title { get; private set; }

        public string TitleValue { get; set; }

        public virtual void EnterScreen(GameScreen i_GameScreen) 
        { 
        }

        public virtual string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            return string.Empty;
        }

        public GameScreen GameScreen { get; private set; }
    }
}