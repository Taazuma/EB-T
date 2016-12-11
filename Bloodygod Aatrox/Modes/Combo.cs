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
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            var ultEnemies = ComboMenu.GetSliderValue("combo.REnemies");
            if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
            {
                return;
            }
            ///////////////////////////////////////////////////////////////////////

            if (useE && E.IsReady() && E.GetPrediction(Target).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                E.Cast(Target.ServerPosition);
            }

            if (Q.IsReady() && Q.GetPrediction(Target).HitChance >= Hitch.hitchance(Q, FirstMenu))
            {
                if (Target.IsUnderHisturret()) return;
                if (ComboMenu.GetSliderValue("Q1") > 0)
                {
                    switch (ComboMenu.GetSliderValue("Q1"))
                    {
                        case 1:
                            Q.Cast(Target.ServerPosition);
                            break;
                        case 2:
                            foreach (var h in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget()))
                            {
                                Q.Cast(h.ServerPosition);
                            }
                            break;
                    }
                }
            }

           if (W.IsReady() && useW)
            {
                if (_player.HealthPercent < ComboMenu.GetSliderValue("combo.minw"))
                {
                    if (_player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 2)
                    {
                        W.Cast();
                    }
                }

                if (_player.HealthPercent > ComboMenu.GetSliderValue("combo.maxw"))
                {
                    if (_player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 1)
                    {
                        W.Cast();
                    }
                }
            }

            if (ComboMenu["RS"].Cast<ComboBox>().CurrentValue == 0 && useR && R.IsReady() && _player.ServerPosition.CountEnemiesInRange(500f) <= ultEnemies)
            {
                R.Cast();
            }

            else if (ComboMenu["RS"].Cast<ComboBox>().CurrentValue == 1 && useR && R.IsReady() && Player.Instance.IsFacing(Target) && _player.ServerPosition.CountEnemiesInRange(500f) <= ultEnemies)
            {
                R.Cast();
            }

            else if (ComboMenu["RS"].Cast<ComboBox>().CurrentValue == 2 && useR && R.IsReady() && Program._player.HealthPercent <= 25 && _player.ServerPosition.CountEnemiesInRange(500f) <= ultEnemies)
            {
                R.Cast();
            }


        }
    }
    }
