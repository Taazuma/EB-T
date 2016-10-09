using static Eclipse.SpellsManager;
using static Eclipse.Menus;
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

namespace Eclipse.Modes
{
    internal class LastHit
    {
        public static void Execute()
        {
            if (LasthitMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Q.TryToCast(Q.GetLastHitMinion(), LasthitMenu);
            }
        }
    }
}
