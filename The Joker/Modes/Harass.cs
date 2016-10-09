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
            var target = TargetSelector.GetTarget(E.Range - 125, DamageType.Mixed);
            var wtarget = TargetSelector.GetTarget(W.Range - 125, DamageType.Mixed);

            if (target == null) return;


            if (Eclipse.Menus.HarassMenu.GetCheckBoxValue("eUse") && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (W.IsReady() && Eclipse.Menus.HarassMenu.GetCheckBoxValue("wUse"))
            {
                if (!wtarget.IsValidTarget(W.Range))
                {
                    Program.HandleW(wtarget);
                }
            }


        }
    }
}
