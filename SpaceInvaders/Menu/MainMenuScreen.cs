using Infrastructure.Menu;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu.MainMenuItems;

namespace SpaceInvaders.Menu
{
    class MainMenuScreen : SpaceInvadersMenuScreen
    {
        private readonly PlayItem r_PlayItem;
        private readonly SoundOptionsItem r_SoundOptionsItem;
        private readonly ChoosePlayersItem r_ChoosePlayersItem;
        private readonly ScreenOptionsItem r_ScreenOptionsItem;
        private readonly QuitGameItem r_QuitItem;

        public MainMenuScreen(Game i_Game, IMenuConfiguration i_MenuConfiguration)
            : base(i_Game, "Main Menu", i_MenuConfiguration)
        {
            r_ScreenOptionsItem = new ScreenOptionsItem("Screen Options", this, i_MenuConfiguration);
            AddMenuItem(r_ScreenOptionsItem);

            r_ChoosePlayersItem = new ChoosePlayersItem("Players: ", this, i_MenuConfiguration);
            r_ChoosePlayersItem.TitleValue = r_SettingsManager.NumOfPlayers == 1 ? "One" : "Two";
            AddMenuItem(r_ChoosePlayersItem);

            r_SoundOptionsItem = new SoundOptionsItem("Sound Options", this, i_MenuConfiguration);
            AddMenuItem(r_SoundOptionsItem);

            r_PlayItem = new PlayItem("Play", this, i_MenuConfiguration);
            AddMenuItem(r_PlayItem);

            r_QuitItem = new QuitGameItem("Quit", this, i_MenuConfiguration);
            AddMenuItem(r_QuitItem);
        }
    }
}
