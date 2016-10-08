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
using Eclipse.DMGHandler;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;

        public static void Execute()
        {


            //////////////////// KS E BETA
            var targetKSE = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if (targetKSE != null && KillStealMenu.GetCheckBoxValue("eUse") && SpellsManager.E.IsReady())
            {
                var predE2 = SpellsManager.E.GetPrediction(targetKSE);
                if (predE2.HitChance >= HitChance.High && targetKSE.Health < Player.Instance.GetSpellDamage(targetKSE, SpellSlot.E))
                {
                    SpellsManager.E.Cast(predE2.CastPosition);
                    return;
                }
            }//////////////////// END KS E

            //////////////////// KS R BETA
            var targetKSR = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if (targetKSR != null && KillStealMenu.GetCheckBoxValue("rUse") && SpellsManager.R.IsReady())
            {
                var predR2 = SpellsManager.R.GetPrediction(targetKSR);
                if (predR2.HitChance >= HitChance.High && targetKSR.Health < Player.Instance.GetSpellDamage(targetKSR, SpellSlot.R))
                {
                    SpellsManager.R.Cast(predR2.CastPosition);
                    return;
                }
            }//////////////////// END KS R

            if (Player.Instance.InDanger(95) && W.IsReady() && (Hitch.ShouldOverload(SpellSlot.W) || Player.Instance.Mana < 80)) //Credits Mario
            {
                W.Cast();
            }


        }


    }
}
