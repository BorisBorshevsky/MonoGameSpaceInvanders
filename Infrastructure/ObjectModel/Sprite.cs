//*** Guy Ronen (c) 2008-2011 ***//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel
{
    public class Sprite : LoadableDrawableComponent
    {
        public Rectangle BoundingRect { get; set; }

        //TODO: Refactor and move to it another layter
        public bool IsAlive { get; set; }

        private Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }

        protected int m_Width;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        protected int m_Height;
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        protected Vector2 m_Position;
        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return (float)m_TintColor.A / (float)byte.MaxValue; }
            set { m_TintColor.A = (byte)(value * (float)byte.MaxValue); }
        }

        protected Vector2 m_Velocity = Vector2.Zero;
        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public Sprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        {
            IsAlive = true;
        }

        /// <summary>
        /// Default initialization of bounds
        /// </summary>
        /// <remarks>
        /// Derived classes are welcome to override this to implement their specific boudns initialization
        /// </remarks>
        protected override void InitBounds()
        {
            // default initialization of bounds
            m_Width = m_Texture.Width;
            m_Height = m_Texture.Height;
            CalculateBoundingRect();
        }


        private bool m_UseSharedBatch = true;

        protected SpriteBatch m_SpriteBatch;
        public SpriteBatch SpriteBatch
        {
            set
            {
                m_SpriteBatch = value;
                m_UseSharedBatch = true;
            }
        }

        protected override void LoadContent()
        {
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);

            if (m_SpriteBatch == null)
            {
                m_SpriteBatch =Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                    m_UseSharedBatch = false;
                }
            }

            base.LoadContent();
        }

        /// <summary>
        /// Basic movement logic (position += velocity * totalSeconds)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <remarks>
        /// Derived classes are welcome to extend this logic.
        /// </remarks>
        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CalculateBoundingRect();
            base.Update(gameTime);
        }

        /// <summary>
        /// Basic texture draw behavior, using a shared/own sprite batch
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            m_SpriteBatch.Draw(m_Texture, m_Position, m_TintColor);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void CalculateBoundingRect()
        {
            
                int left = (int)Position.X;
                int top = (int)Position.Y;

                BoundingRect = new Rectangle(left, top, Width, Height);    
           
        }

        public virtual void Remove()
        {
            IsAlive = false;
            Game.Components.Remove(this);
        }

        public virtual bool IsOutOfBounts()
        {
            return !BoundingRect.Intersects(Game.GraphicsDevice.Viewport.Bounds);
        }

    }

}