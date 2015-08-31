using Infrastructure.Menu;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu.SoundOptionsMenuItems;

namespace SpaceInvaders.Menu
{
    class SoundOptionsMenu : SpaceInvadersMenuScreen
    {
        private readonly SoundEffectsItem r_SoundEffectsItem;
        private readonly ToggleSoundItem r_ToggleSoundItem;
        private readonly BackgroundMusicVolumItem r_BackgroundMusicVolumItem;
        private readonly BackItem r_BackItem;

        public SoundOptionsMenu(Game i_Game, IMenuConfiguration i_MenuConfiguration)
            : base(i_Game, "Sound Options", i_MenuConfiguration)
        {
            m_SoundManager = i_Game.Services.GetService<ISoundManager>();

            r_ToggleSoundItem = new ToggleSoundItem("Toggle Sound: ", this, i_MenuConfiguration);
            r_ToggleSoundItem.TitleValue = m_SoundManager.IsMute ? "Off" : "On";
            AddMenuItem(r_ToggleSoundItem);

            r_BackgroundMusicVolumItem = new BackgroundMusicVolumItem("Background Music Volume: ", this, i_MenuConfiguration);
            r_BackgroundMusicVolumItem.TitleValue = m_SoundManager.BackgroundVolumeLevel.ToString();
            AddMenuItem(r_BackgroundMusicVolumItem);

            r_SoundEffectsItem = new SoundEffectsItem("Sound Effects: ", this, i_MenuConfiguration);
            r_SoundEffectsItem.TitleValue = m_SoundManager.SoundsEffectsVolumeLevel.ToString();
            AddMenuItem(r_SoundEffectsItem);

            r_BackItem = new BackItem("Done", this, i_MenuConfiguration);
            AddMenuItem(r_BackItem);
        }
    }
}