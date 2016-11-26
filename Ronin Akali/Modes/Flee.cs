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


namespace Eclipse.Modes
{
    internal class Flee
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;

        public static void Execute()
        {
            if (W.IsReady())
            {
                W.Cast(Game.CursorPos);
            }
            ///////////////////////////////////////////////////////////////////
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            /////////////////////////////////////////////////////////////////////
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
            ///////////////////////////////////////////////////////////////////
        }
    }
}



