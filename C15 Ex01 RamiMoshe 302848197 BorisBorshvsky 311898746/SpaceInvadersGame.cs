﻿using Infrastructure.Managers;
using SpaceInvaders.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SapceInvadersGame : Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;

        public SapceInvadersGame()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            InputManager inputManager = new InputManager(this);

            

            //SpaceShip ship = new SpaceShip(this);

            Background background = new Background(this, @"Sprites\BG_Space01_1024x768", 0.3f);
            //Enemy enemy = new Enemy(this, Color.LightPink);

            EnemyMatrix enemyMatrix = new EnemyMatrix(this);


        }

        protected override void Initialize()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            m_SpriteBatch.Begin();
            base.Draw(gameTime);
            m_SpriteBatch.End();
        }
    }
}
