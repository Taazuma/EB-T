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

namespace Eclipse.Modes
{
    internal class Combo
    {
        public static void Execute()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var rtarget = TargetSelector.GetTarget(3400, DamageType.Mixed);
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Physical);
            var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(Active._player) <= R.Range);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Core.DelayAction(delegate
            {
                if (wtarget.IsValidTarget(W.Range) && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    var predw = W.GetPrediction(wtarget);
                    if (predw.HitChance >= HitChance.High) W.Cast(predw.CastPosition);
                }
            }, Wdelay);

            Core.DelayAction(delegate
            {
                if (target.IsValidTarget(Q.Range) && Q.IsReady() && ComboMenu.GetCheckBoxValue("qUse"))
            {
                    Q.Cast();
            }
            }, Qdelay);

            Core.DelayAction(delegate
            { 
            if (W.IsReady() && ComboMenu.GetCheckBoxValue("wUse") && Prediction.Health.GetPrediction(wtarget, W.CastDelay) <= wtarget.GetDamage(SpellSlot.W))
            {
                W.Cast(wtarget.Position);
            }
            }, Wdelay);

            if (ComboMenu.GetCheckBoxValue("rUse") && target.IsValidTarget(SpellsManager.R.Range) && R.IsReady())
            {
                foreach (var ultenemies in enemies)
                {
                    var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
                    {
                        if (useR) R.Cast(ultenemies);
                    }
                }
            }



        }
    }
}