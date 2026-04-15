using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LoadoutsKeybindSwap.Common;
public class PlayerManager: ModPlayer
{
    int LSI_Index { get; set; } = 0;
    static bool Loadout1 => Config.Instance.Loadout1;
    static bool Loadout2 => Config.Instance.Loadout2;
    static bool Loadout3 => Config.Instance.Loadout3;

    bool[] LoadoutSwapItems { 
        get
        {
            return [Loadout1, Loadout2, Loadout3];
        }
    }
    public override void OnEnterWorld()
    {
        if (ModLoader.TryGetMod("ExtraLoadouts", out Mod ExtraLoadouts) && Config.Instance.IsWarningEnabled)
        {
            var text = Language.GetTextValue("Mods.LoadoutsKeybindSwap.Chat.ExtraLoadoutsWarning");
            
            text = text.Replace("{0}", Mod.DisplayName)
                .Replace("{1}", ExtraLoadouts.DisplayName);

            Main.NewText($"[c/FFF014:{text}]");
        }
    }
    public List<int> GetEnabledVanillaLoadoutIndexes()
    {
        var list = new List<int>();

        for (int i = 0; i < LoadoutSwapItems.Length; i++)
        {
            if (LoadoutSwapItems[i])    // if enabled in config menu
            {
                list.Add(i);            // add it on list to fetch through later
            }
        }

        return list;
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (LoadoutKeybindSwap.LoadoutSwap.JustPressed)
        {
            var loadoutIndexes = GetEnabledVanillaLoadoutIndexes();
            var LS_ItemExists = LSI_Index + 1 < loadoutIndexes.Count;

            LSI_Index = LS_ItemExists ? LSI_Index + 1 : 0;

            Main.LocalPlayer.TrySwitchingLoadout(loadoutIndexes[LSI_Index]);
        }
    }
    void ExtraLoadoutsTests()
    {
        if (ModLoader.TryGetMod("ExtraLoadouts", out Mod ExtraLoadouts))
        {
            ExtraLoadouts.TryFind<ModPlayer>("LoadoutPlayer", out ModPlayer LoadoutPlayer);

            Type LoadoutPlayerType = LoadoutPlayer.GetType();

            MethodInfo TrySwitchToExLoadout = LoadoutPlayerType.GetMethod("TrySwitchToExLoadout");

            object loadoutPlayer = Activator.CreateInstance(LoadoutPlayerType, null);

            // lmao
            TrySwitchToExLoadout.Invoke(loadoutPlayer, new object[] { 1 });
        }
    }
}