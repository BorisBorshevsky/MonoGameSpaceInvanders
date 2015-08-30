using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.ObjectModel.Managers
{
    public interface ISettingsManager
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

        int InitialeLivesPerPlayer { get; }
        void ResetLives();
        void ResetScores();

        void ResetGameSettings();
       

        GameLevelSettings GetGameLevelSettings();
    }
}
