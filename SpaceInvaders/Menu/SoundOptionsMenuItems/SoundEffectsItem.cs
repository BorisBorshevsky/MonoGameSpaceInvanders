using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.SoundOptionsMenuItems
{
    class SoundEffectsItem : SpaceInvaderMenuItem
    {
        public SoundEffectsItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            if (i_Key == r_MenuConfiguration.ScrollUpKey)
            {
                    i_GameScreen.SoundManager.IncreaseSoundsEffectsVolume();
            }

            if (i_Key == r_MenuConfiguration.ScrollDownKey)
            {
                    i_GameScreen.SoundManager.DecreaseSoundsEffectsVolume();
            }

            return i_GameScreen.SoundManager.SoundsEffectsVolumeLevel.ToString();
        }
    }
}
