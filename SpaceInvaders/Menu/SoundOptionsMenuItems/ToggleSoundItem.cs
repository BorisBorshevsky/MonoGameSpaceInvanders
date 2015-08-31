using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.SoundOptionsMenuItems
{
    class ToggleSoundItem : SpaceInvaderMenuItem
    {
        public ToggleSoundItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            i_GameScreen.SoundManager.ToggleMute();

            return i_GameScreen.SoundManager.IsMute ? "Off" : "On";
        }
    }
}
