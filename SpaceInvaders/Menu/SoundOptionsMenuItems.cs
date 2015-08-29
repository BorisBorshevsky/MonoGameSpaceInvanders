using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public class SoundEffectsItem : MenuItem
    {
        public SoundEffectsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int currVolume = i_GameScreen.SoundManager.SoundsEffectsVolumeLevel;
            if (i_Key == Keys.PageUp)
            {
                if (currVolume < 100)
                {
                    i_GameScreen.SoundManager.IncreaseSoundsEffectsVolume();
                }
            }

            if (i_Key == Keys.PageDown)
            {
                if (currVolume > 0)
                {
                    i_GameScreen.SoundManager.DecreaseSoundsEffectsVolume();
                }

            }

            return i_GameScreen.SoundManager.SoundsEffectsVolumeLevel.ToString();
        }
    }

    public class ToggleSoundItem : MenuItem
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

    public class BackgrounMusicVolumItem : MenuItem
    {
        public BackgrounMusicVolumItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int currVolume = i_GameScreen.SoundManager.BackgroundVolumeLevel;
            if (i_Key == Keys.PageUp)
            {
                if (currVolume < 100)
                {
                    i_GameScreen.SoundManager.IncreaseBackGroundVolume();
                }
            }

            if (i_Key == Keys.PageDown)
            {
                if (currVolume > 0)
                {
                    i_GameScreen.SoundManager.DecreaseBackGroundVolume();
                }
            }

            return i_GameScreen.SoundManager.BackgroundVolumeLevel.ToString();
        }
    }

    public class DoneItem : MenuItem
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