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
using System.Diagnostics;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;

        public static void Execute()
        {


            //W autodisable thanks to Sunnyline2
            if (MiscMenu.GetCheckBoxValue("smartW")&& Program.WStatus())
            {
                int monsters = EntityManager.MinionsAndMonsters.CombinedAttackable.Where(monster => monster.IsValidTarget(W.Range * 2)).Count();
                int enemies = EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(W.Range * 2)).Count();
                if (monsters == 0 && enemies == 0)
                    Program.WDisable();
            }
            //// Sunnyline2


            //////////////////// KS Q
            var targetKSQ = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if (targetKSQ != null && KillStealMenu.GetCheckBoxValue("qUse") && SpellsManager.Q.IsReady())
            {
                var predQ2 = SpellsManager.Q.GetPrediction(targetKSQ);
                if (predQ2.HitChance >= HitChance.High && targetKSQ.Health < Player.Instance.GetSpellDamage(targetKSQ, SpellSlot.Q))
                {
                    SpellsManager.Q.Cast(predQ2.CastPosition);
                    return;
                }
            }//////////////////// END KS Q


            //////////////////// KS Q Logic #2
            if (KillStealMenu.GetCheckBoxValue("qUse"))
            {
                var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                if (qtarget == null) return;

                if (Q.IsReady())
                {
                    var qDamage = qtarget.GetDamage(SpellSlot.Q);

                    var predictedHealth = Prediction.Health.GetPrediction(qtarget, Q.CastDelay + Game.Ping);

                    if (predictedHealth <= qDamage && Q.GetPrediction(qtarget).HitChance >= Hitch.hitchance(Q, FirstMenu))
                    {
                        var rangi = TargetSelector.GetTarget(Program._player.GetAutoAttackRange(), DamageType.Physical);
                        if (qtarget.IsValidTarget(Q.Range))
                        {
                            Q.Cast(qtarget);
                        }
                    }
                }
            }            //////////////////// END Logic #2


        }

    }
}
