using System.Collections.Generic;
using SpaceInvaders.ObjectModel;

namespace SpaceInvaders.Services
{
    public interface IGameStateService
    {
        void GameOver(List<Player> players, string i_Msg = "Game Over");
    }
}