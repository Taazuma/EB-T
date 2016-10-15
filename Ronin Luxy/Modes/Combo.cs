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
        private static AIHeroClient myHero
        {
            get { return Player.Instance; }
        }
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(3400, DamageType.Mixed);
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);
            var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            // COMBO 1
            if (ComboMenu.GetCheckBoxValue("1combo"))
            {
                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                    {
                        Q.Cast(qtarget);
                    }
                }, Qdelay);

                if (target.HasBuffOfType(BuffType.Snare) || target.HasBuffOfType(BuffType.Stun))
                {
                    Core.DelayAction(delegate
                    {
                        if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range) && E.GetPrediction(etarget).HitChance >= Hitch.hitchance(E, FirstMenu))
                        {
                            E.Cast(etarget);
                        }
                    }, Edelay);

                    if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                    {
                        foreach (var ultenemies in enemies)
                        {
                            var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
                            var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);
                            var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                            var rDamage = target.GetDamage(SpellSlot.R) + passiveDamage;
                            {
                                if ((useR) && (predictedHealth <= rDamage))
                                {
                                    R.Cast(ultenemies);
                                }
                                else if (R.IsReady())
                                {

                                    var totalDamage = target.GetDamage(SpellSlot.E) + target.GetDamage(SpellSlot.R) + passiveDamage;


                                    if (predictedHealth <= totalDamage)
                                    {
                                        R.Cast(ultenemies);
                                    }
                                }
                            }
                        }
                    }


                    if (ComboMenu.GetCheckBoxValue("wUse") && W.IsReady() && Player.Instance.HealthPercent <= ComboMenu.GetSliderValue("hpW"))
                    {
                        W.Cast(wtarget.Position);
                    }

                }            // COMBO 1 END
            }


            // COMBO 2 / 3
            if (ComboMenu.GetCheckBoxValue("3combo"))
            {

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                    {
                        Q.Cast(qtarget);
                    }
                }, Qdelay);
                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range) && E.GetPrediction(etarget).HitChance >= Hitch.hitchance(E, FirstMenu))
                    {
                        E.Cast(etarget);
                    }
                }, Edelay);
                if (ComboMenu.GetCheckBoxValue("wUse") && W.IsReady())
                {
                    W.Cast(wtarget.Position);
                }
                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                {
                    foreach (var ultenemies in enemies)
                    {
                        var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
                        var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);
                        var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                        var rDamage = target.GetDamage(SpellSlot.R) + passiveDamage;
                        {
                            if ((useR) && (predictedHealth <= rDamage))
                            {
                                R.Cast(ultenemies);
                            }
                            else if (R.IsReady())
                            {

                                var totalDamage = target.GetDamage(SpellSlot.E) + target.GetDamage(SpellSlot.R) + passiveDamage;


                                if (predictedHealth <= totalDamage)
                                {
                                    R.Cast(ultenemies);
                                }
                            }
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("wUse") && W.IsReady() && Player.Instance.HealthPercent <= ComboMenu.GetSliderValue("hpW"))
                    {
                        W.Cast(wtarget.Position);
                    }
                }

            }            // COMBO 2 / 3 END


        }
    }
}