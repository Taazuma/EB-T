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
            var target = TargetSelector.GetTarget(1250, DamageType.Magical);
            if (target == null) return;
            //---------------------------------------------------------------------------------------- #Start COMBO
  
            if (ComboMenu.GetCheckBoxValue("combouse"))
            {
                var pred = Q.GetPrediction(target);
                //var sayimiz = target.CountEnemiesInRange(Q.Width);
                // THANKS TO aliyrlmz ! //
                var player1 = ObjectManager.Player;
                var quse = ComboMenu.GetCheckBoxValue("qUse");
                var wuse = ComboMenu.GetCheckBoxValue("wUse");
                var euse = ComboMenu.GetCheckBoxValue("eUse");
                var ruse = ComboMenu.GetCheckBoxValue("rUse");

                if (R.IsReady() && (Q.IsReady() || W.IsReady() || E.IsReady()) && quse && wuse && euse && ruse)
                {
                    if (player1.HealthPercent < 50 && W.IsReady() && target.IsValidTarget(W.Range))
                    {
                        R.TryToCast(player1, ComboMenu);
                    }

                    if (Q.IsReady() && target.IsValidTarget(Q.Range) && quse)
                    {
                        if (pred.HitChance >= HitChance.High)
                        {
                            R.TryToCast(player1, ComboMenu);
                        }
                    }

                    if (E.IsReady() && player1.Position.CountAlliesInRange(E.Range) >= ComboMenu.GetSliderValue("comboEnemies") && ruse)
                    {
                        R.TryToCast(player1, ComboMenu);
                    }
                }


                if (player1.HasBuff("KarmaMantra") && (Q.IsReady() || W.IsReady() || E.IsReady()) && quse && wuse && euse)
                {
                    if (player1.HealthPercent < 50 && W.IsReady() && target.IsValidTarget(W.Range))
                    {
                        W.TryToCast(target, ComboMenu);
                    }

                    if (E.IsReady() && player1.Position.CountAlliesInRange(E.Range) >= ComboMenu.GetSliderValue("comboEnemies"))
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
                    if (target.IsValidTarget(W.Range) && wuse)
                    {
                        W.TryToCast(target, ComboMenu);
                    }
                    if (target.IsValidTarget(Q.Range) && quse)
                    {
                        Q.TryToCast(target, ComboMenu);
                    }
                }

                if (target.HasBuff("KarmaSpiritBind") || (Player.Instance.Health <=  ComboMenu.GetSliderValue("combouseE") && target.IsAttackingPlayer) && euse)
                {
                    E.TryToCast(player1, ComboMenu);
                }

                //Run to target with normal E if player can catch.
                if (player1.Distance(target) < Q.Range + player1.MoveSpeed * 0.3 && E.IsReady() && player1.HasBuff("CrestoftheAncientGolem") && euse)
                {
                    E.TryToCast(player1, ComboMenu);
                }
            }

            //----------------------------------------------------------------------------------------#End COMBO

        }
    }
}