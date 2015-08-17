using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Configurations;

namespace SpaceInvaders.ObjectModel
{
    public class Player
    {
        //public event void OnLifeLose();

        private readonly SpaceShip m_SpaceShip;
        private const int k_LoosingLifeScorePanalty = -1000;
        private const int k_InitialLives = 3;
        private int m_Lives = k_InitialLives;
        private int m_Score;

        public Player(Game i_Game, SpaceShipConfiguration i_SpaceShipConfiguration, int i_PlayerId)
        {
            m_SpaceShip = new SpaceShip(i_Game, i_SpaceShipConfiguration, i_PlayerId);
            m_SpaceShip.OnSpaceShipHit += spaceShipOnHit;
            m_SpaceShip.OnDie += spaceShipOnDie;
            m_SpaceShip.OnBulletCollision += bulletCollision;
        }

        private void bulletCollision(object i_Sender, EventArgs i_E)
        {
            Invader invader = i_Sender as Invader;
            if (invader != null)
            {
                m_Score += invader.Score;
            }
            MotherShip motherShip = i_Sender as MotherShip;
            if (motherShip != null)
            {
                m_Score += motherShip.Score;
            }
        }

        private void spaceShipOnDie(object i_Sender, EventArgs i_EventArgs)
        {
            // udpate game state service
            // call spaceship dead animation
            // game Over?
        }

        private void spaceShipOnHit(object i_Sender, EventArgs i_EventArgs)
        {
            m_Score += k_LoosingLifeScorePanalty;
            if (--m_Lives == 0)
            {
                spaceShipOnDie(i_Sender, i_EventArgs);
            }
            else
            {
                //lose life animation
            }
        }



    }
}
