//*** Guy Ronen (c) 2008-2011 ***//
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace Infrastructure.ObjectModel
{
    public abstract class LoadableDrawableComponent : DrawableGameComponent
    {
        protected string m_AssetName;

        // used to load the sprite:
        protected ContentManager ContentManager
        {
            get { return this.Game.Content; }
        }

        public string AssetName
        {
            get { return m_AssetName; }
            set { m_AssetName = value; }
        }

        public LoadableDrawableComponent(
            string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_Game)
        {
            this.AssetName = i_AssetName;
            this.UpdateOrder = i_UpdateOrder;
            this.DrawOrder = i_DrawOrder;

            // register in the game:
            this.Game.Components.Add(this);
        }

        public LoadableDrawableComponent(string i_AssetName, Game i_Game, int i_CallsOrder)
            : this(i_AssetName, i_Game, i_CallsOrder, i_CallsOrder)
        { }

        public override void Initialize()
        {
            base.Initialize();

            // After everything is loaded and initialzied,
            // lets init graphical aspects:
            InitBounds();   // a call to an abstract method;
        }

        protected abstract void InitBounds();
    
    
}
}