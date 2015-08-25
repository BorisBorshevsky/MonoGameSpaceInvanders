using System;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.ObjectModel.Managers
{
    internal class MotherShipDeployer : RegisteredComponent
    {
        private const int k_MinTimeBetweenMotheships = 3000;
        private const int k_MaxTimeBetweenMotheships = 10000;
        private readonly Random r_Random = new Random();
        private MotherShip m_MotherShip;
        private int m_TimeToNextMotherShip;

        public MotherShipDeployer(Game i_Game)
            : base(i_Game)
        {
            m_TimeToNextMotherShip = r_Random.Next(k_MinTimeBetweenMotheships, k_MaxTimeBetweenMotheships);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_MotherShip = new MotherShip(Game);
            m_MotherShip.Initialize();
        }

        public double CurrentElapsedTime { get; set; }

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
                m_TimeToNextMotherShip = r_Random.Next(k_MinTimeBetweenMotheships, k_MaxTimeBetweenMotheships);
            }

            base.Update(i_GameTime);
        }
    }
}