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


            var source = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(Q.Range));

            var qDamage = source.GetDamage(SpellSlot.Q);

            if (source == null) return;

            if (E.IsReady())
            {
                E.Cast();
            }

            if (Q.IsReady() && source.Health <= qDamage)
            {
                Q.Cast(source);
            }

        }
    }
}
