using Infrastructure.Menu;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu.MainMenuItems
{
    class ChoosePlayersItem : SpaceInvaderMenuItem
    {
        public ChoosePlayersItem(string i_Title, GameScreen i_GameScreen, IMenuConfiguration i_MenuConfiguration)
            : base(i_Title, i_GameScreen, i_MenuConfiguration)
        { }

        public override string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            int numOfPlayers = r_SettingsManager.NumOfPlayers;
            numOfPlayers = (numOfPlayers + 1) % 2;
            for (int i = 0; i < r_SettingsManager.PlayersData.Count; i++)
            {
                r_SettingsManager.PlayersData[i].Enabled = i < numOfPlayers;
            }

            return string.Format("{0}", numOfPlayers == 1 ? "One" : "Two");
        }
    }
}
