using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvaders.ObjectModel
{
    public class SpaceShip : Sprite
	{
        private const string k_AssteName = @"Sprites\Ship01_32x32";
        CollisionDetector m_CollisionDetector { get; set; }
        int Lives { get; set; }

        IInputManager m_InputManager;

        public SpaceShip(SapceInvadersGame i_Game)
            : base(k_AssteName, i_Game)
        {
            Lives = 3;
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_CollisionDetector = Game.Services.GetService(typeof(CollisionDetector)) as CollisionDetector;

            base.Initialize();
        }

		protected override void InitBounds()
		{
            base.InitBounds();

			// put in bottom center of view port:
			// get the bottom and center
			float y = (float)GraphicsDevice.Viewport.Height;

			// offset:
			y -= m_Height / 2;

			// put it a little bit higher:
			y -= 30;

			this.Position = new Vector2(0, y);
		}

        const float k_Velocity = 115f;
        const float k_BulletVelocity = 115f;

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

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
                m_Velocity.X = m_InputManager.MousePositionDelta.X *60;
            }

            
            m_Position.X = MathHelper.Clamp(m_Position.X, 0, Game.GraphicsDevice.Viewport.Width - m_Width);



            if (m_InputManager.KeyPressed(Keys.Enter) || m_InputManager.ButtonPressed(eInputButtons.Left))
            {
                Shoot();
            }

        }

        protected void Shoot()
        {
            SpaceShipBullet bullet = new SpaceShipBullet(this.Game);
            bullet.Initialize();
            bullet.Position = new Vector2(m_Position.X + m_Width / 2 - bullet.Width / 2 , m_Position.Y - bullet.Height);
            bullet.Velocity = new Vector2(0, -k_BulletVelocity);

            m_CollisionDetector.Add(bullet);
        }

        public void NotifyOnHit()
        {
            LoseLife();
            this.InitBounds();
        }

        public void LoseLife()
        {
            if (--Lives == 0)
            {
                //Game.GameOver()
            }
        }
	}
}
