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

namespace Eclipse.Modes
{
    internal class Combo
    {
        public static AIHeroClient _player { get { return ObjectManager.Player; } }

        public static void Execute()
        {
            ///////////////////////////////////////////////////////////////////
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (rtarget == null) return;
            ///////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////// COMBO 

            if (Q.IsReady() && qtarget.IsValidTarget(Q.Range))
            {
                Q.Cast(qtarget);
            }

            if (E.IsReady())
            {
                E.Cast();
            }

            if (W.IsReady() && _player.HealthPercent <= 40)
            {
                W.Cast(_player);
            }

            if (R.IsReady() && _player.HealthPercent <= FirstMenu.GetSliderValue("hpR") && FirstMenu.GetCheckBoxValue("Saferme"))
            {
                R.Cast(_player);
            }

            else if (R.IsReady() && FirstMenu.GetCheckBoxValue("Saferali"))
            {
                Program.SafeAllies();
            }

            /////////////////////////////////////////////////////////////////// COMBO END


        }
    }
}