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
    internal class LaneClear
    {
        public static AIHeroClient Player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Orbwalker.ForcedTarget = null;
            var useQ = LaneClearMenu.GetCheckBoxValue("qUse");
            var useE = LaneClearMenu.GetCheckBoxValue("eUse");
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Q.Range, true).Count();
            var sourceq = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Q.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var sourcee = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, E.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (sourceq == null) return;

            if (Q.IsReady() && sourceq.IsValidTarget(Q.Range) && useQ && LaneClearMenu["lc.MinionsQ"].Cast<Slider>().CurrentValue >= count)
            {
                Q.Cast();
            }


            if (E.IsReady() && useE && sourcee.IsValidTarget(E.Range))
            {
                E.Cast(sourcee);
            }

        }
    }
}
