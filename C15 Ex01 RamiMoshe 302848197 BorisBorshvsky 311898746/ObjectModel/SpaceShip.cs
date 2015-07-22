using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    using Infrastructure.ObjectModel;
    using Infrastructure.ServiceInterfaces;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class SpaceShip : Sprite
	{
        private const string k_AssteName = @"Sprites\Ship01_32x32";

        IInputManager m_InputManager;

		public SpaceShip(SapceInvadersGame i_Game)
			: base(k_AssteName, i_Game)
		{}

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            base.Initialize();
        }

		protected override void InitBounds()
		{
            base.InitBounds();

			// put in bottom center of view port:
			// get the bottom and center
			float x = (float)GraphicsDevice.Viewport.Width / 2;
			float y = (float)GraphicsDevice.Viewport.Height;

			// offset:
			x -= m_Width / 2;
			y -= m_Height / 2;

			// put it a little bit higher:
			y -= 30;

			this.Position = new Vector2(x, y);
		}

        const float k_Velocity = 80f;
        const float k_BulletVelocity = 200f;

        public override void Update(GameTime gameTime)
        {
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Left))
            {
                m_Velocity.X = k_Velocity * -1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Right))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            if (m_InputManager.KeyPressed(Keys.Space))
            {
                Shoot();
            }

            base.Update(gameTime);
        }

        protected void Shoot()
        {
            Bullet bullet = new Bullet(this.Game);
            bullet.Initialize();
            bullet.TintColor = Color.Red;
            bullet.Position = new Vector2(m_Position.X + m_Width / 2,m_Position.Y - bullet.Height / 2);
            bullet.Velocity = new Vector2(0, -k_BulletVelocity);
        }
    }
}
