using Microsoft.Xna.Framework;

namespace SpaceInvaders.Configurations
{
    public class SpaceShipConfiguration
    {
        public SpaceShipMouseConfiguration SpaceShipMouseConfiguration { get; set; }

        public SpaceShipKeyboardConfiguration SpaceShipKeyboardConfiguration { get; set; }

        public string AssetName { get; set; }

        public Color TextColor { get; set; }
    }
}