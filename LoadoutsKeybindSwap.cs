using Terraria.ModLoader;

namespace LoadoutsKeybindSwap;
public class LoadoutKeybindSwap : Mod
{
    public static ModKeybind LoadoutSwap { get; private set; }
    public override void Load()
    {
        LoadoutSwap = KeybindLoader.RegisterKeybind(this, "LoadoutSwap", "C");
    }
    public override void Unload()
    {
        LoadoutSwap = null;
    }
}
