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
        public static readonly AIHeroClient Akali = ObjectManager.Player;

        public static void Execute()
        {

            if (DrawingsMenu.GetCheckBoxValue("showkilla"))
                Indicator.DamageToUnit = Program.GetComboDamage;

            if (Player.HasBuff("zedulttargetmark") && MiscMenu.GetCheckBoxValue("wlow"))
            {
                if (W.IsReady())
                {
                    W.Cast(Player.Instance);
                }
            }

            if (Akali.IsInShopRange() && MiscMenu.GetCheckBoxValue("fun"))
            {
                //Chat.Say("/masterybadge");
                W.Cast(Player.Instance);
            }

            //if (Akali.IsDead && MiscMenu.GetCheckBoxValue("fun"))
            //{
        
            //}

            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);
            //var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (Player.Instance.CountEnemiesInRange(W.Range) >= 2 || Player.Instance.HealthPercent <= 20 && W.IsReady() && MiscMenu.GetCheckBoxValue("wlow"))
            {
                W.Cast(Player.Instance);
            }

            if (KillStealMenu.GetCheckBoxValue("qUse")) // Start KS Q
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

            if (KillStealMenu.GetCheckBoxValue("eUse")) // Start KS E
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);

                if (etarget == null) return;

                if (E.IsReady() && etarget.Health + etarget.AttackShield <= Akali.GetSpellDamage(etarget, SpellSlot.E) && etarget.IsValidTarget(E.Range))
                {
                    E.Cast();
                }
            }// END KS

            if (KillStealMenu.GetCheckBoxValue("rUse")) // Start KS R
            {
                var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);

                if (rtarget == null) return;

                if (R.IsReady())
                {
                    //var passiveDamage = rtarget.HasPassive() ? rtarget.GetPassiveDamage() : 0f;
                    var rDamage = rtarget.GetDamage(SpellSlot.R);

                    if (rtarget.Health + rtarget.AttackShield <= rDamage)
                    {
                        if (rtarget.IsValidTarget(R.Range))
                        {
                            R.Cast(rtarget);
                        }
                    }
                }
            }// END KS

            if (MiscMenu.GetCheckBoxValue("autoq") && Q.IsReady() && target.IsValidTarget(Q.Range + 200) && Player.Instance.Mana <= 100)
            {
                Q.Cast(target);
            }


        }

    }
}
