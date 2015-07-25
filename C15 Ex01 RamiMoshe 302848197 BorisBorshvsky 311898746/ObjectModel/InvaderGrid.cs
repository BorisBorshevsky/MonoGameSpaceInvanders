using Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    class InvaderGrid : DrawableGameComponent
    {
        private const int EnemiesInColumn = 5;
        //private const int EnemiesInColumn = 1;
        private const int EnemiesInRow = 9;
        //private const int EnemiesInRow = 1;

        private readonly int m_InitialLeftPadding = 0; //enemy units
        private readonly int m_InitialTopPadding = 3;  //enemy units

        private float m_LeftPadding = 0;
        private float m_TopPadding = 0;

        float matrixTotalWidth;
        float matrixTotlaHeight;

        int enemyWidth;
        int enemyHeight;

        float horizontalDistanceBetweenEnemies;
        float verticalDistanceBetweenEnemies;


        const float speedAfterNewLine = 0.95f;

        public Invader[,] Invades { get; set; }

        float timeBetweenJumpsInSeconds = 0.5f;

        EnemyDirection currentDirection = EnemyDirection.RIGHT;

        public double CurrentElapsedTime { get; set; }


        public InvaderGrid(Game game)
            : base(game)
        {
            Invades = new Invader[EnemiesInRow, EnemiesInColumn];
            this.Game.Components.Add(this);

            for (int col = 0; col < EnemiesInRow; col++)
            {
                for (int row = 0; row < EnemiesInColumn; row++)
                {
                    Invades[col, row] = new Invader(this.Game, Color.AliceBlue);
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

            foreach (Invader enemy in Invades)
            {
                enemy.Initialize();
            }
            Invader enemyForInitialization = Invades[0, 0];
            enemyWidth = enemyForInitialization.Width;
            enemyHeight = enemyForInitialization.Height;
            horizontalDistanceBetweenEnemies = 0.6f * enemyWidth;
            verticalDistanceBetweenEnemies = 0.6f * enemyHeight;
            matrixTotalWidth = (enemyWidth * EnemiesInRow) + (horizontalDistanceBetweenEnemies * (EnemiesInRow - 1));
            matrixTotlaHeight = (enemyHeight * EnemiesInColumn) + (verticalDistanceBetweenEnemies * (EnemiesInColumn - 1));


            for (int col = 0; col < EnemiesInRow; col++)
            {
                for (int row = 0; row < EnemiesInColumn; row++)
                {
                    Invader enemy = Invades[col, row];
                    enemy.Initialize();
                    float enemyXPosition = m_InitialLeftPadding * enemy.Width + m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                    float enemyYPosition = m_InitialTopPadding * enemy.Height + m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

                    enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                }
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CurrentElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentElapsedTime >= timeBetweenJumpsInSeconds) 
            {


                for (int col = 0; col < EnemiesInRow; col++)
                {
                    for (int row = 0; row < EnemiesInColumn; row++)
                    {
                        Invader enemy = Invades[col, row];

                        float enemyXPosition = m_InitialLeftPadding * enemy.Width + m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                        float enemyYPosition = m_InitialTopPadding * enemy.Height + m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

                        enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                    }
                }

                //change direction part
                if (currentDirection == EnemyDirection.RIGHT)
                {
                    if (m_LeftPadding + matrixTotalWidth + enemyWidth / 2 >= Game.GraphicsDevice.Viewport.Width)
                    {
                        m_LeftPadding = Game.GraphicsDevice.Viewport.Width - matrixTotalWidth;
                        m_TopPadding += enemyHeight / 2;
                        timeBetweenJumpsInSeconds *= speedAfterNewLine;
                        currentDirection = EnemyDirection.LEFT;
                    } else{
                        m_LeftPadding += enemyWidth / 2;
                    }
                }
                else
                {
                    if (m_LeftPadding - enemyWidth /2 <= 0){
                        m_LeftPadding = 0;
                        m_TopPadding += enemyHeight / 2;
                        timeBetweenJumpsInSeconds *= speedAfterNewLine;
                        currentDirection = EnemyDirection.RIGHT;
                    } else{
                        m_LeftPadding -= enemyWidth / 2;
                    }
                    
                }

                CurrentElapsedTime -= timeBetweenJumpsInSeconds;
            }

        }


        public void NotifyOnDeadInvader()
        {
            timeBetweenJumpsInSeconds *= 0.92f;
        }
    }
}
