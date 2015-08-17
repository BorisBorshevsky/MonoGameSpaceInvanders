using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.Animators.ConcreteAnimators
{
    public class FadeAnimator : SpriteAnimator
    {
        private readonly float r_FadeFactorPerSecond;

        public FadeAnimator(string i_Name, float i_FadeFactorPerSecond, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            r_FadeFactorPerSecond = i_FadeFactorPerSecond;
        }

        public FadeAnimator(float i_FadeFactorPerSecond, TimeSpan i_AnimationLength)
            : base("Fade", i_AnimationLength)
        {
            r_FadeFactorPerSecond = i_FadeFactorPerSecond;
        }


        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Opacity -= r_FadeFactorPerSecond * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
