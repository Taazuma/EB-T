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
using static RoninSkarner.SpellsManager;
using static RoninSkarner.Menus;

namespace RoninSkarner.Modes
{
    internal class ModeManager
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base unit, Interrupter.InterruptableSpellEventArgs spell)
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Mixed);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);

            if (MiscMenu["interruptq"].Cast<CheckBox>().CurrentValue && Q.IsReady() && E.GetPrediction(etarget).HitChance >= HitChance.High)
            {
                if (unit.Distance(_Player.ServerPosition, true) <= E.Range && E.GetPrediction(qtarget).HitChance >= HitChance.High)
                {
                    E.Cast(qtarget);
                }
            }
        }

        internal static void AntiGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloser)
        {
            if (gapcloser.End.Distance(_Player.ServerPosition) <= 300)
            {
                E.Cast(gapcloser.End.Extend(_Player.Position, _Player.Distance(gapcloser.End) + E.Range).To3D());
            }
        }
        /// <summary>
        /// This event is triggered every tick of the game
        /// </summary>
        /// <param name="args"></param>
        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;

            Active.Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass) && playerMana > HarassMenu.GetSliderValue("manaSlider"))
            {
                Harass.Execute();
            }

            if (playerMana > AutoHarassMenu.GetSliderValue("manaSlider"))
            {
                AutoHarass.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LastHit) && playerMana > LasthitMenu.GetSliderValue("manaSlider"))
            {
                LastHit.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && playerMana > LaneClearMenu.GetSliderValue("manaSlider"))
            {
                LaneClear.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear) && playerMana > JungleClearMenu.GetSliderValue("manaSlider"))
            {
                JungleClear.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Flee.Execute();
            }
        }
    }
}