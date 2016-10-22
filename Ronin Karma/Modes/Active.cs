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
       

        public static void Execute()
        {

            if (DrawingsMenu.GetCheckBoxValue("showkilla"))
                Indicator.DamageToUnit = Program.GetComboDamage;


            if (Player.HasBuff("zedulttargetmark") && MiscMenu.GetCheckBoxValue("esafe") && R.IsReady() && E.IsReady())
            {
                R.Cast();
                var player1 = ObjectManager.Player;
                E.Cast(player1);
            }

            //////////////////// KS Q
            var targetKSQ = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);
            if (targetKSQ == null) return;

            if (KillStealMenu.GetCheckBoxValue("qUse") && SpellsManager.Q.IsReady())
            {
                var predQ2 = SpellsManager.Q.GetPrediction(targetKSQ);
                if (predQ2.HitChance >= HitChance.High && targetKSQ.Health < Player.Instance.GetSpellDamage(targetKSQ, SpellSlot.Q))
                {
                    SpellsManager.Q.Cast(predQ2.CastPosition);
                    return;
                }
            }//////////////////// END KS Q


         


        }

    }
}
