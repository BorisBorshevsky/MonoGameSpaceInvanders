using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    public class SoundOptionsMenu : MenuScreen
    {
        private SoundEffectsItem m_SoundEffectsItem;
        private ToggleSoundItem m_ToggleSoundItem;
        private BackgrounMusicVolumItem m_BackgrounMusicVolumItem;
        private DoneItem m_DoneItem;
        private SoundManager m_SoundManager;

        public SoundOptionsMenu(Game i_Game)
            : base(i_Game, "Sound Options")
        {
            m_SoundManager = (SoundManager)this.Game.Services.GetService(typeof(ISoundManager));

            m_ToggleSoundItem = new ToggleSoundItem("Toggle Sound: ", this);
            m_ToggleSoundItem.TitleValue = m_SoundManager.IsMute ? "Off" : "On";
            AddMenuItem(m_ToggleSoundItem);

            m_BackgrounMusicVolumItem = new BackgrounMusicVolumItem("Background Music Volume: ", this);
            m_BackgrounMusicVolumItem.TitleValue = m_SoundManager.BackgroundVolumeLevel.ToString();
            AddMenuItem(m_BackgrounMusicVolumItem);

            m_SoundEffectsItem = new SoundEffectsItem("Sound Effects: ", this);
            m_SoundEffectsItem.TitleValue = m_SoundManager.SoundsEffectsVolumeLevel.ToString();
            AddMenuItem(m_SoundEffectsItem);

            m_DoneItem = new DoneItem("Done", this);
            AddMenuItem(m_DoneItem);
        }
    }
}