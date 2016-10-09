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
        private static AIHeroClient myHero
        {
            get { return Player.Instance; }
        }
        public static void Execute()
        {

            if (R.IsReady() && Player.Instance.HealthPercent <= 35 && Player.Instance.CountEnemiesInRange(R.Range) >= 1)
            {
                var enemyminion =
                    EntityManager.MinionsAndMonsters.EnemyMinions.OrderByDescending(m => m.Distance(Game.CursorPos))
                        .FirstOrDefault(m => m.IsValidTarget(R.Range));
                if (enemyminion == null) return;

                R.Cast(enemyminion);
            }

            if (W.IsReady())
            {
                W.Cast(Player.Instance);
            }
        }
    }
}



