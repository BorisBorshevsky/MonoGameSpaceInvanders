using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.Menu
{
    public interface IMenuConfiguration
    {
        Keys MoveDownKey { get; }
        Keys MoveUpKey { get; }
        Keys ScrollDownKey { get; }
        Keys ScrollUpKey { get; }
        Keys EnterKey { get; }
        Color ActiveColor { get; }
        Color InActiveColor { get; }
        Color MenuHeadColor { get; }
        string MenuMoveSoundAssetName { get; }
        string MenuFontAssetName { get; }
    }
}
