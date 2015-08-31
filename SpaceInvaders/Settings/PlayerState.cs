
namespace SpaceInvaders.Settings
{
    class PlayerState : IPlayerState
    {
        public PlayerState(int i_Lives)
        {
            Lives = i_Lives;
            Enabled = true;
        }

        public int Score { get; set; }
        public int Lives { get; set; }
        public bool Enabled { get; set; }
    }
}