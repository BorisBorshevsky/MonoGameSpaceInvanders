using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;
using Infrastructure.Managers;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class Player
    {
        //public event void OnLifeLose();

        private readonly SpaceShip m_SpaceShip;
        private const int k_LoosingLifeScorePanalty = -1000;
        private const int k_InitialLives = 3;
        private SoulsBoard m_SoulsBoard;

        public ScoresBoard ScoresBoard { get; private set; }
        public event EventHandler<EventArgs> PlayerLost;

        public Player(Game i_Game, SpaceShipConfiguration i_SpaceShipConfiguration, int i_PlayerId)
        {
            m_SpaceShip = new SpaceShip(i_Game, i_SpaceShipConfiguration, i_PlayerId);
            ScoresBoard = new ScoresBoard(i_Game, i_PlayerId, i_SpaceShipConfiguration.TextColor);
            m_SoulsBoard = new SoulsBoard(i_Game, k_InitialLives, i_SpaceShipConfiguration.AssteName, i_PlayerId);
            m_SpaceShip.OnSpaceShipHit += spaceShipOnHit;
            m_SpaceShip.OnDie += spaceShipOnDie;
            m_SpaceShip.OnBulletCollision += bulletCollision;

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
            m_SpaceShip.InputManager = new DummyInputManager();
            m_SpaceShip.DieAnimationFinished += onPlayerLost; 
            m_SpaceShip.StartDieAnimation();
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
            m_SoulsBoard.RemoveSoul();
            if (m_SoulsBoard.SoulsCount == 0)
            {
                spaceShipOnDie(i_Sender, i_EventArgs);
            }
            else
            {
                m_SpaceShip.StartHitAnimation();
                m_SpaceShip.ResetPosition();
            }
        }
    }
}
