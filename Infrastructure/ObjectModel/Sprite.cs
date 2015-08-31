
using Infrastructure.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ObjectModel
{
    public class Sprite : LoadableDrawableComponent
    {
        protected CompositeAnimator m_Animations;
        public CompositeAnimator Animations
        {
            get { return m_Animations; }
            set { m_Animations = value; }
        }

        protected Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }

        public float Width
        {
            get { return m_WidthBeforeScale * m_Scales.X; }
            set { m_WidthBeforeScale = value / m_Scales.X; }
        }

        public float Height
        {
            get { return m_HeightBeforeScale * m_Scales.Y; }
            set { m_HeightBeforeScale = value / m_Scales.Y; }
        }

        protected float m_WidthBeforeScale;
        public float WidthBeforeScale
        {
            get { return m_WidthBeforeScale; }
            set { m_WidthBeforeScale = value; }
        }

        protected float m_HeightBeforeScale;
        public float HeightBeforeScale
        {
            get { return m_HeightBeforeScale; }
            set { m_HeightBeforeScale = value; }
        }

        protected Vector2 m_Position = Vector2.Zero;
        /// <summary>
        /// Represents the location of the sprite's origin point in screen coorinates
        /// </summary>
        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                    OnPositionChanged();
                }
            }
        }

        public Vector2 m_PositionOrigin;
        public Vector2 PositionOrigin
        {
            get { return m_PositionOrigin; }
            set { m_PositionOrigin = value; }
        }

        public Vector2 m_RotationOrigin = Vector2.Zero;
        public Vector2 RotationOrigin
        {
            get { return m_RotationOrigin; }// r_SpriteParameters.RotationOrigin; }
            set { m_RotationOrigin = value; }
        }

        protected Vector2 PositionForDraw
        {
            get { return this.Position - this.PositionOrigin + this.RotationOrigin; }
        }

        public Vector2 TopLeftPosition
        {
            get { return this.Position - this.PositionOrigin; }
            set { this.Position = value + this.PositionOrigin; }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.Width,
                    (int)this.Height);
            }
        }

        public Rectangle BoundsBeforeScale
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.WidthBeforeScale,
                    (int)this.HeightBeforeScale);
            }
        }

        protected Rectangle m_SourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return m_SourceRectangle; }
            set
            {
                m_SourceRectangle = value;
                m_WidthBeforeScale = value.Width;
                m_HeightBeforeScale = value.Height;
            }
        }

        public Vector2 TextureCenter
        {
            get
            {
                return new Vector2((float)m_Texture.Width / 2, (float)m_Texture.Height / 2);
            }
        }

        public Vector2 SourceRectangleCenter
        {
            get { return new Vector2((float)m_SourceRectangle.Width / 2, (float)m_SourceRectangle.Height / 2); }
        }

        protected float m_Rotation = 0;
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        protected Vector2 m_Scales = Vector2.One;
        public Vector2 Scales
        {
            get { return m_Scales; }
            set
            {
                if (m_Scales != value)
                {
                    m_Scales = value;
                    // Notify the Collision Detection mechanism:
                    OnPositionChanged();
                }
            }
        }

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return m_TintColor.A / (float)byte.MaxValue; }
            set { m_TintColor.A = (byte)(value * byte.MaxValue); }
        }

        protected float m_LayerDepth;
        public float LayerDepth
        {
            get { return m_LayerDepth; }
            set { m_LayerDepth = value; }
        }

        protected SpriteEffects m_SpriteEffects = SpriteEffects.None;
        public SpriteEffects SpriteEffects
        {
            get { return m_SpriteEffects; }
            set { m_SpriteEffects = value; }
        }

        protected Vector2 m_Velocity = Vector2.Zero;
        /// <summary>
        /// Pixels per Second on 2 axis
        /// </summary>
        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        /// <summary>
        /// Radians per Second on X Axis
        /// </summary>
        public float AngularVelocity { get; set; }
        protected GameScreen Screen { get; set; }

        public Sprite(string i_AssetName, GameScreen i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game.Game, i_UpdateOrder, i_DrawOrder)
        {
            AngularVelocity = 0;
            Screen = i_Game;
            Screen.Add(this);
        }

        public Sprite(string i_AssetName, GameScreen i_Game, int i_CallsOrder)
            : this(i_AssetName, i_Game, i_CallsOrder, 0)
        {
        }

        public Sprite(string i_AssetName, GameScreen i_Game)
            : this(i_AssetName, i_Game, int.MaxValue)
        { }

        /// <summary>
        /// Default initialization of bounds
        /// </summary>
        /// <remarks>
        /// Derived classes are welcome to override this to implement their specific boudns initialization
        /// </remarks>
        protected override void InitBounds()
        {
            m_WidthBeforeScale = m_Texture.Width;
            m_HeightBeforeScale = m_Texture.Height;

            InitSourceRectangle();

            InitOrigins();
        }

        protected virtual void InitOrigins()
        {
        }

        protected virtual void InitSourceRectangle()
        {
            m_SourceRectangle = new Rectangle(0, 0, (int)m_WidthBeforeScale, (int)m_HeightBeforeScale);
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

        public override void Initialize()
        {
            base.Initialize();

            m_Animations = new CompositeAnimator(this);
        }

        protected override void LoadContent()
        {
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);

            if (m_SpriteBatch == null)
            {
                m_SpriteBatch =
                    Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

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
        /// <param name="i_GameTime"></param>
        /// <remarks>
        /// Derived classes are welcome to extend this logic.
        /// </remarks>
        public override void Update(GameTime i_GameTime)
        {
            float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            this.Position += this.Velocity * totalSeconds;
            this.Rotation += this.AngularVelocity * totalSeconds;

            base.Update(i_GameTime);

            this.Animations.Update(i_GameTime);
        }

        /// <summary>
        /// Basic texture draw behavior, using a shared/own sprite batch
        /// </summary>
        /// <param name="i_GameTime"></param>
        public override void Draw(GameTime i_GameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            m_SpriteBatch.Draw(m_Texture, this.PositionForDraw, this.SourceRectangle, this.TintColor, this.Rotation, this.RotationOrigin, this.Scales, SpriteEffects.None, this.LayerDepth);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(i_GameTime);
        }

        #region Collision Handlers
        protected override void DrawBoundingBox()
        {
            // not implemented yet
        }

        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;
            ICollidable2D source = i_Source as ICollidable2D;
            if (source != null)
            {
                collided = source.Bounds.Intersects(this.Bounds);
            }

            return collided;
        }

        public virtual void Collided(ICollidable i_Collidable)
        {
            Dispose();
        }
        #endregion //Collision Handlers

        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }

        public virtual bool IsOutOfBounts()
        {
            return !Bounds.Intersects(Game.GraphicsDevice.Viewport.Bounds);
        }

        private bool m_IsAlive = true;
        public bool IsAlive
        {
            get { return m_IsAlive; }
            set { m_IsAlive = value; }
        }

        protected override void Dispose(bool i_Disposing)
        {
            Enabled = false;
            IsAlive = false;
            Visible = false;
            base.Dispose(i_Disposing);
        }
    }
}