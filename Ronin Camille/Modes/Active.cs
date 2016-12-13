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
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {

            if (_player.IsDead || _player.IsRecalling()) return;

            var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (KillStealMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);

                if (wtarget == null) return;

                if (W.IsReady())
                {
                    var wDamage = wtarget.GetDamage(SpellSlot.W);

                    var predictedHealth = Prediction.Health.GetPrediction(wtarget, W.CastDelay + Game.Ping);

                    if (predictedHealth <= wDamage)
                    {
                        var pred = W.GetPrediction(wtarget);
                        if (pred.HitChancePercent >= 90)
                        {
                            W.Cast(wtarget.Position);
                        }
                    }
                }
            }

        }

    }
}
