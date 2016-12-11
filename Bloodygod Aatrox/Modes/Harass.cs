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
            var Target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var ttarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);

            if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
            {
                return;
            }
            var useE = Eclipse.Menus.HarassMenu.GetCheckBoxValue("eUse");

            if (Q.IsReady() && Eclipse.Menus.HarassMenu.GetCheckBoxValue("qUse") && ttarget.IsValidTarget(Q.Range -20) && Q.GetPrediction(ttarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
            {
                Q.Cast(ttarget.Position);
            }

            if (useE && E.IsReady() && Target.IsValidTarget(E.Range - 20) && E.GetPrediction(Target).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                E.Cast(Target.Position);
            }


        }
    }
}
