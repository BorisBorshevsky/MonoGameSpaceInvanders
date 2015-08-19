using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel.Managers
{
    internal class InvaderGrid : RegisteredComponent
    {
        private const int k_InvadersInRow = 9; //9
        private const int k_PinkInvadersInColumn = 1;
        private const int k_LightBlueInvadersInColumn = 2; // 2
        private const int k_YellowInvadersInColumn = 2; // 2

        private const int k_InvadersInColumn =
            k_PinkInvadersInColumn + k_LightBlueInvadersInColumn + k_YellowInvadersInColumn;

        private const int k_InitialTopPadding = 3; //enemy units
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
        private IGameStateService m_GameStateService;


        public InvaderGrid(Game i_Game)
            : base(i_Game)
        {
            constructInvaders();
        }

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
            Invaders = new Invader[k_InvadersInRow, k_InvadersInColumn];

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = 0; row < k_PinkInvadersInColumn; row++)
                {
                    Invaders[col, row] = new PinkInvader(Game);
                }
            }

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn; row < k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row++)
                {
                    Invaders[col, row] = new LightBlueInvader(Game);
                }
            }

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row < k_InvadersInColumn; row++)
                {
                    Invaders[col, row] = new YellowInvader(Game);
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            m_GameStateService = Game.Services.GetService(typeof(IGameStateService)) as IGameStateService;

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

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = 0; row < k_InvadersInColumn; row++)
                {
                    Invader invader = Invaders[col, row];

                    float enemyXPosition = m_LeftPadding + col * (m_HorizontalDistanceBetweenEnemies + invader.Width);
                    float enemyYPosition = m_TopPadding + row * (m_VerticalDistanceBetweenEnemies + invader.Height);

                    invader.Position = new Vector2(enemyXPosition, enemyYPosition);
                    invader.OnInvaderDied += OnDeadInvader;
                }
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
            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = 0; row < k_InvadersInColumn; row++)
                {
                    Invader enemy = Invaders[col, row];
                    if (enemy.IsAlive)
                    {
                        float enemyXPosition = m_LeftPadding + col*(m_HorizontalDistanceBetweenEnemies + enemy.Width);
                        float enemyYPosition = m_TopPadding + row*(m_VerticalDistanceBetweenEnemies + enemy.Height);
                        enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                    }
                }
            }
        }

        public void OnDeadInvader(Object i_Sender, EventArgs i_Args)
        {
            m_TimeBetweenJumpsInSeconds *= k_SpeedAfterInvaderDead;
            if (++m_AmountOfEnemiesDead >= k_InvadersInColumn * k_InvadersInRow)
            {
                m_GameStateService.GameOver("You Won!");
            }
        }
    }
}