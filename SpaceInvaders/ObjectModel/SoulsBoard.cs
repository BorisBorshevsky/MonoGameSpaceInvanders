using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.ObjectModel
{
    public class SoulsBoard : RegisteredComponent
    {
        private int m_SoulsCount;

        private readonly string r_AssetName;
        private readonly List<Sprite> r_Sprites = new List<Sprite>();
        private readonly int r_PlayerId;


        public SoulsBoard(Game i_Game, int i_InitialSoulsCount, string i_AssetName, int i_PlayerId) : base(i_Game)
        {
            m_SoulsCount = i_InitialSoulsCount;
            r_PlayerId = i_PlayerId;

            for (int i = 0; i < m_SoulsCount; i++)
            {
                r_Sprites.Add(new Sprite(i_AssetName, i_Game));    
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < m_SoulsCount; i++)
            {
                r_Sprites[i].Initialize();
                r_Sprites[i].Opacity = 0.5f;
                r_Sprites[i].Scales = Vector2.One/2;
                r_Sprites[i].Position = new Vector2(Game.GraphicsDevice.Viewport.Bounds.Width - 100 - ((i + 1) * r_Sprites[i].Bounds.Width) , (r_PlayerId + 1) * r_Sprites[i].Bounds.Height + 20);
            }
        }

        public void RemoveSoul()
        {
            r_Sprites[--m_SoulsCount].Visible = false;
        }
    }
}
