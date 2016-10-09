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
            var useQ = MiscMenu.GetCheckBoxValue("qescape");
            var useC = MiscMenu.GetCheckBoxValue("rescape");
            if (Q.IsReady() && useQ)
            { 
            Q.Cast(Game.CursorPos);
            }

            var clone = Program.getClone();

            if (clone != null && useC)
            {
                var pos = Game.CursorPos.Extend(clone.Position, clone.Distance(Game.CursorPos) + 2000);
                R.Cast(pos);
            }

        }
    }
}



