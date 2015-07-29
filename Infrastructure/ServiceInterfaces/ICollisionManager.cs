using Infrastructure.Common;

namespace Infrastructure.ServiceInterfaces
{
    public interface ICollisionManager
    {
        void Add(ICollidable2D i_Collidable2D);
        void Remove(ICollidable2D i_Collidable2D);
    }
}