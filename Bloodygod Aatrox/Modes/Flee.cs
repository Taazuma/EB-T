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
            ///////////////////////////////////////////////////////////////////////

            if (Q.IsReady())
            {
               Q.Cast(Game.CursorPos);
            }

            if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
            {
                return;
            }

            if (E.IsReady())
            {
                E.Cast(Target.ServerPosition);
            }

        }
    }
}



