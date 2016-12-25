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
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            if (target == null) return;

            if (Menus.AutoHarassMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                var predictionQ = Q.GetPrediction(target);
                if (predictionQ.HitChance >= HitChance.High)
                {
                    Q.Cast(predictionQ.CastPosition);
                }
            }

            if (W.IsReady())
            {
                W.Cast();
            }

        }
    }
}



