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
            var target = TargetSelector.GetTarget(1000, DamageType.Magical, Player.Instance.Position);
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            var quse = ComboMenu.GetCheckBoxValue("qUse");
            var wuse = ComboMenu.GetCheckBoxValue("wUse");
            var euse = ComboMenu.GetCheckBoxValue("eUse");
            var ruse = ComboMenu.GetCheckBoxValue("rUse");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
            { 
            if (target.IsValidTarget(1000) && quse && target.IsEnemy && Q.GetPrediction(target).HitChance >= Hitch.hitchance(Q, FirstMenu))
            {
                    Q.Cast(target.Position);
            }

            if (R.IsReady() && ruse && rtarget.IsValidTarget(R.Range) && rtarget.IsStunned ||  rtarget.IsTaunted || rtarget.IsCharmed || rtarget.Spellbook.IsChanneling
                   || rtarget.HasBuffOfType(BuffType.Charm) || rtarget.HasBuffOfType(BuffType.Knockback) || rtarget.HasBuffOfType(BuffType.Knockup)
                   || rtarget.HasBuffOfType(BuffType.Snare) || rtarget.HasBuffOfType(BuffType.Stun) || rtarget.HasBuffOfType(BuffType.Suppression) // Credits Kappa the Kappa Def. not Kappa ^)
                   || rtarget.HasBuffOfType(BuffType.Taunt))
            { 
                    R.Cast();
            }

            if (W.IsLearned && W.IsReady() && wtarget.IsValidTarget(W.Range * 2) && wuse)
            {
                Program.WEnable();
            }

            if (etarget.IsValidTarget(E.Range) && euse && E.IsReady())
            {
                E.Cast();
            }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {
                if (target.IsValidTarget(1000) && quse && target.IsEnemy && Q.GetPrediction(target).HitChance >= Hitch.hitchance(Q, FirstMenu))
                {
                    Q.Cast(target.Position);
                }

                if (R.IsLearned && R.IsReady() && ruse)
                {
                    if (Player.Instance.CountEnemiesInRange(R.Range) >= ComboMenu.GetSliderValue("enemyr"))
                    { 
                    R.Cast();
                    }
                }

                if (W.IsLearned && W.IsReady() && wtarget.IsValidTarget(W.Range * 2) && wuse)
                {
                    Program.WEnable();
                }

                if (etarget.IsValidTarget(E.Range) && euse && E.IsReady())
                {
                    E.Cast();
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

        }
    }
}