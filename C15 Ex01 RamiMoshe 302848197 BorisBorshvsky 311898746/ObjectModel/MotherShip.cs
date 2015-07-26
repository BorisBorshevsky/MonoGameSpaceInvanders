using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
	using Infrastructure.ObjectModel;

    public class MotherShip : Sprite
	{
        private const string k_AssteName = @"Sprites\MotherShip_32x120";
        public const int k_ScoreOnHit = 750;

        public MotherShip(Game i_Game)
            : base(k_AssteName, i_Game)
        {
            this.m_TintColor = Color.Red;
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            m_Position = new Vector2(Width * -1f, Height * 1f ); 
            Velocity = new Vector2(110, 0);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.IsOutOfBounts())
            {
                this.Remove();
            }
        }
	}
}
