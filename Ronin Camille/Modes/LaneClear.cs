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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Orbwalker.ForcedTarget = null;
            var useQ = LaneClearMenu.GetCheckBoxValue("qUse");
            var useW = LaneClearMenu.GetCheckBoxValue("wUse");
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Active._player.ServerPosition, E.Range, false).Count();
            var sourceq = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Active._player.ServerPosition, Q.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var sourcee = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Active._player.ServerPosition, E.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var qDamage = sourceq.GetDamage(SpellSlot.Q);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (sourceq == null) return;
            if (count == 0) return;

            if (Q.IsReady() && useQ)
            {
                Q.Cast();
            }

            if (!W.IsReady() || !useW || LaneClearMenu["lc.MinionsW"].Cast<Slider>().CurrentValue > count) return;
            var prediction = W.GetPrediction(sourcee);

            if (prediction.HitChance >= HitChance.High)
            {
                W.Cast(sourcee.Position);
            }



        }
    }
}
