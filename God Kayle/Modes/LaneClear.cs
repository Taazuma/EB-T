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
        public static void Execute()
        {
            var minions =ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsEnemy && Modes.Combo._player.Distance(m) <= SpellsManager.E.Range);
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, E.Range, true).Count();
            if (count == 0) return;
            if (minions == null) return;

            if (E.IsReady() && FirstMenu["lc.MinionsE"].Cast<Slider>().CurrentValue <= count)
            {
                E.Cast();
            }

        }
    }
}
