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
using static RoninTune.Menus;
using EloBuddy.SDK.Menu;
using static RoninTune.SpellsManager;

namespace RoninTune.Modes
{
    internal class Flee
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {

            var target = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            if (Q.IsReady())
            {
                Q.Cast(Player.ServerPosition.Extend(Game.CursorPos, Q.Range).To3D());
            }

            if (E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

        }
    }
}



