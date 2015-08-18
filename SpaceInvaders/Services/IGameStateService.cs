namespace SpaceInvaders.Services
{
    public interface IGameStateService
    {
        void GameOver(string i_Msg = "Game Over");
        void AddToScore(int i_Score);
    }
}