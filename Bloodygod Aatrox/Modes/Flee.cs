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
using static Eclipse.Menus;
using EloBuddy.SDK.Menu;
using static Eclipse.SpellsManager;

//using Settings = RoninTune.Modes.Flee

namespace Eclipse.Modes
{
    internal class Flee
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {

            ///////////////////////////////////////////////////////////////////////
            var Target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var useQ = ComboMenu.GetCheckBoxValue("qUse");
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            var ultEnemies = ComboMenu.GetSliderValue("combo.REnemies");
            if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
            {
                return;
            }
            ///////////////////////////////////////////////////////////////////////

            if (useQ && Q.IsReady())
            {
                if (!Target.HasBuff("AatroxQ"))
                {
                    Q.Cast(Target.ServerPosition);
                }
            }

            if (useE && E.IsReady())
            {
                E.Cast(Target.ServerPosition);
            }

        }
    }
}



