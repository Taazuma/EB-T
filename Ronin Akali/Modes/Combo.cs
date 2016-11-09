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
        public static AIHeroClient enemyHaveMota
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

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
     

            // COMBO 1 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
            {

                if (MiscMenu.GetCheckBoxValue("useitems"))
                {
                    Eclipse.Modes.Items.CastItems(target);
                }

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

                if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 0 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Game.CursorPos);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 1 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(wtarget.Position);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 2 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Active.Akali.Position - 20);
                }

            }
            // COMBO 1 END --------------------------------------------------------------------------------

            // COMBO 2 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {

                if (MiscMenu.GetCheckBoxValue("useitems"))
                {
                    Eclipse.Modes.Items.CastItems(target);
                }

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

                if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 0 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Game.CursorPos);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 1 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(wtarget.Position);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 2 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Active.Akali.Position - 20);
                }
            }
            // COMBO 2 END --------------------------------------------------------------------------------

            // COMBO 3 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 2)
            {

                if (MiscMenu.GetCheckBoxValue("useitems"))
                {
                    Eclipse.Modes.Items.CastItems(target);
                }

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

                if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 0 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Game.CursorPos);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 1 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(wtarget.Position);
                }
                else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 2 && W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
                {
                    W.Cast(Active.Akali.Position - 20);
                }
            }
            // COMBO 3 END --------------------------------------------------------------------------------

            // COMBO 4 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 3)
            {
                R.Cast(rtarget);
                Q.Cast(target);
                W.Cast(wtarget.Position);
                var motaEnemy = enemyHaveMota;
                if (motaEnemy != null && motaEnemy.IsValidTarget(_player.GetAutoAttackRange(qtarget)))
                    return;
                E.Cast();
            }
            // COMBO 4 END --------------------------------------------------------------------------------

            

        }
    }
}