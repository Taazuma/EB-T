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
        private static AIHeroClient enemyHaveMota
        {
            get
            {
                return
                    (from enemy in
                        ObjectManager.Get<AIHeroClient>().Where(enemy => enemy.IsEnemy && enemy.IsValidTarget(R.Range))
                     from buff in enemy.Buffs
                     where buff.DisplayName == "AkaliMota"
                     select enemy).FirstOrDefault();
            }
        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Mixed);
            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);
            //var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);

            // COMBO 1 Beginn --------------------------------------------------------------------------------
            if (ComboMenu.GetCheckBoxValue("combo1"))
            {
                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range))
                    {
                        Q.Cast(qtarget);
                    }
                }, Qdelay);
                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                    {
                        R.Cast(rtarget);
                    }
                }, Rdelay);

                var motaEnemy = enemyHaveMota;
                if (motaEnemy != null && motaEnemy.IsValidTarget(_player.GetAutoAttackRange(qtarget)))
                    return;

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range))
                    {
                        E.Cast();
                    }
                }, Edelay);

                if (W.IsReady() && wtarget.IsValidTarget(W.Range) && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    if (Player.Instance.CountEnemiesInRange(Q.Range) >= 1 || Player.Instance.HealthPercent <= 15)
                    {
                        W.Cast(Player.Instance);
                    }
                }
            }
            // COMBO 1 END --------------------------------------------------------------------------------

            // COMBO 2 Beginn --------------------------------------------------------------------------------
            if (ComboMenu.GetCheckBoxValue("combo2"))
            {
                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range))
                    {
                        Q.Cast(qtarget);
                    }
                }, Qdelay);

                var motaEnemy = enemyHaveMota;
                if (motaEnemy != null && motaEnemy.IsValidTarget(_player.GetAutoAttackRange(qtarget)))
                    return;

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range))
                    {
                        E.Cast();
                    }
                }, Edelay);

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                    {
                        R.Cast(rtarget);
                    }
                }, Rdelay);

                if (W.IsReady() && wtarget.IsValidTarget(W.Range) && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    if (Player.Instance.CountEnemiesInRange(Q.Range) >= 1 || Player.Instance.HealthPercent <= 15)
                    {
                        W.Cast(Player.Instance);
                    }
                }
            }
            // COMBO 2 END --------------------------------------------------------------------------------

            // COMBO 3 Beginn --------------------------------------------------------------------------------
            if (ComboMenu.GetCheckBoxValue("combo3"))
            {

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                    {
                        R.Cast(rtarget);
                    }
                }, Rdelay);

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range))
                    {
                        Q.Cast(qtarget);
                    }
                }, Qdelay);

                var motaEnemy = enemyHaveMota;
                if (motaEnemy != null && motaEnemy.IsValidTarget(_player.GetAutoAttackRange(qtarget)))
                    return;

                Core.DelayAction(delegate
                {
                    if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range))
                    {
                        E.Cast();
                    }
                }, Edelay);



                if (W.IsReady() && wtarget.IsValidTarget(W.Range) && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    if (Player.Instance.CountEnemiesInRange(Q.Range) >= 1 || Player.Instance.HealthPercent <= 15)
                    {
                        W.Cast(Player.Instance);
                    }
                }
            }
            // COMBO 3 END --------------------------------------------------------------------------------

            // COMBO 4 Beginn --------------------------------------------------------------------------------
            if (ComboMenu.GetCheckBoxValue("combo4"))
            {

                    if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range))
                    {
                        Q.Cast(qtarget);
                    }

                var motaEnemy = enemyHaveMota;
                if (motaEnemy != null && motaEnemy.IsValidTarget(_player.GetAutoAttackRange(qtarget)))
                    return;

                    if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range))
                    {
                        E.Cast();
                    }

                    if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range))
                    {
                        R.Cast(rtarget);
                    }

            }
            // COMBO 4 END --------------------------------------------------------------------------------

        }
    }
}