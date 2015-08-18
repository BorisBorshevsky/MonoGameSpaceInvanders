using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.ObjectModel.Sprites;

namespace SpaceInvaders.ObjectModel
{
    class BarrierComposer : RegisteredComponent
    {
        private const int k_GapBetweenBarriers = 1;
        private const int k_NumberOfBarriers = 4;
        private const int k_BottomOffset = 50;
        readonly List<Barrier> r_Barriers = new List<Barrier>();

        public BarrierComposer(Game i_Game) : base(i_Game)
        {
            generateBarriers();
        }

        private void setBarrierInitialPosition(Barrier i_Barrier, int i_BarrierIndex)
        {
            int y = Game.GraphicsDevice.Viewport.Height - k_BottomOffset - (2 * i_Barrier.Bounds.Height);
            int x = (Game.GraphicsDevice.Viewport.Width / 2 - getAllBarriersWidth() / 2 + (i_BarrierIndex * (k_GapBetweenBarriers + 1)) * i_Barrier.Bounds.Width) - i_Barrier.Bounds.Width / 4;
            i_Barrier.Position = new Vector2(x,y);
        }

        private int getAllBarriersWidth()
        {
            Barrier firstBarrier = r_Barriers[0];
            return firstBarrier.Bounds.Width * (r_Barriers.Count) + (k_GapBetweenBarriers) * (firstBarrier.Bounds.Width) * (r_Barriers.Count - 1);
        }

        public override void Initialize()
        {
            r_Barriers.ForEach(i_Barrier => i_Barrier.Initialize());
            
            for (int i = 0; i < r_Barriers.Count(); i++)
            {
                setBarrierInitialPosition(r_Barriers[i], i); 
            }
            
            base.Initialize();
        }


        private void generateBarriers()
        {
            for (int i = 0; i < k_NumberOfBarriers; i++)
            {
                r_Barriers.Add(new Barrier(Game));
            }
        }
    }
}
