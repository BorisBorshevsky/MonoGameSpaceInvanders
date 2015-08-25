using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.Managers
{
    public class DummyInputManager : IInputManager
    {
        public GamePadState GamePadState { get; private set; }
        public KeyboardState KeyboardState { get; private set; }
        public MouseState MouseState { get; private set; }
        public Vector2 MousePositionDelta { get; private set; }
        public int ScrollWheelDelta { get; private set; }
        public Vector2 LeftThumbDelta { get; private set; }
        public Vector2 RightThumbDelta { get; private set; }
        public float LeftTrigerDelta { get; private set; }
        public float RightTrigerDelta { get; private set; }

        public bool ButtonIsDown(eInputButtons i_MouseButtons)
        {
            return false;
        }

        public bool ButtonIsUp(eInputButtons i_MouseButtons)
        {
            return false;
        }

        public bool ButtonsAreDown(eInputButtons i_MouseButtons)
        {
            return false;
        }

        public bool ButtonsAreUp(eInputButtons i_MouseButtons)
        {
            return false;
        }

        public bool ButtonPressed(eInputButtons i_Buttons)
        {
            return false;
        }

        public bool ButtonReleased(eInputButtons i_Buttons)
        {
            return false;
        }

        public bool ButtonsPressed(eInputButtons i_Buttons)
        {
            return false;
        }

        public bool ButtonsReleased(eInputButtons i_Buttons)
        {
            return false;
        }

        public bool KeyPressed(Keys i_Key)
        {
            return false;
        }

        public bool KeyReleased(Keys i_Key)
        {
            return false;
        }

        public bool KeyHeld(Keys i_Key)
        {
            return false;
        }
    }
}
