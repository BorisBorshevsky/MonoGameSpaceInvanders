using Infrastructure;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.ObjectModel.Managers;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menu
{
    public class PlayItem : MenuItem
    {
        public PlayItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }
        
        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new LevelTransitionScreen(i_GameScreen.Game));
        }
    }

    public class SoundOptionsItem : MenuItem
    {
        public SoundOptionsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new SoundOptionsMenu(i_GameScreen.Game));
        }
    }

    public class ChoosePlayersItem : MenuItem
    {
        public ChoosePlayersItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {}

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            ISettingsManager m_SettingsManager = (ISettingsManager)GameScreen.Game.Services.GetService(typeof(ISettingsManager));

            int numOfPlayers = m_SettingsManager.NumOfPlayers;
            numOfPlayers = (numOfPlayers + 1) %2; 
            for (int i = 0; i < m_SettingsManager.PlayersData.Count; i++)
            {
                m_SettingsManager.PlayersData[i].Enabled = i < numOfPlayers;
            }

            
            return string.Format("{0}", numOfPlayers == 1 ? "One" : "Two");
        }
    }

    public class ScreenOptionsItem : MenuItem
    {
        public ScreenOptionsItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.ScreensManager.SetCurrentScreen(new ScreenOptionsMenu(i_GameScreen.Game));
        }
    }

    public class QuitGameItem : MenuItem
    {
        public QuitGameItem(string i_Title, GameScreen i_GameScreen)
            : base(i_Title, i_GameScreen)
        {
        }

        public override void EnterScreen(GameScreen i_GameScreen)
        {
            i_GameScreen.Game.Exit();
        }
    }
}