//*** Guy Ronen © 2008-2011 ***//

using System;
using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace Infrastructure.Animators
{
    public class CompositeAnimator : SpriteAnimator
    {
        private readonly Dictionary<string, SpriteAnimator> r_AnimationsDictionary = new Dictionary<string, SpriteAnimator>();

        protected readonly List<SpriteAnimator> r_AnimationsList = new List<SpriteAnimator>();

        public CompositeAnimator(Sprite i_BoundSprite)
            : this("AnimationsMamager", TimeSpan.Zero, i_BoundSprite)
        {
            this.Enabled = false;
            this.Initialize();
        }

        public CompositeAnimator(string i_Name, TimeSpan i_AnimationLength, Sprite i_BoundSprite, params SpriteAnimator[] i_Animations)
            : base(i_Name, i_AnimationLength)
        {
            this.BoundSprite = i_BoundSprite;
            this.Initialize();
            foreach (SpriteAnimator animation in i_Animations)
            {
                this.Add(animation);
            }
        }

        public void Add(SpriteAnimator i_Animation)
        {
            i_Animation.BoundSprite = this.BoundSprite;
            i_Animation.Enabled = true;
            r_AnimationsDictionary.Add(i_Animation.Name, i_Animation);
            r_AnimationsList.Add(i_Animation);
        }

        public void Remove(string i_AnimationName)
        {
            SpriteAnimator animationToRemove;
            r_AnimationsDictionary.TryGetValue(i_AnimationName, out animationToRemove);
            if (animationToRemove != null)
            {
                r_AnimationsDictionary.Remove(i_AnimationName);
                r_AnimationsList.Remove(animationToRemove);
            }
        }

        public SpriteAnimator this[string i_Name]
        {
            get
            {
                SpriteAnimator retVal = null;
                r_AnimationsDictionary.TryGetValue(i_Name, out retVal);
                return retVal;
            }
        }

        public override void Restart()
        {
            base.Restart();

            foreach (SpriteAnimator animation in r_AnimationsList)
            {
                animation.Restart();
            }
        }

        public override void Restart(TimeSpan i_AnimationLength)
        {
            base.Restart(i_AnimationLength);

            foreach (SpriteAnimator animation in r_AnimationsList)
            {
                animation.Restart();
            }
        }

        protected override void RevertToOriginal()
        {
            foreach (SpriteAnimator animation in r_AnimationsList)
            {
                animation.Reset();
            }
        }

        protected override void CloneSpriteInfo()
        {
            base.CloneSpriteInfo();

            foreach (SpriteAnimator animation in r_AnimationsList)
            {
                animation.m_OriginalSpriteInfo = m_OriginalSpriteInfo;
            }
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            foreach (SpriteAnimator animation in r_AnimationsList)
            {
                animation.Update(i_GameTime);
            }
        }

    }
}
