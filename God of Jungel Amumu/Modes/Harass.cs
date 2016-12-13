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
            var euse = Eclipse.Menus.HarassMenu.GetCheckBoxValue("eUse");
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);

            if (etarget == null || etarget.IsInvulnerable || etarget.MagicImmune)
            {
                return;
            }

            if (E.IsReady() && euse && etarget.IsValidTarget(E.Range))
            {
                E.Cast();
            }
          

        }
    }
}
