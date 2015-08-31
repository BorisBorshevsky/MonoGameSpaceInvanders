using System.Collections.Generic;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.Settings
{
    interface ISettingsManager
    {
        List<IPlayerState> PlayersData { get; }

        int GameLevel { get; }
        void IncrementLevel();

        int NumOfPlayers { get; }

        bool AllowWindowResizing { get; set; }

        bool FullScreenMode { get;}
        void ToggleFullScreen();

        bool IsMouseVisible { get;}
        void ToggleMouseVisibility();

        int InitialLivesPerPlayer { get; }
        void ResetLives();
        void ResetScores();

        void ResetGameSettings();
       
        GameLevelSettings GetGameLevelSettings();
    }
}
