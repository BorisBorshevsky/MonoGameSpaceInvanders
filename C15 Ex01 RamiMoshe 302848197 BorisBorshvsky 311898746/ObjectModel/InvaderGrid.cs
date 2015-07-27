using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    class InvaderGrid : RegisteredComponent
    {
        private const int k_InvadersInRow = 9;
        private const int k_PinkInvadersInColumn = 1;
        private const int k_LightBlueInvadersInColumn = 2;
        private const int k_YellowInvadersInColumn = 2;
        private const int k_InvadersInColumn = k_PinkInvadersInColumn + k_LightBlueInvadersInColumn + k_YellowInvadersInColumn;

        private const int k_InitialLeftPadding = 0; //enemy units
        private const int k_InitialTopPadding = 3;  //enemy units

        private const float k_InitialTimeBetweenJumpsInSeconds = 0.5f;
        private const float k_SpeedAfterNewLine = 0.95f;

        private float m_TimeBetweenJumpsInSeconds = k_InitialTimeBetweenJumpsInSeconds;
        private float m_LeftPadding = 0;
        private float m_TopPadding = 0;

        //private float matrixTotalWidth;
        //private float matrixTotlaHeight;

        private int enemyWidth;
        private int enemyHeight;


        private EnemyDirection currentDirection = EnemyDirection.RIGHT;

        private float horizontalDistanceBetweenEnemies;
        private float verticalDistanceBetweenEnemies;

        private bool jumpDownOnNextJump = false;


        public Invader[,] Invaders { get; set; }




        public double CurrentElapsedTime { get; set; }


        public InvaderGrid(Game game)
            : base(game)
        {
            Invaders = new Invader[k_InvadersInRow, k_InvadersInColumn];

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = 0; row < k_PinkInvadersInColumn; row++)
                {
                    Invaders[col, row] = new PinkInvader(this.Game);
                }
            }

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn; row < k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row++)
                {
                    Invaders[col, row] = new LightBlueInvader(this.Game);
                }
            }

            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = k_PinkInvadersInColumn + k_LightBlueInvadersInColumn; row < k_InvadersInColumn; row++)
                {
                    Invaders[col, row] = new YellowInvader(this.Game);
                }
            }

        }

        enum EnemyDirection
        {
            LEFT,
            RIGHT
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (Invader enemy in Invaders)
            {
                enemy.Initialize();
            }

            Invader enemyForInitialization = Invaders[0, 0];
            enemyWidth = enemyForInitialization.Width;
            enemyHeight = enemyForInitialization.Height;
            horizontalDistanceBetweenEnemies = 0.6f * enemyWidth;
            verticalDistanceBetweenEnemies = 0.6f * enemyHeight;


            for (int col = 0; col < k_InvadersInRow; col++)
            {
                for (int row = 0; row < k_InvadersInColumn; row++)
                {
                    Invader enemy = Invaders[col, row];
                    enemy.Initialize();
                    float enemyXPosition = k_InitialLeftPadding * enemy.Width + m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                    float enemyYPosition = k_InitialTopPadding * enemy.Height + m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

                    enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {




            CurrentElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentElapsedTime >= m_TimeBetweenJumpsInSeconds)
            {

                //decide enemy position
                

                if (jumpDownOnNextJump)
                {
                    m_TopPadding += enemyHeight / 2;
                    jumpDownOnNextJump = false;
                }
                else
                {

                    float minX = int.MaxValue;
                    float maxX = int.MinValue;
                 
                    foreach (var invader in Invaders)
                    {
                        if (invader.IsAlive)
                        {
                            if (invader.Position.X < minX) minX = invader.Position.X;
                            if (invader.Position.X + invader.Width > maxX) maxX = invader.Position.X + invader.Width;
                        }
                    }
                    float matrixTotalWidth = maxX - minX;

                    //change direction part
                    if (currentDirection == EnemyDirection.RIGHT)
                    {
                        if (maxX + enemyWidth / 2 >= Game.GraphicsDevice.Viewport.Width)
                        {
                            m_LeftPadding = Game.GraphicsDevice.Viewport.Width - matrixTotalWidth;
                            jumpDownOnNextJump = true;
                            m_TimeBetweenJumpsInSeconds *= k_SpeedAfterNewLine;
                            currentDirection = EnemyDirection.LEFT;
                        }
                        else
                        {
                            m_LeftPadding += enemyWidth / 2;
                        }
                    }
                    else
                    {
                        if (minX - enemyWidth / 2 <= 0)
                        {
                            m_LeftPadding = 0;
                            jumpDownOnNextJump = true;
                            m_TimeBetweenJumpsInSeconds *= k_SpeedAfterNewLine;
                            currentDirection = EnemyDirection.RIGHT;
                        }
                        else
                        {
                            m_LeftPadding -= enemyWidth / 2;
                        }

                    }
                }

                //update enemies position
                for (int col = 0; col < k_InvadersInRow; col++)
                {
                    for (int row = 0; row < k_InvadersInColumn; row++)
                    {
                        Invader enemy = Invaders[col, row];

                        float enemyXPosition = k_InitialLeftPadding * enemy.Width + m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                        float enemyYPosition = k_InitialTopPadding * enemy.Height + m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

                        enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                    }
                }

                CurrentElapsedTime -= m_TimeBetweenJumpsInSeconds;
            }
            base.Update(gameTime);

        }


        public void NotifyOnDeadInvader()
        {
            m_TimeBetweenJumpsInSeconds *= 0.92f;
        }
    }
}
