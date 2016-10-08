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
        //public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Player.Instance.HealthPercent <= 25 && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (W.IsReady())
            {
                W.Cast();
            }

            if (Q.IsReady() && Player.Instance.HealthPercent <= 5)
            {
                Q.Cast();
            }

        }
    }
}



