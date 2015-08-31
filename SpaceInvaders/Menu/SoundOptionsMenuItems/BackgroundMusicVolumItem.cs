using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.SoundOptionsMenuItems
{
    class BackgroundMusicVolumItem : SpaceInvaderMenuItem
    {
        public BackgroundMusicVolumItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int currentVolume = i_GameScreen.SoundManager.BackgroundVolumeLevel;
            if (i_Key == Keys.PageUp)
            {
                if (currentVolume < 100)
                {
                    i_GameScreen.SoundManager.IncreaseBackGroundVolume();
                }
            }

            if (i_Key == Keys.PageDown)
            {
                if (currentVolume > 0)
                {
                    i_GameScreen.SoundManager.DecreaseBackGroundVolume();
                }
            }

            return i_GameScreen.SoundManager.BackgroundVolumeLevel.ToString();
        }
    }
}
