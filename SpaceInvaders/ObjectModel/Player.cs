using System;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;
using Infrastructure.Managers;
using SpaceInvaders.ObjectModel.Sprites;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class Player
    {

        private readonly SpaceShip r_SpaceShip;
        private const int k_LoosingLifeScorePanalty = -1000;
        private const int k_InitialLives = 3;
        private readonly SoulsBoard r_SoulsBoard;

        public ScoresBoard ScoresBoard { get; private set; }
        public event EventHandler<EventArgs> PlayerLost;

        public Player(Game i_Game, SpaceShipConfiguration i_SpaceShipConfiguration, int i_PlayerId)
        {
            r_SpaceShip = new SpaceShip(i_Game, i_SpaceShipConfiguration, i_PlayerId);
            ScoresBoard = new ScoresBoard(i_Game, i_PlayerId, i_SpaceShipConfiguration.TextColor);
            r_SoulsBoard = new SoulsBoard(i_Game, k_InitialLives, i_SpaceShipConfiguration.AssteName, i_PlayerId);
            r_SpaceShip.SpaceShipHit += spaceShipOnHit;
            r_SpaceShip.Died += spaceShipOnDie;
            r_SpaceShip.BulletCollided += bulletCollision;

            var gameStateService = i_Game.Services.GetService(typeof(IGameStateService)) as IGameStateService;
            gameStateService.AddPlayer(this);
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
            if (r_SoulsBoard.SoulsCount == 0)
            {
                spaceShipOnDie(i_Sender, i_EventArgs);
            }
            else
            {
                r_SpaceShip.StartHitAnimation();
                r_SpaceShip.ResetPosition();
            }
        }
    }
}
