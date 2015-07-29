﻿using System;
using System.Collections.Generic;
using Infrastructure.Common;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace Infrastructure.Managers
{
    public class CollisionManager : GameService, ICollisionManager
    {
        protected readonly List<ICollidable2D> r_ChnagedCollidables = new List<ICollidable2D>();
        protected readonly List<ICollidable2D> r_Collidables = new List<ICollidable2D>();

        public CollisionManager(Game i_Game)
            : base(i_Game, int.MaxValue)
        {
        }

        public void Add(ICollidable2D i_Collidable2D)
        {
            if (!r_Collidables.Contains(i_Collidable2D))
            {
                r_Collidables.Add(i_Collidable2D);
                i_Collidable2D.VisibleChanged += collidabe_Changed;
                i_Collidable2D.SizeChanged += collidabe_Changed;
                i_Collidable2D.PositionChanged += collidabe_Changed;
                i_Collidable2D.Disposed += onCollidabeleDisposed;
            }
        }

        public void Remove(ICollidable2D i_Collidable2D)
        {
            r_Collidables.Remove(i_Collidable2D);
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof (ICollisionManager), this);
        }

        private void onCollidabeleDisposed(object i_Sender, EventArgs i_Args)
        {
            ICollidable2D collidable2D = i_Sender as ICollidable2D;
            if (collidable2D != null)
            {
                collidable2D.VisibleChanged -= collidabe_Changed;
                collidable2D.SizeChanged -= collidabe_Changed;
                collidable2D.PositionChanged -= collidabe_Changed;
                collidable2D.Disposed -= onCollidabeleDisposed;
                r_Collidables.Remove(collidable2D);
            }
        }

        // function colidable changed (add listeners)
        //function collidable disposed (remove listeners)

        private void collidabe_Changed(object i_Sender, EventArgs i_Args)
        {
            ICollidable2D collidable2D = i_Sender as ICollidable2D;
            r_ChnagedCollidables.Add(collidable2D);
        }

        private void checkCollisions(ICollidable2D i_CurrentCollidable2D)
        {
            List<ICollidable2D> colidedWithList = new List<ICollidable2D>();

            if (i_CurrentCollidable2D != null)
            {
                r_Collidables.ForEach(i_IteratedCollidable =>
                {
                    if (i_IteratedCollidable != i_CurrentCollidable2D)
                    {
                        if (i_CurrentCollidable2D.CollidesWith(i_IteratedCollidable))
                        {
                            colidedWithList.Add(i_IteratedCollidable);
                        }
                    }
                });

                if (colidedWithList.Count > 0)
                {
                    colidedWithList.ForEach(i_IteratedCollidable =>
                    {
                        i_IteratedCollidable.Collided(i_CurrentCollidable2D);
                        i_CurrentCollidable2D.Collided(i_IteratedCollidable);
                    });
                }
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            r_ChnagedCollidables.ForEach(checkCollisions);
            r_ChnagedCollidables.Clear();

            base.Update(i_GameTime);
        }
    }
}