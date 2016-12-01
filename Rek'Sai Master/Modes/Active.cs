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

            if (Program._player.IsDead || Program._player.IsRecalling()) return;

            #region KS

            //////////////////// KS Q2
            if (KillStealMenu.GetCheckBoxValue("qUse"))
            {
                var q2target = TargetSelector.GetTarget(Q2.Range, DamageType.Magical);

                if (q2target == null) return;

                if (Q.IsReady())
                {
                    var q2Damage = q2target.GetDamage(SpellSlot.Q);

                    var predictedHealth = Prediction.Health.GetPrediction(q2target, Q2.CastDelay + Game.Ping);

                    if (predictedHealth <= q2Damage && Q2.GetPrediction(q2target).HitChance >= Hitch.hitchance(Q2, FirstMenu))
                    {
                        var rangi = TargetSelector.GetTarget(Program._player.GetAutoAttackRange(), DamageType.Physical);
                        if (q2target.IsValidTarget(Q2.Range))
                        {
                            Q2.Cast(q2target);
                        }
                    }
                }
            }            //////////////////// END KS Q2

            //////////////////// KS E2
            if (KillStealMenu.GetCheckBoxValue("eUse"))
            {
                var e2target = TargetSelector.GetTarget(E2.Range, DamageType.Magical);

                if (e2target == null) return;

                if (E2.IsReady())
                {
                    var e2Damage = e2target.GetDamage(SpellSlot.E);

                    var predictedHealth = Prediction.Health.GetPrediction(e2target, E2.CastDelay + Game.Ping);

                    if (predictedHealth <= e2Damage && E2.GetPrediction(e2target).HitChance >= Hitch.hitchance(E2, FirstMenu))
                    {
                        var rangi = TargetSelector.GetTarget(Program._player.GetAutoAttackRange(), DamageType.Physical);
                        if (e2target.IsValidTarget(E2.Range))
                        {
                            E2.Cast(e2target);
                        }
                    }
                }
            }       //////////////////// END KS E2     

            //////////////////// KS Q2
            var targetKSQ2 = TargetSelector.GetTarget(SpellsManager.Q2.Range, DamageType.Magical);

            if (targetKSQ2 != null && Program.burrowed && KillStealMenu.GetCheckBoxValue("qUse") && SpellsManager.Q2.IsReady())
            {
                var predQ2 = SpellsManager.Q2.GetPrediction(targetKSQ2);
                if (predQ2.HitChance >= HitChance.High && targetKSQ2.Health < Player.Instance.GetSpellDamage(targetKSQ2, SpellSlot.Q))
                {
                    SpellsManager.Q2.Cast(predQ2.CastPosition);
                    return;
                }
            }//////////////////// END KS Q2

            #endregion KS

        }

    }
}
