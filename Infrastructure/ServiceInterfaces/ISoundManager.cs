namespace Infrastructure.ServiceInterfaces
{
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
