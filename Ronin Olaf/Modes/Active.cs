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
using Eclipse.Managers;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static void Execute()
        {

            if (Program._player.IsDead || Program._player.IsRecalling()) return;

            
            #region Killsteal
            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (KillStealMenu.GetCheckBoxValue("qUse")) // Start KS Q
            {
                var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                if (qtarget == null) return;

                if (Q.IsReady())
                {
                    var rDamage = Player.Instance.GetSpellDamage(qtarget, SpellSlot.Q);
                    if (Player.Instance.GetSpellDamage(qtarget, SpellSlot.Q) >= Prediction.Health.GetPrediction(qtarget, Q.CastDelay + Game.Ping))
                    {
                            Q.Cast(qtarget.ServerPosition);
                    }
                }
            }// END KS

            if (KillStealMenu.GetCheckBoxValue("eUse")) // Start KS E
            {
                var etarget = TargetSelector.GetTarget(E.Range + 50, DamageType.Magical);

                if (etarget == null) return;

                if (E.IsReady())
                {
                    var rDamage = Player.Instance.GetSpellDamage(etarget, SpellSlot.E);
                    if (Player.Instance.GetSpellDamage(etarget, SpellSlot.E) >= Prediction.Health.GetPrediction(etarget, E.CastDelay + Game.Ping))
                    {
                        E.Cast(etarget);
                    }
                }
            }// END KS




            #endregion Killsteal
            


        }

    }
}
