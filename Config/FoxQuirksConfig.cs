using BepInEx.Configuration;

namespace FoxQuirks.Config
{
    internal static class FoxQuirksConfig
    {
        public static ConfigEntry<bool> ShipSanctuaryEnabled { get; private set; } = null!;
        
        public static void Initialize(ConfigFile config)
        {
            ShipSanctuaryEnabled = config.Bind(
                "Ship Sanctuary",
                "Enabled",
                true,
                "Prevents foxes from targeting or attacking players inside the ship."
            );
        }
    }
}