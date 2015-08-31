using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.SoundOptionsMenuItems
{
    class BackgroundMusicVolumItem : SpaceInvaderMenuItem
    {
        public BackgroundMusicVolumItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int currentVolume = i_GameScreen.SoundManager.BackgroundVolumeLevel;
            if (i_Key == r_MenuConfiguration.ScrollUpKey)
            {
                i_GameScreen.SoundManager.IncreaseBackGroundVolume();
            }

            if (i_Key == r_MenuConfiguration.ScrollDownKey)
            {
                i_GameScreen.SoundManager.DecreaseBackGroundVolume();
            }

            return i_GameScreen.SoundManager.BackgroundVolumeLevel.ToString();
        }
    }
}
