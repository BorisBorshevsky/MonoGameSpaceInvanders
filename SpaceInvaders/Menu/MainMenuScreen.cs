using Infrastructure;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.Menu
{
    public class MainMenuScreen : MenuScreen
    {
        private readonly PlayItem r_PlayItem;
        private readonly SoundOptionsItem r_SoundOptionsItem;
        private readonly ChoosePlayersItem r_ChoosePlayersItem;
        private readonly ScreenOptionsItem r_ScreenOptionsItem;
        private readonly QuitGameItem r_QuitItem;
        private readonly SettingsManager r_SettingsManager;

        public MainMenuScreen(Game i_Game)
            : base(i_Game, "Main Menu")
        {
            r_SettingsManager = (SettingsManager)Game.Services.GetService(typeof(ISettingsManager));
            
            r_ScreenOptionsItem = new ScreenOptionsItem("Screen Options", this);
            AddMenuItem(r_ScreenOptionsItem);

            r_ChoosePlayersItem = new ChoosePlayersItem("Players: ", this);
            r_ChoosePlayersItem.TitleValue = r_SettingsManager.NumOfPlayers == 1 ? "One" : "Two";
            AddMenuItem(r_ChoosePlayersItem);

            r_SoundOptionsItem = new SoundOptionsItem("Sound Options", this);
            AddMenuItem(r_SoundOptionsItem);
            
            r_PlayItem = new PlayItem("Play", this);
            AddMenuItem(r_PlayItem);

            r_QuitItem = new QuitGameItem("Quit", this);
            AddMenuItem(r_QuitItem);
        }
    }
}
