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
        public static Obj_AI_Minion Minion;

        public static void Execute()
        {
            //////////////////// KS E
            var targetKSE = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if (targetKSE != null && KillStealMenu.GetCheckBoxValue("eUse") && SpellsManager.E.IsReady())
            {
                if (targetKSE.Health < Player.Instance.GetSpellDamage(targetKSE, SpellSlot.E))
                {
                    SpellsManager.E.Cast(targetKSE);
                    return;
                }
            }//////////////////// END KS E

            if (R.IsLearned && Player.Instance.CountEnemiesInRange(R.Range) >= 1 || Player.Instance.HealthPercent <= 20 && R.IsReady() && ComboMenu.GetCheckBoxValue("rLow"))
            {
                R.Cast();
            }



        }

    }
}
