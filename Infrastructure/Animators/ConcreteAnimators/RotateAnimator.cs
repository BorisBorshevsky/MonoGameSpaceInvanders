//*** Guy Ronen © 2008-2011 ***//

using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.Animators.ConcreteAnimators
{
    public class RotateAnimator : SpriteAnimator
    {
       private readonly float r_RotationVelocity;

        public RotateAnimator(string i_Name, float i_RotationVelocity, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.r_RotationVelocity = i_RotationVelocity;
        }

        public RotateAnimator(float i_RotationVelocity, TimeSpan i_AnimationLength)
            : this("Rotate", i_RotationVelocity, i_AnimationLength)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            BoundSprite.RotationOrigin = BoundSprite.SourceRectangleCenter;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.BoundSprite.Rotation += (float)i_GameTime.ElapsedGameTime.TotalSeconds * r_RotationVelocity;
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
        }
    }
}
