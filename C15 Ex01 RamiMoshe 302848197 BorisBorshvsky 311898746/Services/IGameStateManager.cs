namespace SpaceInvaders.Services
{
    public interface IGameStateManager
    {
        void GameOver(string i_Msg = "Game Over");
        void LoseLife();
        void AddToScore(int i_Score);
    }
}