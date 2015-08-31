using System;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Settings;

namespace SpaceInvaders.ObjectModel
{
    class ScoresBoard : GameComponent
    {
        private const int k_InitialLeftPadding = 5;
        private const int k_InitialHeightPadding = 5;
        private const int k_RecordHeight = 20;

        private readonly Color r_TextColor;
        private readonly int r_PlayerNumber;

        private SpriteFont m_ArialFont;
        private SpriteBatch m_SpriteBatch;
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
            return new Vector2(k_InitialLeftPadding, k_InitialHeightPadding + (PlayerNumber * k_RecordHeight));
        }
    }
}