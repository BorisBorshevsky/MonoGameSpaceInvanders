using Infrastructure.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
    class EnemyMatrix : DrawableGameComponent
    {
        private const int EnemiesInColumn = 5;
        private const int EnemiesInRow = 9;

        private readonly int m_InitialLeftPadding = 0;
        private readonly int m_InitialTopPadding = 3;

        private int m_LeftPadding = 0;
        private int m_TopPadding = 0;

        private int m_enemyWidth;
        private int m_enemyHeight;


        public Enemy[,] Enemy { get; set; }

        public EnemyMatrix(Game game) : base(game)
        {
            Enemy = new Enemy[EnemiesInRow, EnemiesInColumn]; 
            this.Game.Components.Add(this);
           
            
            for (int col = 0; col < EnemiesInRow; col++)
            {
                for (int row = 0; row < EnemiesInColumn; row++)
                {
                    Enemy[col, row] = new Enemy(this.Game, Color.AliceBlue);
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int col = 0; col < EnemiesInRow; col++)
            {
                for (int row = 0; row < EnemiesInColumn; row++)
                {
                    Enemy enemy = Enemy[col, row];
                    enemy.Initialize();
                    float horizontalDistanceBetweenEnemies = 0.6f * enemy.Width;
                    float verticalDistanceBetweenEnemies = 0.6f * enemy.Height;
                    float enemyXPosition = m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                    float enemyYPosition = m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

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

            m_enemyWidth = Enemy[0, 0].Width;
            m_enemyHeight = Enemy[0, 0].Height;

            m_TimeToNextBlink += gameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeToNextBlink >= 0.5)
            {
                

                for (int col = 0; col < EnemiesInRow; col++)
                {
                    for (int row = 0; row < EnemiesInColumn; row++)
                    {
                        Enemy enemy = Enemy[col, row];

                        float horizontalDistanceBetweenEnemies = 0.6f*enemy.Width;
                        float verticalDistanceBetweenEnemies = 0.6f*enemy.Height;
                        float enemyXPosition = m_LeftPadding + col * (horizontalDistanceBetweenEnemies + enemy.Width);
                        float enemyYPosition = m_TopPadding + row * (verticalDistanceBetweenEnemies + enemy.Height);

                        enemy.Position = new Vector2(enemyXPosition, enemyYPosition);
                    }
                }

                m_LeftPadding += m_enemyWidth / 2;
                m_TimeToNextBlink -= 0.5;
            }

        }

        public double m_TimeToNextBlink { get; set; }
    }
}
