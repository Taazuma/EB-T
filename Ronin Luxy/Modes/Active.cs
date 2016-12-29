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


            if (W.IsReady())
            {
                if (Player.Instance.HealthPercent <= 20 && EntityManager.Heroes.Enemies.Count(e => !e.IsDead && W.IsInRange(e)) > 0 && MiscMenu.GetCheckBoxValue("wLow"))
                {
                    if (Player.Instance.HealthPercent <= 20)
                    {
                        return;
                    }
                    else
                    {
                        var firstOrDefault = EntityManager.Heroes.Enemies.FirstOrDefault(e => !e.IsDead && W.IsInRange(e));
                        if (firstOrDefault != null)
                            W.Cast(firstOrDefault.ServerPosition);
                    }
                }
            }

            var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            //Thanks to Mario
            if (KillStealMenu.GetCheckBoxValue("rUse") && R.IsReady())
            {
                var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);

                if (rtarget == null) return;

                if (R.IsReady())
                {
                    var passiveDamage = rtarget.HasPassive() ? rtarget.GetPassiveDamage() : 0f;
                    var rDamage = rtarget.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(rtarget, R.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var pred = R.GetPrediction(rtarget);
                        if (pred.HitChancePercent >= 90)
                        {
                            R.Cast(pred.CastPosition);
                        }
                    }
                }
            }

          


        }

    }
}
