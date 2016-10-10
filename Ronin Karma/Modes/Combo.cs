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
        public static AIHeroClient getPlayer()
        {
            return ObjectManager.Player;
        }

        public static void Execute()
        {
            //-------------------------------------------------------------------------------------------------------------------------
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (qtarget == null) return;
            //---------------------------------------------------------------------------------------- #Start COMBO 1

            if (ComboMenu.GetCheckBoxValue("combo1"))
            {

                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW)
                {
                    R.Cast();
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady())
                {
                    var player1 = ObjectManager.Player;
                    E.Cast(player1);
                }

                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    Q.Cast(qtarget);
                }

            }

            //----------------------------------------------------------------------------------------#End COMBO 1

            //----------------------------------------------------------------------------------------#Start COMBO 2

            if (ComboMenu.GetCheckBoxValue("combo2"))
            {
                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW)
                {
                    R.Cast();
                }

                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    Q.Cast(qtarget);
                }

            }

            //----------------------------------------------------------------------------------------#End COMBO 2

            //----------------------------------------------------------------------------------------#Start COMBO 3

            if (ComboMenu.GetCheckBoxValue("combo3"))
            {
                if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    Q.Cast(qtarget);
                }

                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW)
                {
                    R.Cast();
                }

            }

            //----------------------------------------------------------------------------------------#End COMBO 3

            //----------------------------------------------------------------------------------------#Start COMBO 4

            if (ComboMenu.GetCheckBoxValue("combo4"))
            {
                if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range) && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    Q.Cast(qtarget);
                }

                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW)
                {
                    R.Cast();
                }

                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady())
                {
                    var player1 = ObjectManager.Player;
                    E.Cast(player1);
                }

            }

            //----------------------------------------------------------------------------------------#End COMBO 4

            //----------------------------------------------------------------------------------------#Start COMBO 5

            if (ComboMenu.GetCheckBoxValue("combo5"))
            {
                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady())
                {
                    var player1 = ObjectManager.Player;
                    E.Cast(player1);
                }

                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW && ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    R.Cast();
                    Q.Cast(qtarget);
                }
            }


            //----------------------------------------------------------------------------------------#End COMBO 5

            //----------------------------------------------------------------------------------------#Start COMBO 6

            if (ComboMenu.GetCheckBoxValue("combo6"))
            {

                if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && getPlayer().HealthPercent < comboUseRW && ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    R.Cast();
                    Q.Cast(qtarget);
                }

                if (ComboMenu.GetCheckBoxValue("wUse") && wtarget.IsValidTarget(W.Range) && W.IsReady())
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady())
                {
                    var player1 = ObjectManager.Player;
                    E.Cast(player1);
                }
            }

            //----------------------------------------------------------------------------------------#End COMBO 6

            //----------------------------------------------------------------------------------------#Start COMBO 7

            if (ComboMenu.GetCheckBoxValue("combo7"))
            {
                var target = TargetSelector.GetTarget(1250, DamageType.Magical);
                if (target == null) return;
                var pred = Q.GetPrediction(target);
                var sayimiz = target.CountEnemiesInRange(Q.Width);
                var player1 = ObjectManager.Player;
                if (R.IsReady() && (Q.IsReady() || W.IsReady() || E.IsReady()))
                {
                    if (player1.HealthPercent < 50 && W.IsReady() && target.IsValidTarget(W.Range))
                    {
                        R.TryToCast(player1, ComboMenu);
                    }
                    if (Q.IsReady() && target.IsValidTarget(Q.Range))
                    {
                        if (pred.HitChance >= HitChance.High)
                        {
                            R.TryToCast(player1, ComboMenu);
                        }
                    }
                    if (E.IsReady() && player1.Position.CountAlliesInRange(E.Range) >= 3)
                    {
                        R.TryToCast(player1, ComboMenu);
                    }
                }


                if (player1.HasBuff("KarmaMantra") && (Q.IsReady() || W.IsReady() || E.IsReady()))
                {
                    if (player1.HealthPercent < 50 && W.IsReady() && target.IsValidTarget(W.Range))
                    {
                        W.TryToCast(target, ComboMenu);
                    }
                    if (E.IsReady() && player1.Position.CountAlliesInRange(E.Range) >= 3)
                    {
                        E.TryToCast(player1, ComboMenu);
                    }
                    if (target.IsValidTarget(Q.Range) && Q.IsReady())
                    {
                        Q.TryToCast(target, ComboMenu);
                    }
                }
                //if player doesnt have karma mantra activated.
                else
                {
                    if (target.IsValidTarget(W.Range))
                    {
                        W.TryToCast(target, ComboMenu);
                    }
                    if (target.IsValidTarget(Q.Range))
                    {
                        Q.TryToCast(target, ComboMenu);
                    }
                }

                if (target.HasBuff("KarmaSpiritBind") || (Player.Instance.Health <=  60 && target.IsAttackingPlayer))
                {
                    E.TryToCast(player1, ComboMenu);
                }
                //Run to target with normal E if player can catch.
                if (player1.Distance(target) < Q.Range + player1.MoveSpeed * 0.3 && E.IsReady() &&
                    player1.HasBuff("CrestoftheAncientGolem"))
                {
                    E.TryToCast(player1, ComboMenu);
                }
            }

            //----------------------------------------------------------------------------------------#End COMBO 7

        }
    }
}