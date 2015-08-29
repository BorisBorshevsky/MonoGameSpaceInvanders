using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel.Managers;

namespace SpaceInvaders
{
    public class ScreenOptionsMenu : MenuScreen
    {
        private WindowResizingItem m_WindowResizingItem;
        private FullScreenModeItem m_FullScreenModeItem;
        private MouseVisabilityItem m_MouseVisabilityItem;
        private DoneItem m_DoneItem;
        private SettingsManager m_SettingsManager;

        public ScreenOptionsMenu(Game i_Game)
            : base(i_Game, "Screen Options")
        {
            m_SettingsManager = (SettingsManager)Game.Services.GetService(typeof(ISettingsManager));

            m_MouseVisabilityItem = new MouseVisabilityItem("Mouse Visability: ", this);
            m_MouseVisabilityItem.TitleValue = m_SettingsManager.IsMouseVisible ? "Visible" : "Invisible";
            AddMenuItem(m_MouseVisabilityItem);

            m_FullScreenModeItem = new FullScreenModeItem("Full Screen Mode: ", this);
            m_FullScreenModeItem.TitleValue = m_SettingsManager.FullScreenMode ? "On" : "Off";
            AddMenuItem(m_FullScreenModeItem);

            m_WindowResizingItem = new WindowResizingItem("Allow Window Resizing: ", this);
            m_WindowResizingItem.TitleValue = (m_SettingsManager.AllowWindowResizing) ? "On" : "Off";
            AddMenuItem(m_WindowResizingItem);

            m_DoneItem = new DoneItem("Done", this);
            AddMenuItem(m_DoneItem);
        }
    }
}