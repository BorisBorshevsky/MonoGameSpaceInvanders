//*** Guy Ronen (c) 2008-2011 ***//

using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel
{
    public class RegisteredComponent : GameComponent
    {
        protected GameScreen Screen { get; set; }
        public RegisteredComponent(Game i_Game, int i_UpdateOrder)
            : base(i_Game)
        {
            UpdateOrder = i_UpdateOrder;
            Game.Components.Add(this); // self-register as a coponent
        }

        public RegisteredComponent(GameScreen i_GameScreen, int i_UpdateOrder = int.MaxValue)
            : base(i_GameScreen.Game)
        {
            Screen = i_GameScreen;
            UpdateOrder = i_UpdateOrder;
            i_GameScreen.Add(this);
        }

        public RegisteredComponent(Game i_Game)
            : this(i_Game, int.MaxValue)
        {
        }
    }
}