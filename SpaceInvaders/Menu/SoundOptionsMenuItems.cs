using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu
{
    class SoundEffectsItem : MenuItem
    {
        public SoundEffectsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int currentVolume = i_GameScreen.SoundManager.SoundsEffectsVolumeLevel;
            if (i_Key == Keys.PageUp)
            {
                if (currentVolume < 100)
                {
                    i_GameScreen.SoundManager.IncreaseSoundsEffectsVolume();
                }
            }

            if (i_Key == Keys.PageDown)
            {
                if (currentVolume > 0)
                {
                    i_GameScreen.SoundManager.DecreaseSoundsEffectsVolume();
                }
            }

            return i_GameScreen.SoundManager.SoundsEffectsVolumeLevel.ToString();
        }
    }

    class ToggleSoundItem : MenuItem
    {
        public ToggleSoundItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            i_GameScreen.SoundManager.ToggleMute();

            return i_GameScreen.SoundManager.IsMute ? "Off" : "On";
        }
    }

    class BackgroundMusicVolumItem : MenuItem
    {
        public BackgroundMusicVolumItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

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

    class DoneItem : MenuItem
    {
        public DoneItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ExitScreen();
        }
    }
}