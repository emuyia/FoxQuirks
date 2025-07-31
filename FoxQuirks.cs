using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using FoxQuirks.Config;
using FoxQuirks.Patches;

namespace FoxQuirks;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class FoxQuirks : BaseUnityPlugin
{
    internal static ManualLogSource Log = null!;
    private readonly Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        Log = Logger;
        
        FoxQuirksConfig.Initialize(Config);
        
        _harmony.PatchAll(typeof(ShipSanctuaryPatches));
        
        Log.LogInfo("FoxQuirks has been initialized and patched.");
    }
}