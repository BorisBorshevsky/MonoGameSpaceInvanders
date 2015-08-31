using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.Menu
{
    public interface IMenuItem
    {
        string Title { get; }

        string TitleValue { get; }

        void EnterScreen(GameScreen i_GameScreen);

        string ItemSelected(GameScreen i_GameScreen, Keys i_Key);
    }
}
