﻿namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Audio;

    public interface ISoundManager
    {
        void IncreaseBackGroundVolume();
        
        void DecreaseBackGroundVolume();
        
        void ToggleMute();
        
        void IncreaseSoundsEffectsVolume();
        
        void DecreaseSoundsEffectsVolume();

        int BackgroundVolumeLevel { get; set; }

        int SoundsEffectsVolumeLevel { get; set; }

        bool IsMute { get; }

        void PlaySoundEffect(string i_Path);

        void PlayBackgroundMusic(string i_Path);

    }

}
