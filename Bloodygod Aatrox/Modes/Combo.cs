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
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {

            ///////////////////////////////////////////////////////////////////////
            var Target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var useQ = ComboMenu.GetCheckBoxValue("qUse");
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            var ultEnemies = ComboMenu.GetSliderValue("combo.REnemies");
            if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
            {
                return;
            }
            ///////////////////////////////////////////////////////////////////////

            if (useE && E.IsReady())
            {
                E.Cast(Target.ServerPosition);
            }

            if (useQ && Q.IsReady())
            {
                if (!Target.HasBuff("AatroxQ"))
                {
                    Q.Cast(Target.ServerPosition);
                }
            }

            if (W.IsReady() && useW)
            {
                if (W.IsReady() && _player.HealthPercent < ComboMenu.GetSliderValue("combo.minw"))
                {
                    if (_player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 2)
                    {
                        W.Cast();
                    }
                }

                if (W.IsReady() && _player.HealthPercent > ComboMenu.GetSliderValue("combo.maxw"))
                {
                    if (_player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 1)
                    {
                        W.Cast();
                    }
                }
            }

         
            if (useR && R.IsReady() && Player.Instance.IsFacing(Target) && _player.ServerPosition.CountEnemiesInRange(500f) <= ultEnemies)
            {
                R.Cast();
            }

        }
    }
    }
