using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.Common
{
    public interface ICollidable2D
    {
        bool Visible { get; }
        //size changed
        //position changed
        //visible changed
        //visible : bool
        //dispose 
        Rectangle BoundingRect { get; }
        Vector2 Velocity { get; }
        bool CollidesWith(ICollidable2D i_Collidable);
        void Collided(ICollidable2D i_Collidable);
        event EventHandler<EventArgs> PositionChanged;
        event EventHandler<EventArgs> VisibleChanged;
        event EventHandler<EventArgs> SizeChanged;
        event EventHandler<EventArgs> Disposed;
    }
}