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
using static RoninSkarner.Menus;
using static RoninSkarner.SpellsManager;

namespace RoninSkarner.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class Combo
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(1500, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var enemiese = EntityManager.Heroes.Enemies.OrderByDescending
             (a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(Player) <= E.Range);

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (E.IsReady() && ComboMenu.GetCheckBoxValue("eUse"))    
           {
                var predictionE = E.GetPrediction(target);
                if (predictionE.HitChance >= HitChance.High)
                { 
                    E.Cast(predictionE.CastPosition);
                }
                else
                {
                    E.TryToCast(target, ComboMenu);
                }
            }

            if (ComboMenu.GetCheckBoxValue("qUse") && target.IsValidTarget(SpellsManager.Q.Range) && Q.IsReady())
            {
                Q.Cast();
            }

            if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 0 && W.IsReady())
            {
                W.Cast();
            }

            else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 1 && W.IsReady() && target.IsValidTarget(SpellsManager.W.Range))
            {
                W.Cast();
            }

            else if (ComboMenu["WC"].Cast<ComboBox>().CurrentValue == 2 && W.IsReady() && target.IsValidTarget(SpellsManager.W.Range) && Player.IsFacing(target))
            {
                W.Cast();
            }

            var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);

            if (ComboMenu.GetCheckBoxValue("rUse") && ComboMenu.GetCheckBoxValue("manu.ult"))
            {
                return;
            }

            else if (ComboMenu.GetCheckBoxValue("rUse") && target.IsValidTarget(R.Range + 50) && R.IsReady())
            { 
                foreach (var ultenemies in enemies)
            {
                var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
            {
                        if (useR) R.Cast(ultenemies);
            }
            }
            }






            }
    }
}
