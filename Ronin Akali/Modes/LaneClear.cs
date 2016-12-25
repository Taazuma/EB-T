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
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Player.AttackRange, false).Count();
            var source = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(Q.Range));
            if (count == 0) return;
            if (source == null || source.IsInvulnerable || source.MagicImmune)
            {
                return;
            }

            if (Q.IsReady() && LaneClearMenu.GetCheckBoxValue("qUse"))
            {
                Q.Cast(source);
            }

            var QBuff = source.HasBuff("AkaliMota");


            if (QBuff && source.IsValidTarget(Combo._player.GetAutoAttackRange(source)) && LaneClearMenu.GetCheckBoxValue("aaclear"))
            {
                return;
            }

            if (E.IsReady() && LaneClearMenu.GetCheckBoxValue("eUse") && source.IsValidTarget(E.Range))
            {
                E.Cast();
            }


            if (R.IsReady() && LaneClearMenu.GetCheckBoxValue("rUse"))
            {
                R.Cast(source);
            }
        }
    }
}
