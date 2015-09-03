using System;
using Infrastructure.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.Animators
{
    public class AnimatedSpriteText : Sprite
    {
        private SpriteAnimator m_PulseAnimator;
        private readonly string r_Text;
        private string m_TextValue = string.Empty;

        public AnimatedSpriteText(string i_AssetName, string i_Text, GameScreen i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        {
            r_Text = i_Text + m_TextValue;
        }

        public string TextValue
        {
            set {m_TextValue = value;}
        }

        protected override void LoadContent()
        {
            SpriteFont = Game.Content.Load<SpriteFont>(m_AssetName);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            m_PulseAnimator = new PulseAnimator("PulseAnimator", new TimeSpan(), 1f, 1f);
            this.Animations.Add(m_PulseAnimator);
            this.Animations.Enabled = false;
        }

        protected override void InitBounds()
        {
            m_WidthBeforeScale = SpriteFont.MeasureString(r_Text).X;
            m_HeightBeforeScale = SpriteFont.MeasureString(r_Text).Y;
            InitSourceRectangle();
            InitOrigins();
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.DrawString(SpriteFont, displayText(), m_Position, this.TintColor, this.Rotation, this.RotationOrigin, this.Scales, SpriteEffects.None, this.LayerDepth);
        }

        private string displayText()
        {
            return string.Format("{0} {1}", r_Text, m_TextValue);
        }

        public SpriteFont SpriteFont { get; set; }
    }
}
