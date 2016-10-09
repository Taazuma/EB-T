using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace RoninTune
{
    public static class SpellManager
    {
        public static Spell.Targeted Smite { get; private set; }

        static SpellManager()
        {
            var smite = Player.Spells.FirstOrDefault(s => s.SData.Name.ToLower().Contains("smite"));
            if (smite != null)
                Smite = new Spell.Targeted(smite.Slot, 570);
        }

        public static void Initialize()
        {
        }

        public static bool HasSmite()
        {
            return Smite != null;
        }

        public static bool HasChillingSmite()
        {

            return Smite != null &&
                   Smite.Name.Equals("s5_summonersmiteplayerganker", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool HasChallengingSmite()
        {
            return Smite != null &&
                   Smite.Name.Equals("s5_summonersmiteduel", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
