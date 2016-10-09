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

namespace Eclipse.Modes
{
    internal class Harass
    {
        public static void Execute()
        {
            var Target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var ttarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (!Target.IsValidTarget())
            {
                return;
            }

            var useE = Eclipse.Menus.HarassMenu.GetCheckBoxValue("eUse");

            if (Q.IsReady() && Eclipse.Menus.HarassMenu.GetCheckBoxValue("qUse") && ttarget.Distance(Eclipse.Modes.LaneClear.Player) <= Q.Range)
            {
                Q.Cast(ttarget);
            }

            if (useE && E.IsReady())
            {
                E.Cast(Target.Position);
            }


        }
    }
}
