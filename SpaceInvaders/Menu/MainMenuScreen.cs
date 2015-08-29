using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menu;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private PlayItem m_PlayItem;
        private SoundOptionsItem m_SoundOptionsItem;
        private ChoosePlayersItem m_ChoosePlayersItem;
        private ScreenOptionsItem m_ScreenOptionsItem;
        private QuitGameItem m_QuitItem;
        private SettingsManager m_SettingsManager;

        public MainMenuScreen(Game i_Game)
            : base(i_Game, "Main Menu")
        {
            m_SettingsManager = (SettingsManager)Game.Services.GetService(typeof(ISettingsManager));
            
            m_ScreenOptionsItem = new ScreenOptionsItem("Screen Options", this);
            AddMenuItem(m_ScreenOptionsItem);

            m_ChoosePlayersItem = new ChoosePlayersItem("Players: ", this);
            m_ChoosePlayersItem.TitleValue = m_SettingsManager.NumOfPlayers == 1 ? "One" : "Two";
            AddMenuItem(m_ChoosePlayersItem);

            m_SoundOptionsItem = new SoundOptionsItem("Sound Options", this);
            AddMenuItem(m_SoundOptionsItem);
            
            m_PlayItem = new PlayItem("Play", this);
            AddMenuItem(m_PlayItem);

            m_QuitItem = new QuitGameItem("Quit", this);
            AddMenuItem(m_QuitItem);
        }
    }
}
