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
    internal class AutoHarass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);

            if (qtarget == null) return;

            if (AutoHarassMenu.GetCheckBoxValue("rqUse") && AutoHarassMenu.GetKeyBindValue("autoHarassKey"))
            {
                R.Cast();
                Q.Cast(qtarget);
            }

            if (AutoHarassMenu.GetCheckBoxValue("qUse") && AutoHarassMenu.GetKeyBindValue("autoHarassKey"))
            {
                Q.Cast(qtarget);
            }

        }
    }
}
