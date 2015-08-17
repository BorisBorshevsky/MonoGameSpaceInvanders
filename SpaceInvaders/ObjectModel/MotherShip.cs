
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\MotherShip_32x120";
        private readonly int m_Score = 750;
        
        public int Score
        {
            get { return m_Score; }
        }


        public MotherShip(Game i_Game, bool i_Enable = true)
            : base(k_AssteName, i_Game)
        {
            m_TintColor = Color.Red;
            Enabled = i_Enable;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y < 0)
                {
                    Stop();
                }
            }
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2(Width * -1f + 1, Height * 1f);
            Velocity = new Vector2(110, 0);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (IsOutOfBounts())
            {
                Stop();
            }
        }

        public void Stop()
        {
            Visible = false;
            Enabled = false;
        }

        public void Start()
        {
            InitBounds();
            Visible = true;
            Enabled = true;
        }
    }
}