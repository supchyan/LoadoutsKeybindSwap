using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LoadoutsKeybindSwap.Common;
public class Config: ModConfig
{
    public static Config Instance;
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("LoadoutsSelect")]

    [DefaultValue(true)]
    public bool Loadout1;

    [DefaultValue(true)]
    public bool Loadout2;

    [DefaultValue(true)]
    public bool Loadout3;

    [Header("Misc")]

    [DefaultValue(true)]
    public bool IsWarningEnabled;
}