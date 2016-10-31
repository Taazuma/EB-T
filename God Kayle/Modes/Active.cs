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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using System.Diagnostics;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;

        public static void Execute()
        {
            if (DrawingsMenu.GetCheckBoxValue("showkilla"))
                Indicator.DamageToUnit = SpellsManager.GetTotalDamage;

            //////////////////////////////////////////////////////////////////////////////////////////////////// Safer
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);

            if (target == null) return;

            if (W.IsReady() && Combo._player.HealthPercent <= 20 && FirstMenu.GetCheckBoxValue("Saferme") && Combo._player.ManaPercent >= 20)
            {
                W.Cast(Modes.Combo._player);
            }

            else if (W.IsReady() && FirstMenu.GetCheckBoxValue("Saferali") && Combo._player.ManaPercent >= 20)
            {
                foreach (var ally in EntityManager.Heroes.Allies)
                {
                    if (!ally.IsMe && ally.HealthPercent <= 15 && !ally.IsRecalling() && !ally.IsDead)
                    {
                        W.Cast(ally);
                    }
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////// Safer

            if (Player.HasBuff("zedulttargetmark"))
            {
                if (W.IsReady())
                {
                    W.Cast(Player.Instance);
                }
            }

            if (Q.IsReady()) // Start KS Q
            {
                var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                if (qtarget == null) return;

                if (Q.IsReady())
                {
                    var rDamage = qtarget.GetDamage(SpellSlot.Q);

                    if (qtarget.Health + qtarget.AttackShield <= rDamage)
                    {
                        if (qtarget.IsValidTarget(Q.Range))
                        {
                            Q.Cast(qtarget);
                        }
                    }
                }
            }// END KS


        }

    }
}
