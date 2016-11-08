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
    /// This mode will always run
    /// </summary>
    internal class Active
    {

        public static AIHeroClient _player { get { return ObjectManager.Player; } }

        public static void Execute()
        {

            if (_player.IsDead || _player.IsRecalling()) return;

            if (E.IsReady() && MiscMenu.GetCheckBoxValue("eks")) // Start KS
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);

                if (etarget == null) return;

                if (E.IsReady())
                {
                    var eDamage = etarget.GetDamage(SpellSlot.E);

                    if (etarget.Health + etarget.AttackShield <= eDamage)
                    {
                        if (etarget.IsValidTarget(Q.Range))
                        {
                            E.Cast(etarget.Position);
                        }
                    }
                }
            }// END KS

        }
    }
}