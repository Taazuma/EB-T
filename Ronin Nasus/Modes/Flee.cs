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
using static Ronin.Menus;
using EloBuddy.SDK.Menu;
using static Ronin.SpellsManager;

//using Settings = RoninTune.Modes.Flee

namespace Ronin.Modes
{
    internal class Flee
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);

            if (E.IsReady() && W.IsReady())
            {
                E.Cast(etarget);
                W.Cast(wtarget);
            }


        }
    }
}



