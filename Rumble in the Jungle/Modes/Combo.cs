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
            var target = TargetSelector.GetTarget(1700, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }


            Core.DelayAction(delegate
            {
                if (target.IsValidTarget(E.Range) && E.IsReady() && ComboMenu.GetCheckBoxValue("eUse") && (Hitch.ShouldOverload(SpellSlot.E) || Player.Instance.Mana < 80))
                {
                    E.Cast(target);
                }
            }, Edelay);

                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1)
                {
                      E.Cast(target.Position);
                }

           if (target.IsValidTarget(Q.Range) && Q.IsReady() && ComboMenu.GetCheckBoxValue("qUse") && Player.Instance.IsFacing(target) && (Hitch.ShouldOverload(SpellSlot.Q) || Player.Instance.Mana < 80))
            {
                Q.Cast();
            }

           if (W.IsReady() && ComboMenu.GetCheckBoxValue("wUse"))
            {
                W.Cast();
            }

            var targetR = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (targetR == null || targetR.IsZombie || targetR.HasUndyingBuff()) return;

            if (ComboMenu.GetCheckBoxValue("my") && R.IsReady())
                if (targetR.IsMoving)
                {
                    if (targetR.IsValidTarget(R.Range + 1000))
                    {
                        R.Cast(target.Position -target.MoveSpeed);
                    }
                }
                else
                {
                    R.Cast(target.Position + 550);
                }

            if (R.IsReady() && targetR.IsValidTarget(R.Range) && ComboMenu.GetCheckBoxValue("mario") && !targetR.IsInRange(Player.Instance, E.Range) && !targetR.IsFacing(Player.Instance))
            {
                if (KillStealMenu.GetCheckBoxValue("rUse") && Prediction.Health.GetPrediction(targetR, R.CastDelay) <=
                    SpellDamage.GetRealDamage(SpellSlot.R, targetR))
                {
                    Hitch.CastR(targetR, MiscMenu.GetSliderValue("minR"));
                }
                else
                {
                    Hitch.CastR(targetR, MiscMenu.GetSliderValue("minR"));
                }
            }



        }
    }
}