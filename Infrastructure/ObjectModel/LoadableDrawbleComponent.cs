//*** Guy Ronen (c) 2008-2011 ***//

using System;
using Infrastructure.Common;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Infrastructure.ObjectModel
{

    public abstract class LoadableDrawableComponent : DrawableGameComponent
    {
        protected string m_AssetName;

        protected LoadableDrawableComponent(
            string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game)
        {
            AssetName = i_AssetName;
            UpdateOrder = i_UpdateOrder;
            DrawOrder = i_DrawOrder;

            // register in the game:
            Game.Components.Add(this);
        }

        protected LoadableDrawableComponent(string i_AssetName, Game i_Game, int i_CallsOrder)
            : this(i_AssetName, i_Game, i_CallsOrder, i_CallsOrder)
        {
        }

        // used to load the sprite:
        protected ContentManager ContentManager
        {
            get { return Game.Content; }
        }

        public string AssetName
        {
            get { return m_AssetName; }
            set { m_AssetName = value; }
        }

        public override void Initialize()
        {
            base.Initialize();

            if (this is ICollidable2D)
            {
                ICollisionManager collisionMaanager =
                    Game.Services.GetService(typeof (ICollisionManager)) as ICollisionManager;
                if (collisionMaanager != null)
                {
                    collisionMaanager.Add(this as ICollidable2D);
                }
            }

            InitBounds(); // a call to an abstract method;
        }

        protected abstract void InitBounds();
        public virtual event EventHandler<EventArgs> Disposed;
        public virtual event EventHandler<EventArgs> PositionChanged;
        public virtual event EventHandler<EventArgs> SizeChanged;

        protected void RaisePositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }

        protected void RaiseSizeChanged()
        {
            if (SizeChanged != null)
            {
                SizeChanged(this, EventArgs.Empty);
            }
        }

        protected void RaiseDisposed()
        {
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }
    }
}