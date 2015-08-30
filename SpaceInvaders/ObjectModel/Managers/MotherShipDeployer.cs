using System;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.ObjectModel.Managers
{
    class MotherShipDeployer : RegisteredComponent
    {
        private const int k_MinTimeBetweenMotherships = 3000;
        private const int k_MaxTimeBetweenMotherships = 10000;

        private readonly Random r_Random = new Random();

        private MotherShip m_MotherShip;
        private int m_TimeToNextMotherShip;

        public double CurrentElapsedTime { get; set; }

        public MotherShipDeployer(GameScreen i_GameScreen)
            : base(i_GameScreen)
        {
            m_TimeToNextMotherShip = r_Random.Next(k_MinTimeBetweenMotherships, k_MaxTimeBetweenMotherships);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_MotherShip = new MotherShip(Screen);
            m_MotherShip.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            CurrentElapsedTime += i_GameTime.ElapsedGameTime.TotalMilliseconds;
            if (CurrentElapsedTime >= m_TimeToNextMotherShip)
            {
                if (m_MotherShip.Enabled == false)
                {
                    m_MotherShip.Start();
                }

                CurrentElapsedTime = 0;
                m_TimeToNextMotherShip = r_Random.Next(k_MinTimeBetweenMotherships, k_MaxTimeBetweenMotherships);
            }

            base.Update(i_GameTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_MotherShip.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}