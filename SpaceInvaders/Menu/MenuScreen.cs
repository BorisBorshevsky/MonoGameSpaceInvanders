using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Animators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Configurations;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.Menu
{
    class MenuScreen : GameScreen
    {
        protected SpriteFont m_Font;

        protected readonly ISettingsManager r_SettingsManager;
        private readonly List<MenuItem> r_MenuItem;
        private readonly List<AnimatedSpriteText> r_AnimatedSpriteText;
        private readonly string r_MenuTitle;
        private readonly Color r_InactiveColor = Color.Green;
        private readonly Color r_ActiveColor = Color.Blue;

        private int m_ActiveItemIndex;
        private int m_MaxActiveItemIndex;
        
        public MenuScreen(Game i_Game, string i_MenuTitle)
            : base(i_Game)
        {
            r_MenuTitle = i_MenuTitle;
            r_MenuItem = new List<MenuItem>();
            r_AnimatedSpriteText = new List<AnimatedSpriteText>();
            r_SettingsManager = i_Game.Services.GetService<ISettingsManager>();
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ActiveItemIndex = 0;
            m_MaxActiveItemIndex = r_MenuItem.Count - 1;
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            AnimatedSpriteText current = new AnimatedSpriteText(@"Fonts\Arial", i_MenuItem.Title, this);
            r_MenuItem.Add(i_MenuItem);
            r_AnimatedSpriteText.Add(current);
            current.Position = new Vector2(0, r_AnimatedSpriteText.Count * 30);
            current.TintColor = r_InactiveColor;
            current.TextValue = i_MenuItem.TitleValue;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = false;
            r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_ActiveColor;

            if (InputManager.KeyPressed(Keys.Up) || InputManager.KeyPressed(Keys.Down))
            {
                r_AnimatedSpriteText[m_ActiveItemIndex].TintColor = r_InactiveColor;

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

                SoundManager.PlaySoundEffect(Sounds.k_MenuMove);
            }

            if (InputManager.KeyPressed(Keys.Enter))
            {
                r_MenuItem[m_ActiveItemIndex].EnterScreen(this);
            }

            if (InputManager.KeyPressed(Keys.PageUp))
            {
                string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, Keys.PageUp);
                SoundManager.PlaySoundEffect(Sounds.k_MenuMove);
                r_AnimatedSpriteText[m_ActiveItemIndex].TextValue = newValue;
            }

            if (InputManager.KeyPressed(Keys.PageDown))
            {
                string newValue = r_MenuItem[m_ActiveItemIndex].ItemSelected(this, Keys.PageDown);
                SoundManager.PlaySoundEffect(Sounds.k_MenuMove);
                r_AnimatedSpriteText[m_ActiveItemIndex].TextValue = newValue;
            }

            r_AnimatedSpriteText[m_ActiveItemIndex].Animations.Enabled = true;
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
            SpriteBatch.DrawString(m_Font, r_MenuTitle, Vector2.Zero, Color.White);
            SpriteBatch.End();

            base.Draw(i_GameTime);
        }
    }
}