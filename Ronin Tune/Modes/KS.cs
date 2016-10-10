using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
using System.Drawing;
using static RoninTune.Menus;
using static RoninTune.SpellsManager;

//using Settings = RoninTune.Modes.Flee

namespace RoninTune.Modes
{
    internal class KS
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;
  
        public static void Execute()
        {
            {

                foreach (
                    var target in
                        EntityManager.Heroes.Enemies.Where(
                            hero =>
                                hero.IsValidTarget(R.Range) && !hero.IsDead && !hero.IsZombie && hero.HealthPercent <= 25))
                {

                    var predQ = Q.GetPrediction(target);
                    if (KillStealMenu.GetCheckBoxValue("rUse") && R.IsReady())
                    {
                        R.Cast();
                        R1.Cast(target);
                    }

                    if (KillStealMenu.GetCheckBoxValue("qUse") && Q.IsReady())
                        Player.GetSpellDamage(target, SpellSlot.Q);
                    {
                        if (predQ.HitChance >= HitChance.High)
                        {
                            Q.Cast(target.Position);
                        }
                    }

                    if (KillStealMenu.GetCheckBoxValue("eUse") && E.IsReady() &&
                        target.Health + target.AttackShield <
                        Player.GetSpellDamage(target, SpellSlot.E))
                    {
                        E.Cast(target);
                    }


                }
            }
        }
    }
}



