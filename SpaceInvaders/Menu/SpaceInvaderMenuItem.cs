using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.Menu
{
    class SpaceInvaderMenuItem : MenuItem
    {
        protected readonly ISettingsManager r_SettingsManager;

        public SpaceInvaderMenuItem(string i_Title, GameScreen i_GameScreen) : base(i_Title, i_GameScreen)
        {
            r_SettingsManager = i_GameScreen.Game.Services.GetService<ISettingsManager>();
        }
    }
}
