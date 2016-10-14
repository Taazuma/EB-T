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
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, E.Range, true).Count();
            if (count == 0) return;
            var source = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(Q.Range));

            if (E.IsReady() && LaneClearMenu.GetCheckBoxValue("eUse") && LaneClearMenu["lc.MinionsE"].Cast<Slider>().CurrentValue <= count)
            {
                E.Cast(source);
                return;
            }

            if (LaneClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Q.TryToCast(Q.GetLastHitMinion(), LaneClearMenu);
            }

        }
    }
}
