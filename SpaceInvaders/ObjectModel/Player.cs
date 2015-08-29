using System;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.ObjectModel
{
    public class Player : IDisposable
    {

        private readonly SpaceShip r_SpaceShip;
        private const int k_LoosingLifeScorePanalty = -1000;
        private readonly SoulsBoard r_SoulsBoard;

        public ScoresBoard ScoresBoard { get; private set; }
        public event EventHandler<EventArgs> PlayerLost;

        public Player(GameScreen i_GameScreen, SpaceShipConfiguration i_SpaceShipConfiguration, int i_PlayerId, IPlayerState IPlayerState)
        {
            r_SpaceShip = new SpaceShip(i_GameScreen, i_SpaceShipConfiguration, i_PlayerId);
            ScoresBoard = new ScoresBoard(i_GameScreen, i_PlayerId, i_SpaceShipConfiguration.TextColor);
            r_SoulsBoard = new SoulsBoard(i_GameScreen, IPlayerState, i_SpaceShipConfiguration.AssteName, i_PlayerId);
            r_SpaceShip.SpaceShipHit += spaceShipOnHit;
            r_SpaceShip.Died += spaceShipOnDie;
            r_SpaceShip.BulletCollided += bulletCollision;
        }

        private void bulletCollision(object i_Sender, EventArgs i_E)
        {
            Invader invader = i_Sender as Invader;
            if (invader != null)
            {
                ScoresBoard.AddScore(invader.Score);
            }
            MotherShip motherShip = i_Sender as MotherShip;
            if (motherShip != null)
            {
                ScoresBoard.AddScore(motherShip.Score);
            }
        }

        private void spaceShipOnDie(object i_Sender, EventArgs i_EventArgs)
        {
            r_SpaceShip.InputManager = new DummyInputManager();
            r_SpaceShip.DieAnimationFinished += onPlayerLost; 
            r_SpaceShip.StartDieAnimation();
            r_SpaceShip.SpaceShipHit -= spaceShipOnHit;
            r_SpaceShip.BulletCollided -= bulletCollision;
        }

        private void onPlayerLost(object i_Sender, EventArgs i_EventArgs)
        {
            if (PlayerLost != null)
            {
                PlayerLost.Invoke(i_Sender, i_EventArgs);
            }

        }

        private void spaceShipOnHit(object i_Sender, EventArgs i_EventArgs)
        {
            ScoresBoard.AddScore(k_LoosingLifeScorePanalty);
            r_SoulsBoard.RemoveSoul();
            if (r_SoulsBoard.PlayerState.Lives == 0)
            {
                spaceShipOnDie(i_Sender, i_EventArgs);
            }
            else
            {
                r_SpaceShip.StartHitAnimation();
                r_SpaceShip.ResetPosition();
            }
        }

        public void Dispose()
        {
            r_SpaceShip.Dispose();
            r_SoulsBoard.Dispose();
            ScoresBoard.Dispose();
        }
    }
}
