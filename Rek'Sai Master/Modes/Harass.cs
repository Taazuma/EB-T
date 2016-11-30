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
    internal class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Q2.Range, DamageType.Mixed);

            if (target == null) return;

            var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
            var targetQ2 = TargetSelector.GetTarget(850, DamageType.Magical);
            var predq2 = Q2.GetPrediction(targetQ2).HitChance >= Hitch.hitchance(Q2, FirstMenu);
            var targetE = TargetSelector.GetTarget(550, DamageType.Physical);
            var targetE2 = TargetSelector.GetTarget(E2.Range, DamageType.Physical);

            if (HarassMenu.GetCheckBoxValue("qUse") && Q.IsReady() && targetE.IsValidTarget(100))
            {
                Q.Cast();
            }

            if (HarassMenu.GetCheckBoxValue("eUse") && E.IsReady())
            {
                E.Cast(targetE);
            }

            if (HarassMenu.GetCheckBoxValue("q2Use") && Q2.IsReady() && predq2)
            {
                var predQ2 = SpellsManager.Q2.GetPrediction(targetQ2);
                Q2.Cast(predQ2.CastPosition);
            }

        }
    }
}
