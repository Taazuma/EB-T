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
    internal class AutoHarass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(W.Range + 100, DamageType.Mixed);

            if (target == null) return;

            if (Menus.AutoHarassMenu.GetKeyBindValue("autoHarassKey"))
            {
                var predw = W.GetPrediction(target);
                if (target.IsValidTarget(W.Range + 50) && W.IsReady() && predw.HitChance >= HitChance.High)
                {
                    W.Cast(predw.CastPosition);
                }
            }

           
        }
    }
}
