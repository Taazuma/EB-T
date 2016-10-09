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
using static Ronin.Menus;
using static Ronin.SpellsManager;

namespace Ronin.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class JungleClear
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(900));


            if (JungleClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Q.Cast();
                Orbwalker.ForcedTarget = target;
            }

            if (JungleClearMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast(target);
            }

            if (JungleClearMenu.GetCheckBoxValue("eUse") && E.IsReady())
            {
                E.Cast(target.Position);
            }

        }
    }
}