using Infrastructure;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu.SoundOptionsMenuItems;

namespace SpaceInvaders.Menu
{
    class SoundOptionsMenu : MenuScreen
    {
        private SoundEffectsItem m_SoundEffectsItem;
        private ToggleSoundItem m_ToggleSoundItem;
        private BackgroundMusicVolumItem r_MBackgroundMusicVolumItem;
        private DoneItem m_DoneItem;

        public SoundOptionsMenu(Game i_Game)
            : base(i_Game, "Sound Options")
        {
            m_SoundManager = i_Game.Services.GetService<ISoundManager>();

            m_ToggleSoundItem = new ToggleSoundItem("Toggle Sound: ", this);
            m_ToggleSoundItem.TitleValue = m_SoundManager.IsMute ? "Off" : "On";
            AddMenuItem(m_ToggleSoundItem);

            r_MBackgroundMusicVolumItem = new BackgroundMusicVolumItem("Background Music Volume: ", this);
            r_MBackgroundMusicVolumItem.TitleValue = m_SoundManager.BackgroundVolumeLevel.ToString();
            AddMenuItem(r_MBackgroundMusicVolumItem);

            m_SoundEffectsItem = new SoundEffectsItem("Sound Effects: ", this);
            m_SoundEffectsItem.TitleValue = m_SoundManager.SoundsEffectsVolumeLevel.ToString();
            AddMenuItem(m_SoundEffectsItem);

            m_DoneItem = new DoneItem("Done", this);
            AddMenuItem(m_DoneItem);
        }
    }
}