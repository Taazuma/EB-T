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
using static RoninSkarner.Menus;
using static RoninSkarner.SpellsManager;

namespace RoninSkarner.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class LaneClear
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {

            var qtarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && Q.IsInRange(x));
            var wtarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && W.IsInRange(x));
            var etarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && E.IsInRange(x));

            if (LaneClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Q.TryToCast(Q.GetLastHitMinion(), LaneClearMenu);
            }

            if (LaneClearMenu.GetCheckBoxValue("eUse") && E.IsReady())
            {
                E.Cast(qtarget.Position);
            }

        }
    }
}