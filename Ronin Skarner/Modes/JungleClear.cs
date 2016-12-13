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
    internal class JungleClear
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(1000));

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (JungleClearMenu.GetCheckBoxValue("qUse") && Q.IsReady() && target.IsValidTarget(SpellsManager.Q.Range))
            {
                Q.Cast();
            }
            if (JungleClearMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast();
            }
            if (JungleClearMenu.GetCheckBoxValue("eUse") && E.IsReady() && target.IsValidTarget(SpellsManager.E.Range))
            {
                E.Cast(target.Position);
            }




        }
    }
}