using System;
using System.Collections.Generic;
using Infrastructure.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.Menu
{
    public class MenuScreen : GameScreen
    {
        protected const int k_ItemHeight = 30;

        protected SpriteFont m_Font;

        private readonly List<MenuItem> r_MenuItem;
        private readonly List<AnimatedSpriteText> r_AnimatedSpriteText;
        private readonly string r_MenuTitle;
        protected readonly IMenuConfiguration r_MenuConfiguration;

        private int m_ActiveItemIndex;
        private int m_MaxActiveItemIndex;
        
        public MenuScreen(Game i_Game, string i_MenuTitle, IMenuConfiguration i_MenuConfiguration)
            : base(i_Game)
        {
            r_MenuTitle = i_MenuTitle;
            r_MenuItem = new List<MenuItem>();
            r_AnimatedSpriteText = new List<AnimatedSpriteText>();
            r_MenuConfiguration = i_MenuConfiguration;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ActiveItemIndex = 0;
            m_MaxActiveItemIndex = r_MenuItem.Count - 1;
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            AnimatedSpriteText current = new AnimatedSpriteText(r_MenuConfiguration.MenuFontAssetName, i_MenuItem.Title, this);
            r_MenuItem.Add(i_MenuItem);
            r_AnimatedSpriteText.Add(current);
            current.Position = new Vector2(0, r_AnimatedSpriteText.Count * k_ItemHeight);
            current.TintColor = r_MenuConfiguration.InActiveColor;
            current.TextValue = i_MenuItem.TitleValue;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            handleMouse();
            handleKeyboard();
        }

        private void handleMouse()
        {
            foreach (var animatedSpriteText in r_AnimatedSpriteText)
            {
                var mouseRectangle = new Rectangle(InputManager.MouseState.X, InputManager.MouseState.Y, 1, 1);
                if (animatedSpriteText.Bounds.Intersects(mouseRectangle))
                {
                    r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = false;
                    r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_MenuConfiguration.InActiveColor;
                    m_ActiveItemIndex = r_AnimatedSpriteText.IndexOf(animatedSpriteText);
                    r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = true;
                    r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_MenuConfiguration.ActiveColor;

                    handleMouseClick();
                }
            }
        }

        private void handleMouseClick()
        {
            if (InputManager.ButtonPressed(eInputButtons.Right))
            {
                string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, r_MenuConfiguration.ScrollDownKey);
                updateMenuTextValue(newValue);
            }
            else
            {
                if (InputManager.ButtonPressed(eInputButtons.Left))
                {
                    string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, r_MenuConfiguration.ScrollUpKey);
                    updateMenuTextValue(newValue);
                    r_MenuItem[m_ActiveItemIndex].EnterScreen(this);
                }
            }
        }

        private void updateMenuTextValue(string i_Value)
        {
            SoundManager.PlaySoundEffect(r_MenuConfiguration.MenuMoveSoundAssetName);
            r_AnimatedSpriteText[m_ActiveItemIndex].TextValue = i_Value;
        }

        private void handleKeyboard()
        {
            r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = false;
            r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_MenuConfiguration.ActiveColor;

            if (InputManager.KeyPressed(r_MenuConfiguration.MoveUpKey) ||
                InputManager.KeyPressed(r_MenuConfiguration.MoveDownKey))
            {
                r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_MenuConfiguration.InActiveColor;

                if (InputManager.KeyPressed(r_MenuConfiguration.MoveUpKey))
                {
                    m_ActiveItemIndex--;
                    m_ActiveItemIndex = (m_ActiveItemIndex >= 0) ? m_ActiveItemIndex : m_MaxActiveItemIndex;
                }

                if (InputManager.KeyPressed(r_MenuConfiguration.MoveDownKey))
                {
                    m_ActiveItemIndex++;
                    m_ActiveItemIndex = (m_ActiveItemIndex < m_MaxActiveItemIndex + 1) ? m_ActiveItemIndex : 0;
                }

                SoundManager.PlaySoundEffect(r_MenuConfiguration.MenuMoveSoundAssetName);
            }

            if (InputManager.KeyPressed(r_MenuConfiguration.EnterKey))
            {
                r_MenuItem[m_ActiveItemIndex].EnterScreen(this);
            }

            if (InputManager.KeyPressed(r_MenuConfiguration.ScrollUpKey))
            {
                string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, r_MenuConfiguration.ScrollUpKey);
                SoundManager.PlaySoundEffect(r_MenuConfiguration.MenuMoveSoundAssetName);
                r_AnimatedSpriteText[m_ActiveItemIndex].TextValue = newValue;
            }

            if (InputManager.KeyPressed(r_MenuConfiguration.ScrollDownKey))
            {
                string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, r_MenuConfiguration.ScrollDownKey);
                SoundManager.PlaySoundEffect(r_MenuConfiguration.MenuMoveSoundAssetName);
                r_AnimatedSpriteText[m_ActiveItemIndex].TextValue = newValue;
            }

            r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Font = ContentManager.Load<SpriteFont>(r_MenuConfiguration.MenuFontAssetName);
        }

        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch.Begin();
            Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch.DrawString(m_Font, r_MenuTitle, Vector2.Zero, r_MenuConfiguration.MenuHeadColor);
            SpriteBatch.End();

            base.Draw(i_GameTime);
        }
    }
}