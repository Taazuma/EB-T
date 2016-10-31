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
        public static AIHeroClient _player { get { return ObjectManager.Player; } }

        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);

            if (target == null) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast(target);
            }

            if (E.IsReady() && target.IsValidTarget(30))
            {
                E.Cast();
            }


        }
    }
}
