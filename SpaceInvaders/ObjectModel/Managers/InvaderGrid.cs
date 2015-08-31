using System;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Sprites;
using SpaceInvaders.Settings;

namespace SpaceInvaders.ObjectModel.Managers
{
    class InvaderGrid : RegisteredComponent
    {
        private const int k_PinkInvadersInColumn = 1;
        private const int k_LightBlueInvadersInColumn = 2; 
        private const int k_YellowInvadersInColumn = 2; 
        private int m_InvadersInRow = 9;

        private const int k_InvadersInColumn =
            k_PinkInvadersInColumn + k_LightBlueInvadersInColumn + k_YellowInvadersInColumn;

        private const int k_InitialTopPadding = 3; //invader units
        private const float k_SpeedAfterInvaderDead = 0.92f;
        private const float k_InitialTimeBetweenJumpsInSeconds = 0.5f;
        private const float k_SpeedAfterNewLine = 0.95f;
        private int m_AmountOfEnemiesDead;
        private int m_Direction = 1; // 1 = right   // -1 = left
        private float m_EnemyHeight;
        private float m_EnemyWidth;
        private float m_HorizontalDistanceBetweenEnemies;
        private bool m_JumpDownOnNextJump;
        private float m_LeftPadding;
        private float m_TimeBetweenJumpsInSeconds = k_InitialTimeBetweenJumpsInSeconds;
        private float m_TopPadding;
        private float m_VerticalDistanceBetweenEnemies;
        private Rectangle m_GridBounds;
        private ISettingsManager m_SettingsManager;

        public event EventHandler<EventArgs> InvaderReachedBottom;
        public event EventHandler<EventArgs> AllEnemiesDied;

        public InvaderGrid(GameScreen i_GameScreen)
            : base(i_GameScreen)
        { }

        public double CurrentElapsedTime { get; set; }
        public Invader[,] Invaders { get; set; }

        private Rectangle calculateBounds()
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;

            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (Invader invader in Invaders)
            {
                if (invader.IsAlive)
                {
                    if (invader.Position.X < minX)
                    {
                        minX = invader.Bounds.X;
                    }

                    if (invader.Position.X + invader.Width > maxX)
                    {
                        maxX = invader.Bounds.X + invader.Bounds.Width;
                    }

                    if (invader.Position.Y < minY)
                    {
                        minY = invader.Bounds.Y;
                    }

                    if (invader.Position.Y + invader.Height > maxY)
                    {
                        maxY = invader.Bounds.Y + invader.Bounds.Height;
                    }
                }
            }

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        private void constructInvaders()
        {
            Invaders = new Invader[m_InvadersInRow, k_InvadersInColumn];

            for (int col = 0; col < m_InvadersInRow; col++)
            {
                for (int row = 0; row < k_PinkInvadersInColumn; row++)
                {
                    Invaders[col, row] = new PinkInvader(Screen);
                }
            }

            for (int col = 0; col < m_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn; row < k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row++)
                {
                    Invaders[col, row] = new LightBlueInvader(Screen);
                }
            }

            for (int col = 0; col < m_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row < k_InvadersInColumn; row++)
                {
                    Invaders[col, row] = new YellowInvader(Screen);
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();

            m_InvadersInRow += m_SettingsManager.GetGameLevelSettings().AdditionalInvadersColumns;
            constructInvaders();
            initializeInvaders();
            m_GridBounds = calculateBounds();
        }

        private void initializeInvaders()
        {
            foreach (Invader invader in Invaders)
            {
                invader.Initialize();
            }

            Invader enemyForInitialization = Invaders[0, 0];
            m_EnemyWidth = enemyForInitialization.Bounds.Width;
            m_EnemyHeight = enemyForInitialization.Bounds.Height;
            m_HorizontalDistanceBetweenEnemies = 0.6f * m_EnemyWidth;
            m_VerticalDistanceBetweenEnemies = 0.6f * m_EnemyHeight;
            m_TopPadding = k_InitialTopPadding * m_EnemyHeight;

            for (int col = 0; col < m_InvadersInRow; col++)
            {
                for (int row = 0; row < k_InvadersInColumn; row++)
                {
                    Invader invader = Invaders[col, row];

                    float enemyXPosition = m_LeftPadding + col * (m_HorizontalDistanceBetweenEnemies + invader.Width);
                    float enemyYPosition = m_TopPadding + row * (m_VerticalDistanceBetweenEnemies + invader.Height);

                    invader.Position = new Vector2(enemyXPosition, enemyYPosition);
                    invader.InvaderDied += OnDeadInvader;
                    invader.InvaderReachedBottom += onInvaderReachedBottom;
                }
            }
        }

        void onInvaderReachedBottom(object i_Sender , EventArgs i_Args)
        {
            if (InvaderReachedBottom != null)
            {
                InvaderReachedBottom.Invoke(i_Sender, i_Args);
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            CurrentElapsedTime += i_GameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentElapsedTime >= m_TimeBetweenJumpsInSeconds)
            {
                m_GridBounds = calculateBounds();
                if (m_JumpDownOnNextJump)
                {
                    moveDown();
                    m_JumpDownOnNextJump = false;
                }
                else
                {
                    moveHorizontally(m_GridBounds);
                }

                updateInvadersPositions();
                CurrentElapsedTime -= m_TimeBetweenJumpsInSeconds;
            }

            base.Update(i_GameTime);
        }

        private void moveDown()
        {
            m_TopPadding += m_EnemyHeight / 2;
            m_TimeBetweenJumpsInSeconds *= k_SpeedAfterNewLine;
        }

        private void moveHorizontally(Rectangle i_GridBounds)
        {
            bool willMissRight = i_GridBounds.Right + (m_EnemyWidth / 2) * m_Direction >= Game.GraphicsDevice.Viewport.Bounds.Right;
            bool willMissLeft = i_GridBounds.Left + (m_EnemyWidth / 2) * m_Direction <= Game.GraphicsDevice.Viewport.Bounds.Left;

            if (willMissLeft)
            {
                m_LeftPadding -= i_GridBounds.Left;
            }
            else if (willMissRight)
            {
                m_LeftPadding += Game.GraphicsDevice.Viewport.Width - i_GridBounds.Right;
            }
            else
            {
                m_LeftPadding += (m_EnemyWidth / 2) * m_Direction;
            }

            if (willMissLeft || willMissRight)
            {
                m_JumpDownOnNextJump = true;
                m_Direction *= -1;
            }
        }

        private void updateInvadersPositions()
        {
            for (int col = 0; col < m_InvadersInRow; col++)
            {
                for (int row = 0; row < k_InvadersInColumn; row++)
                {
                    Invader enemy = Invaders[col, row];
                    if (!enemy.IsAlive) continue;
                    
                    float enemyXPosition = m_LeftPadding + col*(m_HorizontalDistanceBetweenEnemies + enemy.Width);
                    float enemyYPosition = m_TopPadding + row*(m_VerticalDistanceBetweenEnemies + enemy.Height);
                    enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                }
            }
        }

        private void onAllEnemiesDied()
        {
            if (AllEnemiesDied != null)
            {
                AllEnemiesDied.Invoke(this, EventArgs.Empty);
            }
        }

        public void OnDeadInvader(object i_Sender, EventArgs i_Args)
        {
            m_TimeBetweenJumpsInSeconds *= k_SpeedAfterInvaderDead;
            if (++m_AmountOfEnemiesDead >= k_InvadersInColumn * m_InvadersInRow)
            {
                onAllEnemiesDied();
            }
        }

        protected override void Dispose(bool i_Disposing)
        {
            if (i_Disposing)
            {
                foreach (var invader in Invaders)
                {
                    invader.Dispose();
                }
            }

            base.Dispose(i_Disposing);
        }
    }
}