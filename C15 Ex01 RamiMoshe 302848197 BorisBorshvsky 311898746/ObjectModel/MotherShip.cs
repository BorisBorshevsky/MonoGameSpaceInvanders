using Infrastructure.Common;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssteName = @"Sprites\MotherShip_32x120";
        public const int k_ScoreOnHit = 750;
        private IGameStateManager m_GameStateManagerService;

        public MotherShip(Game i_Game)
            : base(k_AssteName, i_Game)
        {
            m_TintColor = Color.Red;
        }

        public override void Collided(ICollidable2D i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null)
            {
                if (bullet.Velocity.Y < 0)
                {
                    Remove();
                    Dispose();
                    m_GameStateManagerService.AddToScore(k_ScoreOnHit);
                }
            }
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2(Width*-1f + 1, Height*1f);
            Velocity = new Vector2(110, 0);
        }

        public override void Initialize()
        {
            m_GameStateManagerService = Game.Services.GetService(typeof (IGameStateManager)) as IGameStateManager;
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (IsOutOfBounts())
            {
                Remove();
                Dispose();
            }
        }
    }
}