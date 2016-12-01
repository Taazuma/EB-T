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
    internal class JungleClear
    {
        public static void Execute()
        {
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(900));

            if (target == null) return;

            if (JungleClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Q.Cast();
            }

            if (JungleClearMenu.GetCheckBoxValue("eUse") && E.IsReady())
            {
                E.Cast(target);
            }

            if (JungleClearMenu.GetCheckBoxValue("q2Use") && Q2.IsReady())
            {
                Q2.Cast(target.Position);
            }


        }
    }
}
