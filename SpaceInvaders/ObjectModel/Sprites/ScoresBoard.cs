using System;
using System.Collections.Generic;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.ObjectModel.Sprites
{
    public class ScoresBoard : GameComponent
    {
        private SpriteFont m_ArialFont;

        private readonly Color r_TextColor;
        private const int k_InitialLeftPadding = 5;
        private const int k_InitialHightPadding = 5;
        private const int k_RecordHight = 20;
        private readonly GameScreen r_GameScreen;
        private SpriteBatch m_SpriteBatch;

        private readonly int r_PlayerNumber;
        private ISettingsManager m_SettingsManager;

        public int PlayerNumber
        {
            get { return r_PlayerNumber; }
        }

        public int Score
        {
            get { return m_SettingsManager.PlayersData[r_PlayerNumber].Score; } 
            private set { m_SettingsManager.PlayersData[r_PlayerNumber].Score = value; }
        }

        public ScoresBoard(GameScreen i_GameScreen, int i_PlayerNumber, Color i_TextColor)
            : base(i_GameScreen.Game)
        {
            r_TextColor = i_TextColor;
            r_PlayerNumber = i_PlayerNumber;
            r_GameScreen = i_GameScreen;
            i_GameScreen.Add(this);
        }

        public void AddScore(int i_Score)
        {
            Score = MathHelper.Clamp(Score + i_Score, 0, Int32.MaxValue);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SettingsManager = Game.Services.GetService<ISettingsManager>();
            m_ArialFont = Game.Services.GetService<IFontManager>().SpriteFont;
            m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }


        public void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin();
            string stringToDraw = String.Format("P{0} Score: {1}", PlayerNumber, Score);
            Vector2 drawingPosition = getDrawingPosition();
            m_SpriteBatch.DrawString(m_ArialFont, stringToDraw, drawingPosition, r_TextColor);
            m_SpriteBatch.End();
        }

        private Vector2 getDrawingPosition()
        {
            return new Vector2(k_InitialLeftPadding, k_InitialHightPadding + (PlayerNumber * k_RecordHight));
        }

    }
}
