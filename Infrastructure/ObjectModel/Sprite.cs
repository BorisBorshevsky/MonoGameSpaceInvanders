//*** Guy Ronen (c) 2008-2011 ***//

using Infrastructure.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel
{
    public class Sprite : LoadableDrawableComponent
    {
        protected int m_Height;
        private bool m_IsAlive = true;
        protected Vector2 m_Position;
        protected SpriteBatch m_SpriteBatch;
        protected Color m_TintColor = Color.White;
        private bool m_UseSharedBatch = true;
        protected Vector2 m_Velocity = Vector2.Zero;
        protected int m_Width;

        public Sprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        {
        }

        public Rectangle BoundingRect
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, Width, Height); }
        }

        public bool IsAlive
        {
            get { return m_IsAlive; }
            set { m_IsAlive = value; }
        }

        public Texture2D Texture { get; set; }

        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                m_Position = value;
                RaisePositionChanged();
            }
        }

        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return m_TintColor.A/(float) byte.MaxValue; }
            set { m_TintColor.A = (byte) (value*byte.MaxValue); }
        }

        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public SpriteBatch SpriteBatch
        {
            set
            {
                m_SpriteBatch = value;
                m_UseSharedBatch = true;
            }
        }

        /// <summary>
        ///     Default initialization of bounds
        /// </summary>
        /// <remarks>
        ///     Derived classes are welcome to override this to implement their specific boudns initialization
        /// </remarks>
        protected override void InitBounds()
        {
            // default initialization of bounds
            m_Width = Texture.Width;
            m_Height = Texture.Height;
        }

        public virtual void Collided(ICollidable2D i_Collidable)
        {
            Remove();
            Dispose();
        }

        public virtual bool CollidesWith(ICollidable2D i_Collidable)
        {
            if (Visible && i_Collidable.Visible)
            {
                return BoundingRect.Intersects(i_Collidable.BoundingRect);
            }
            return false;
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>(m_AssetName);

            if (m_SpriteBatch == null)
            {
                m_SpriteBatch = Game.Services.GetService(typeof (SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                    m_UseSharedBatch = false;
                }
            }

            base.LoadContent();
        }

        /// <summary>
        ///     Basic movement logic (position += velocity * totalSeconds)
        /// </summary>
        /// <param name="i_GameTime"></param>
        /// <remarks>
        ///     Derived classes are welcome to extend this logic.
        /// </remarks>
        public override void Update(GameTime i_GameTime)
        {
            Position += Velocity*(float) i_GameTime.ElapsedGameTime.TotalSeconds;
            base.Update(i_GameTime);
        }

        /// <summary>
        ///     Basic texture draw behavior, using a shared/own sprite batch
        /// </summary>
        /// <param name="i_GameTime"></param>
        public override void Draw(GameTime i_GameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            m_SpriteBatch.Draw(Texture, m_Position, m_TintColor*Opacity);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(i_GameTime);
        }

        public virtual void Remove()
        {
            IsAlive = false;
            Visible = false;
            Game.Components.Remove(this);
        }

        public virtual bool IsOutOfBounts()
        {
            return !BoundingRect.Intersects(Game.GraphicsDevice.Viewport.Bounds);
        }
    }
}