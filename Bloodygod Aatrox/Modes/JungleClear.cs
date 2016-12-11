using static Eclipse.SpellsManager;
using static Eclipse.Menus;
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
namespace Eclipse.Modes
{
    internal class JungleClear
    {
        public static void Execute()
        { 
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(900));
            var useQ = JungleClearMenu.GetCheckBoxValue("qUse");
            var useW = JungleClearMenu.GetCheckBoxValue("wUse");
            var useE = JungleClearMenu.GetCheckBoxValue("eUse");
            var source = EntityManager.MinionsAndMonsters.GetJungleMonsters(Eclipse.Modes.LaneClear.Player.ServerPosition).OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.Distance(Eclipse.Modes.LaneClear.Player) <= Eclipse.Modes.LaneClear.Player.GetAutoAttackRange());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (target == null) return;

            if (E.IsReady() && useE)
            {
                E.Cast(target.Position);
                return;
            }

            if (Q.IsReady() && useQ)
            {
                Q.Cast(target.Position);
                return;
            }

            if (!W.IsReady() || !useW) return;
            if (W.IsReady() && Eclipse.Modes.LaneClear.Player.HealthPercent < JungleClearMenu["jungle.minw"].Cast<Slider>().CurrentValue)
            {
                if (Eclipse.Modes.LaneClear.Player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 2)
                {
                    W.Cast();
                    return;
                }
            }

            if (!W.IsReady() ||
                !(Eclipse.Modes.LaneClear.Player.HealthPercent > JungleClearMenu["jungle.maxw"].Cast<Slider>().CurrentValue)) return;
            if (Eclipse.Modes.LaneClear.Player.Spellbook.GetSpell(SpellSlot.W).ToggleState != 1) return;
            W.Cast();
            return;

        }
    }
}
