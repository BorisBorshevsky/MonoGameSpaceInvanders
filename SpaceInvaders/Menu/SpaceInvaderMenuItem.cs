using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Settings;

namespace SpaceInvaders.Menu
{
    class SpaceInvaderMenuItem : MenuItem
    {
        protected readonly ISettingsManager r_SettingsManager;

        public SpaceInvaderMenuItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        {
            r_SettingsManager = i_GameScreen.Game.Services.GetService<ISettingsManager>();
        }
    }
}
