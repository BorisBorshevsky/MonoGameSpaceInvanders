using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.ObjectModel
{
    class SoulsBoard : RegisteredComponent
    {
        public IPlayerState PlayerState { get; private set; }

        private readonly List<Sprite> r_Sprites = new List<Sprite>();
        private readonly int r_PlayerId;
        private const int k_RightOffset = 32;
        private const int k_TopOffset = 16;
        private const int k_DistanceBetweenSouls = 8;

        public SoulsBoard(GameScreen i_GameScreen, IPlayerState i_PlayerState, string i_AssetName, int i_PlayerId)
            : base(i_GameScreen)
        {
            PlayerState = i_PlayerState;
            r_PlayerId = i_PlayerId;

            for (int i = 0; i < PlayerState.Lives; i++)
            {
                r_Sprites.Add(new SoulIcon(i_AssetName, i_GameScreen));
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int index = 0; index < PlayerState.Lives; index++)
            {
                r_Sprites[index].Initialize();
                int rightBound = Game.GraphicsDevice.Viewport.Bounds.Width;
                float xPosition = rightBound - k_RightOffset - (index*(r_Sprites[index].Bounds.Width + k_DistanceBetweenSouls));
                float yPosition = k_TopOffset + (r_PlayerId*(r_Sprites[index].Bounds.Height + k_DistanceBetweenSouls));
                r_Sprites[index].Position = new Vector2(xPosition, yPosition);
            }
        }

        public void RemoveSoul()
        {
            r_Sprites[--PlayerState.Lives].Visible = false;
        }

        protected override void Dispose(bool i_Disposing)
        {
            if (i_Disposing)
            {
                foreach (var sprite in r_Sprites)
                {
                    sprite.Dispose();
                }
            }
            base.Dispose(i_Disposing);
        }
    }
}