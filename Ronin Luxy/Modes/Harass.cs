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
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            if (target == null) return;
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Mixed);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            var rtarget = TargetSelector.GetTarget(3400, DamageType.Mixed);
           // var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);

            if (Q.IsReady() && HarassMenu.GetCheckBoxValue("qUse"))
            {
                var predq = Q.GetPrediction(qtarget);
                if (predq.HitChance >= HitChance.High)
                {
                    Q.Cast(predq.CastPosition);
                }
            }

            if (E.IsReady() && HarassMenu.GetCheckBoxValue("eUse"))
            {
                var prede = E.GetPrediction(etarget);
                if (prede.HitChance >= HitChance.High)
                {
                    E.Cast(prede.CastPosition);
                }
            }

        }
    }
}
