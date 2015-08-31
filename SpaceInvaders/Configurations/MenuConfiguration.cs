using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Infrastructure.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Configurations
{
    class MenuConfiguration : IMenuConfiguration
    {
        public Keys MoveDownKey
        {
            get { return Keys.Down; }
        }

        public Keys MoveUpKey
        {
            get { return Keys.Up; }
        }

        public Keys ScrollDownKey
        {
            get { return Keys.PageDown; }
        }

        public Keys ScrollUpKey
        {
            get { return Keys.PageUp; }
        }

        public Keys EnterKey
        {
            get { return Keys.Enter; }
        }

        public Color ActiveColor
        {
            get { return Color.Blue; }
        }

        public Color InActiveColor
        {
            get { return Color.Green; }
        }

        public Color MenuHeadColor
        {
            get { return Color.LightCoral; }
        }

        public string MenuMoveSoundAssetName
        {
            get { return Sounds.k_MenuMove; }
        }

        public string MenuFontAssetName
        {
            get { return @"Fonts\Arial"; }
        }
    }
}
