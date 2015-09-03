using System.Collections.Generic;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        private readonly Dictionary<string, SoundEffect> r_SoundEffects = new Dictionary<string, SoundEffect>();

        private Song m_BackgroundSong;
        private int m_BackgroundMusicVolume = 50; //initial value
        private int m_SoundEffectsMusicVolume = 50; //initial value

        public SoundManager(Game i_Game)
            : base(i_Game)
        { }

        public void IncreaseBackGroundVolume()
        {
            m_BackgroundMusicVolume = m_BackgroundMusicVolume == 100 ? 0 : MathHelper.Clamp(m_BackgroundMusicVolume + 10, 0, 100);
            MediaPlayer.Volume = IsMute ? 0 : (float)m_BackgroundMusicVolume / 100;
        }

        public void DecreaseBackGroundVolume()
        {
            m_BackgroundMusicVolume = m_BackgroundMusicVolume == 0 ? 0 : MathHelper.Clamp(m_BackgroundMusicVolume + -10, 0, 100);
            MediaPlayer.Volume = IsMute ? 0 : (float)m_BackgroundMusicVolume / 100;
        }

        public void ToggleMute()
        {
            IsMute = !IsMute;
            MediaPlayer.Volume = IsMute ? 0 : (float) m_BackgroundMusicVolume / 100;
        }

        public void IncreaseSoundsEffectsVolume()
        {
            m_SoundEffectsMusicVolume = m_SoundEffectsMusicVolume == 100 ? 0 : MathHelper.Clamp(m_SoundEffectsMusicVolume + 10, 0, 100);
        }

        public void DecreaseSoundsEffectsVolume()
        {
            m_SoundEffectsMusicVolume = m_SoundEffectsMusicVolume == 0 ? 100 : MathHelper.Clamp(m_SoundEffectsMusicVolume - 10, 0, 100);
        }

        public int BackgroundVolumeLevel
        {
            get { return m_BackgroundMusicVolume; }
            set
            {
                m_BackgroundMusicVolume = MathHelper.Clamp(value, 0, 100);
            }
        }

        public int SoundsEffectsVolumeLevel
        {
            get { return m_SoundEffectsMusicVolume; }
            set { m_SoundEffectsMusicVolume = MathHelper.Clamp(value, 0, 100); }
        }

        public bool IsMute { get; private set; }

        public void PlaySoundEffect(string i_Path)
        {
            if (!r_SoundEffects.ContainsKey(i_Path))
            {
                r_SoundEffects.Add(i_Path, Game.Content.Load<SoundEffect>(i_Path));
            }
            
            if (!IsMute)
            {
                r_SoundEffects[i_Path].Play((float)m_SoundEffectsMusicVolume / 100, 0f, 0f);    
            }    
        }

        public void PlayBackgroundMusic(string i_Path)
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                Song backgroundSong = getBackgroundSong(i_Path);
                MediaPlayer.Play(backgroundSong);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = IsMute ? 0 : (float)m_BackgroundMusicVolume / 100;
            }
        }

        private Song getBackgroundSong(string i_Path)
        {
            return m_BackgroundSong ?? (m_BackgroundSong = Game.Content.Load<Song>(i_Path));
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ISoundManager), this);
        }
    }
}