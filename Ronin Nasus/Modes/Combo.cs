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
    internal class Combo
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Mixed);
            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);

                if (ComboMenu.GetCheckBoxValue("wUse") && W.IsReady() && wtarget.IsValidTarget(W.Range))
                {
                    W.Cast(wtarget);
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && E.IsReady() && etarget.IsValidTarget(E.Range))
                {
                E.Cast(etarget.Position);
                }

                if (ComboMenu.GetCheckBoxValue("qUse") && Q.IsReady() && qtarget.IsValidTarget(Q.Range))
                 {
                    Q.Cast();
                    Orbwalker.ForcedTarget = qtarget;
                 }

             if (ComboMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range) && Player.Instance.HealthPercent <= ComboMenu.GetSliderValue("hpR"))
            {
                R.Cast();
            }



        }
    }
}