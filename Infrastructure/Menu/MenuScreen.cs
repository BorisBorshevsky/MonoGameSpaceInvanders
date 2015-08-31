﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure;
using Infrastructure.Animators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure
{
    public class MenuScreen : GameScreen
    {
        private List<MenuItem> m_MenuItem;
        private List<AnimatedSpriteText> m_AnimetedSpriteText;
        protected SpriteFont m_Font;
        private int m_ActiveItemIndex;
        private int m_MaxActiveItemIndex;
        private Vector2 m_ItemPosition = Vector2.Zero;
        private string m_MenuTitle;
        private Color m_InactiveColor = Color.Green;
        private Color m_ActiveColor = Color.Blue;
        private string r_MenuMoveSound;


        private bool HasMenuMoveSound { get; set; }


        public MenuScreen(Game i_Game, string i_MenuTitle)
            : base(i_Game)
        {
            m_MenuTitle = i_MenuTitle;
            m_MenuItem = new List<MenuItem>();
            m_AnimetedSpriteText = new List<AnimatedSpriteText>();
        }

        public MenuScreen(Game i_Game, string i_MenuTitle, string i_MenuMoveSound)
            : this(i_Game, i_MenuTitle)
        {
            r_MenuMoveSound = i_MenuMoveSound;
            HasMenuMoveSound = true;
        }

        protected event EventHandler<EventArgs> MenuMoved;

        protected void OnMenuMoved()
        {
            if (HasMenuMoveSound)
            {
                SoundManager.PlaySoundEffect(r_MenuMoveSound);
            }
            
            if (MenuMoved != null)
            {
                MenuMoved.Invoke(this, EventArgs.Empty);
            }
        }


        public override void Initialize()
        {
            base.Initialize();
            m_ActiveItemIndex = 0;
            m_MaxActiveItemIndex = m_MenuItem.Count - 1;
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            AnimatedSpriteText current = new AnimatedSpriteText(@"Fonts\Arial", i_MenuItem.Title, this);
            m_MenuItem.Add(i_MenuItem);
            m_AnimetedSpriteText.Add(current);
            current.Position = new Vector2(0, m_AnimetedSpriteText.Count * 30);
            current.TintColor = m_InactiveColor;
            current.TextValue = i_MenuItem.TitleValue;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            m_AnimetedSpriteText[m_ActiveItemIndex].Animations.Enabled = false;
            m_AnimetedSpriteText[m_ActiveItemIndex].TintColor = m_ActiveColor;

            if (InputManager.KeyPressed(Keys.Up) || InputManager.KeyPressed(Keys.Down))
            {
                m_AnimetedSpriteText[m_ActiveItemIndex].TintColor = m_InactiveColor;

                if (InputManager.KeyPressed(Keys.Up))
                {
                    m_ActiveItemIndex--;
                    m_ActiveItemIndex = (m_ActiveItemIndex >= 0) ? m_ActiveItemIndex : m_MaxActiveItemIndex;
                }

                if (InputManager.KeyPressed(Keys.Down))
                {
                    m_ActiveItemIndex++;
                    m_ActiveItemIndex = (m_ActiveItemIndex < m_MaxActiveItemIndex + 1) ? m_ActiveItemIndex : 0;
                }

                OnMenuMoved();
            }

            if (InputManager.KeyPressed(Keys.Enter))
            {
                m_MenuItem[m_ActiveItemIndex].EnterScreen(this);
            }

            if (InputManager.KeyPressed(Keys.PageUp))
            {
                string newValue = m_MenuItem[m_ActiveItemIndex].ItemSelected(this, Keys.PageUp);
                m_AnimetedSpriteText[m_ActiveItemIndex].TextValue = newValue;
                OnMenuMoved();
            }

            if (InputManager.KeyPressed(Keys.PageDown))
            {
                string newValue = m_MenuItem[m_ActiveItemIndex].ItemSelected(this, Keys.PageDown);
                m_AnimetedSpriteText[m_ActiveItemIndex].TextValue = newValue;
                OnMenuMoved();
            }

            m_AnimetedSpriteText[m_ActiveItemIndex].Animations.Enabled = true;
        }

        

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = ContentManager.Load<SpriteFont>(@"Fonts\Arial");
        }

        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch.Begin();
            Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch.DrawString(m_Font, m_MenuTitle, Vector2.Zero, Color.White);
            SpriteBatch.End();
            base.Draw(i_GameTime);
        }
    }
}