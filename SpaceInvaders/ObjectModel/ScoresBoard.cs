﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.ObjectModel
{
    public class ScoresBoard : DrawableGameComponent
    {
        private SpriteFont m_ArialFont;
        private SpriteBatch spriteBatch;

        private int m_Score;
        private readonly int m_PlayerNumber;
        private readonly Color r_TextColor;
        private const int k_InitialLeftPadding = 5;
        private const int k_InitialHightPadding = 5;
        private const int k_RecordHight = 20;

        public ScoresBoard(Game i_Game, int i_PlayerNumber, Color i_TextColor)
            : base(i_Game)
        {
            r_TextColor = i_TextColor;
            m_PlayerNumber = i_PlayerNumber;
            i_Game.Components.Add(this);
        }

        public void AddScore(int i_Score)
        {
            m_Score = MathHelper.Clamp(m_Score + i_Score, 0, Int32.MaxValue);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_ArialFont = Game.Content.Load<SpriteFont>(@"Fonts\Arial"); //Should be Calibri
        }

        public override void Draw(GameTime i_GameTime)
        {
            spriteBatch.Begin();
            string stringToDraw = String.Format("P{0} Score: {1}", m_PlayerNumber, m_Score);
            Vector2 drawingPosition = getDrawingPosition();
            spriteBatch.DrawString(m_ArialFont, stringToDraw, drawingPosition, r_TextColor);
            spriteBatch.End();

            base.Draw(i_GameTime);
        }

        private Vector2 getDrawingPosition()
        {
            return new Vector2(k_InitialLeftPadding, k_InitialHightPadding + (m_PlayerNumber * k_RecordHight));
        }
    }
}