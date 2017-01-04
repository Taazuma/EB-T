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
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var quse = ComboMenu.GetCheckBoxValue("qUse");
            var wuse = ComboMenu.GetCheckBoxValue("wUse");
            var euse = ComboMenu.GetCheckBoxValue("eUse");
            var ruse = ComboMenu.GetCheckBoxValue("rUse");
            var predictionQ = Q.GetPrediction(qtarget);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (qtarget == null || qtarget.IsInvulnerable || qtarget.MagicImmune)
            {
                return;
            }

            #region Combos

            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
                {

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetSliderValue("QQ") > 0 && predictionQ.HitChance >= HitChance.High)
                    {
                        switch (ComboMenu.GetSliderValue("QQ"))
                        {
                            case 1:
                                Q.Cast(predictionQ.CastPosition);
                                break;
                            case 2:
                                foreach (var h in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget()))
                                {
                                    var predictionh = Q.GetPrediction(h);
                                    Q.Cast(predictionh.CastPosition);
                                }
                                break;
                        }
                    }
                }, Qdelay);

                if (R.IsReady() && ruse && rtarget.IsValidTarget(R.Range - 10) && rtarget.IsStunned ||  rtarget.IsTaunted || rtarget.HasBuffOfType(BuffType.Knockback) || rtarget.HasBuffOfType(BuffType.Knockup)
               || rtarget.HasBuffOfType(BuffType.Snare) || rtarget.HasBuffOfType(BuffType.Stun) || rtarget.HasBuffOfType(BuffType.Taunt))
                 { 
                    R.Cast();
                 }

                  Core.DelayAction(delegate
                 {
                     if (W.IsLearned && W.IsReady() && wtarget.IsValidTarget(W.Range * 2) && wuse)
                {
                    Program.WEnable();
                }
                }, Wdelay);

                Core.DelayAction(delegate
                {
                    if (etarget.IsValidTarget(E.Range) && euse && E.IsReady())
                {
                E.Cast();
                }
                }, Edelay);
                 }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetSliderValue("QQ") > 0 && predictionQ.HitChance >= HitChance.High)
                    {
                        switch (ComboMenu.GetSliderValue("QQ"))
                        {
                            case 1:
                                Q.Cast(predictionQ.CastPosition);
                                break;
                            case 2:
                                foreach (var h in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget()))
                                {
                                    var predictionh = Q.GetPrediction(h);
                                    Q.Cast(predictionh.CastPosition);
                                }
                                break;
                        }
                    }
                }, Qdelay);

                if (R.IsLearned && R.IsReady() && ruse)
                {
                    if (Player.Instance.CountEnemiesInRange(R.Range) >= ComboMenu.GetSliderValue("enemyr"))
                    { 
                    R.Cast();
                    }
                }

                Core.DelayAction(delegate
                {
                    if (W.IsLearned && W.IsReady() && wtarget.IsValidTarget(W.Range * 2) && wuse)
                {
                    Program.WEnable();
                }
                }, Wdelay);


             Core.DelayAction(delegate
                {
                if (etarget.IsValidTarget(E.Range) && euse && E.IsReady())
                {
                    E.Cast();
                }
                }, Edelay);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion Combos

        }
    }
}