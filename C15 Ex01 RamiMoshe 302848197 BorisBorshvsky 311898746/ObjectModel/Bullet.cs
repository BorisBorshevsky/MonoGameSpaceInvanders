using Microsoft.Xna.Framework;

namespace SpaceInvaders.ObjectModel
{
	using Infrastructure.ObjectModel;

    public class Bullet : Sprite
	{
		private const string k_AssteName = @"Sprites\Bullet";

        public Bullet(Game i_Game)
			: base(k_AssteName, i_Game)
		{
		}
    }
}
