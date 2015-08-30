
namespace SpaceInvaders.ObjectModel.Managers
{
    interface IPlayerState
    {
        int Score { get; set; }

        int Lives { get; set; }

        bool Enabled { get; set; }
    }
}