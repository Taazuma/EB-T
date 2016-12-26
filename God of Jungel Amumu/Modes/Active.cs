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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using System.Diagnostics;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;

        public static void Execute()
        {


            Core.DelayAction(delegate
            {
                //W autodisable thanks to Sunnyline2
                if (MiscMenu.GetCheckBoxValue("smartW")&& Program.WStatus())
            {
                int monsters = EntityManager.MinionsAndMonsters.CombinedAttackable.Where(monster => monster.IsValidTarget(W.Range * 2)).Count();
                int enemies = EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(W.Range * 2)).Count();
                if (monsters == 0 && enemies == 0)
                    Program.WDisable();
            }
            }, WSdelay);
            //// Sunnyline2





        }

    }
}
