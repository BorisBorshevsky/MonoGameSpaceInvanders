using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        readonly Dictionary<string, SoundEffect> r_SoundEffects = new Dictionary<string, SoundEffect>();

        private int m_BackgroundMusicVolume = 10;
        private int m_SoundEffectsMusicVolume = 10;
        private bool m_Mute;

        public SoundManager(Game i_Game)
            : base(i_Game)
        {}

        public void IncreaseBackGroundVolume()
        {
            m_BackgroundMusicVolume = MathHelper.Clamp(m_BackgroundMusicVolume + 10, 0, 100);
            MediaPlayer.Volume = IsMute ? 0 : (float)m_BackgroundMusicVolume / 100;
        }

        public void DecreaseBackGroundVolume()
        {
            m_BackgroundMusicVolume = MathHelper.Clamp(m_BackgroundMusicVolume - 10, 0, 100);
            MediaPlayer.Volume = IsMute ? 0 : (float)m_BackgroundMusicVolume / 100;
        }

        public void ToggleMute()
        {
            m_Mute = !m_Mute;
            MediaPlayer.Volume = m_Mute ? 0 : (float) m_BackgroundMusicVolume / 100;
        }

        public void IncreaseSoundsEffectsVolume()
        {
            m_SoundEffectsMusicVolume = MathHelper.Clamp(m_SoundEffectsMusicVolume + 10, 0, 100);
        }

        public void DecreaseSoundsEffectsVolume()
        {
            m_SoundEffectsMusicVolume = MathHelper.Clamp(m_SoundEffectsMusicVolume - 10, 0, 100);
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

        public bool IsMute
        {
            get { return m_Mute; }
        }

        public void PlaySoundEffect(string i_Path)
        {
            if (!r_SoundEffects.ContainsKey(i_Path))
            {
                r_SoundEffects.Add(i_Path, Game.Content.Load<SoundEffect>(i_Path));
            }
            
            if (!m_Mute)
            {
                r_SoundEffects[i_Path].Play((float)m_SoundEffectsMusicVolume / 100, 0f, 0f);    
            }
            
        }

        public void PlayBackgroundMusic(string i_Path)
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                var song = Game.Content.Load<Song>(i_Path);
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = m_Mute ? 0 : (float)m_BackgroundMusicVolume / 100;
            }
            //todo: rami shit with music

        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ISoundManager), this);
        }
    }
}
