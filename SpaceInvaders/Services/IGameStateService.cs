using SpaceInvaders.ObjectModel;

namespace SpaceInvaders.Services
{
    public interface IGameStateService
    {
        void GameOver(string i_Msg = "Game Over");

        void AddPlayer(Player i_Player);
    }
}