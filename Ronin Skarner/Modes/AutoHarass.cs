using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using static RoninSkarner.Menus;
using static RoninSkarner.SpellsManager;

namespace RoninSkarner.Modes
{
    /// <summary>
    /// This mode will always run
    /// </summary>
    internal class AutoHarass
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var enemiese = EntityManager.Heroes.Enemies.OrderByDescending
                       (a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(Player) <= E.Range);
            var target = TargetSelector.GetTarget(1500, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);

            if (AutoHarassMenu.GetCheckBoxValue("eUse") && etarget.IsValidTarget(SpellsManager.E.Range) && E.IsReady() && AutoHarassMenu.GetKeyBindValue("autoHarassKey") && E.GetPrediction(etarget).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                foreach (var eenemies in enemiese)
                {
                    var predE = E.GetPrediction(eenemies);
                    {
                        E.Cast(predE.CastPosition);
                    }
                }
            }
        }
    }
}