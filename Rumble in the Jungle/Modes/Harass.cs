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
        public float lastE;
        public static void Execute()
        {

            var target = TargetSelector.GetTarget(1300, DamageType.Magical);

            if (target == null) return;

            if (target.IsValidTarget(Q.Range) && HarassMenu.GetCheckBoxValue("qUse"))
            {
                Q.Cast();
            }

            Core.DelayAction(delegate
            {
                if (target.IsValidTarget(E.Range) && HarassMenu.GetCheckBoxValue("eUse")) /*&& (!Program.ActiveE || FirstMenu.GetSliderValue("eDelay"))*/
            {
                E.Cast(target);
            }
            }, Edelay);
        }
    }
}
