using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure
{
    public class MenuItem : IMenuItem
    {
        public MenuItem(string i_Title, GameScreen i_GameScreen)
        {
            TitleValue = string.Empty;
            Title = i_Title;
            GameScreen = i_GameScreen;
        }

        public string Title { get; private set; }

        public string TitleValue { get; set; }

        public virtual void EnterScreen(GameScreen i_GameScreen) 
        { 
        }

        public virtual string ItemSelected(GameScreen i_GameScreen, Keys i_Key)
        {
            return string.Empty;
        }

        public GameScreen GameScreen { get; private set; }
    }
}