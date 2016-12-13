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
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (Item.HasItem(3074) && Item.CanUseItem(3074) || Item.HasItem(3077) && Item.CanUseItem(3077) || Item.HasItem(3748) && Item.CanUseItem(3748))
            {
                Program.HailHydra();
            }

                if (JungleClearMenu.GetCheckBoxValue("qUse") && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast();
            }

            if (JungleClearMenu.GetCheckBoxValue("eUse") && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(Game.CursorPos);
            }

            if (JungleClearMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast(target.Position);
            }

        }
    }
}
