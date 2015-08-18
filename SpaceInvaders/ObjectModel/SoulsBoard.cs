using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.ObjectModel
{
    public class SoulsBoard : RegisteredComponent
    {
        public int SoulsCount { get; private set; }

        private readonly string r_AssetName;
        private readonly List<Sprite> r_Sprites = new List<Sprite>();
        private readonly int r_PlayerId;
        private const int k_RightOffset = 32;
        private const int k_TopOffset = 16;
        private const int k_DistanceBetweenSouls = 8;

        
        public SoulsBoard(Game i_Game, int i_InitialSoulsCount, string i_AssetName, int i_PlayerId) : base(i_Game)
        {
            SoulsCount = i_InitialSoulsCount;
            r_PlayerId = i_PlayerId;

            for (int i = 0; i < SoulsCount; i++)
            {
                r_Sprites.Add(new SoulIcon(i_AssetName, i_Game));    
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < SoulsCount; i++)
            {
                r_Sprites[i].Initialize();
                int rightBound = Game.GraphicsDevice.Viewport.Bounds.Width;
                float xPosition = rightBound - k_RightOffset - (i*(r_Sprites[i].Bounds.Width + k_DistanceBetweenSouls));
                float yPosition = k_TopOffset + (r_PlayerId*(r_Sprites[i].Bounds.Height + k_DistanceBetweenSouls));
                r_Sprites[i].Position = new Vector2(xPosition, yPosition);
            }
        }


        public void RemoveSoul()
        {
            r_Sprites[--SoulsCount].Visible = false;
        }
        
    }
}
