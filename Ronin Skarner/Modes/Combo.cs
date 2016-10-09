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

            if (ComboMenu.GetCheckBoxValue("eUse") && etarget.IsValidTarget(SpellsManager.E.Range) && E.IsReady() && E.GetPrediction(etarget).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                foreach (var eenemies in enemiese)
                {
                    var predE = E.GetPrediction(eenemies);
                    { 
                    E.Cast(predE.CastPosition);
                    }
                }
            }

            if (ComboMenu.GetCheckBoxValue("qUse") && target.IsValidTarget(SpellsManager.Q.Range) && Q.IsReady())
            {
                Q.Cast();
            }

            if (ComboMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast();
            }

            if (ComboMenu.GetCheckBoxValue("wrUse") && W.IsReady() && target.IsValidTarget(SpellsManager.W.Range))
            {
                W.Cast();
            }

            var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);
            if (ComboMenu.GetCheckBoxValue("rUse") && ComboMenu.GetCheckBoxValue("manu.ult"))
            {
                return;
            }

            else if (ComboMenu.GetCheckBoxValue("rUse") && target.IsValidTarget(SpellsManager.R.Range) && R.IsReady())
            { 
                foreach (var ultenemies in enemies)
            {
                var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
            {
                        if (useR)
                        R.Cast(ultenemies);
            }
            }
            }


            if (ComboMenu["gankc"].Cast<KeyBind>().CurrentValue)
            {

                if (W.IsReady())
                {
                    W.Cast();
                }

                if (ComboMenu.GetCheckBoxValue("eUse") && etarget.IsValidTarget(SpellsManager.E.Range) && E.IsReady() && E.GetPrediction(etarget).HitChance >= HitChance.High)
                {
                    E.Cast(etarget);
                }

                if (ComboMenu.GetCheckBoxValue("rUse") && target.IsValidTarget(SpellsManager.R.Range) && R.IsReady())
                {
                    foreach (var ultenemies in enemies)
                    {
                        var useR = ComboMenu["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
                        {
                            if (useR)
                                R.Cast(ultenemies);
                        }
                    }
                }

                if (ComboMenu.GetCheckBoxValue("qUse") && target.IsValidTarget(SpellsManager.Q.Range) && Q.IsReady())
                {
                    Q.Cast();
                }
            }



            }
    }
}