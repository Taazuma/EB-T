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
        public static AIHeroClient player
        {
            get { return ObjectManager.Player; }

        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            //////////////////////////////////////////////////////////////////////////////////////////
            var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            float dist = (float)(Q.Range + player.MoveSpeed * 2.5);
            var useQ = ComboMenu.GetCheckBoxValue("qUse");
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            var cmbDmg = Program.ComboDamage(target);

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
            //////////////////////////////////////////////////////////////////////////////////////////

            if (ComboMenu.GetCheckBoxValue("itemss"))
            {
                Eclipse.Activator.Items.CastItems(target);
            }

            if (Program.ShacoClone && !Program.GhostDelay && ComboMenu["useClone"].Cast<CheckBox>().CurrentValue &&!MiscMenu["autoMoveClone"].Cast<CheckBox>().CurrentValue)
            {
                Program.moveClone();
            }

            if (Q.IsReady() && useQ && target.IsValidTarget(Q.Range))
            {
                Q.Cast(Prediction.Position.PredictUnitPosition(target, 500).To3D());
            }

            else
            {
                if (!Program.CheckWalls(target) || Utils.GetPath(player, target.Position) < dist)
                {
                    Q.Cast(player.Position.Extend(target.Position, Q.Range));
                }
            }

            if (W.IsReady() && useW)
            {
                if (target.IsValidTarget(W.Range + 250))
                {
                    Program.HandleW(target);
                }
            }
            
            if (useE && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (useR && R.IsReady() && !Program.ShacoClone && target.HealthPercent < 60 &&
            cmbDmg < target.Health && target.HealthPercent > cmbDmg && target.HealthPercent > 25)
            {
                R2.Cast();
            }

            if (ComboMenu.GetCheckBoxValue("rLow") && R2.IsReady() && Player.Instance.HealthPercent <= ComboMenu.GetSliderValue("hpR"))
            {
                R2.Cast();
            }

        }
        }
}