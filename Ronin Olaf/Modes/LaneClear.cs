using static Eclipse.SpellsManager;
using static Eclipse.Menus;
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
using Eclipse.Managers;
using static Eclipse.Misc.Helper;

namespace Eclipse.Modes
{
    internal class LaneClear
    {
        public static void Execute()
        {
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program._player.ServerPosition, Program._player.AttackRange, false).Count();
            var source = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderBy(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(Q.Range));
            if (count == 0) return;
            if (source == null || source.IsInvulnerable || source.MagicImmune)
            {
                return;
            }

            var qDamage = DamageManager.GetQDamage(source);

            if (LaneClearMenu.GetComboBoxValue("QSE") <= 0 && Q.IsReady() && LaneClearMenu.GetCheckBoxValue("qUse") && Player.Instance.GetSpellDamage(source, SpellSlot.Q) >= Prediction.Health.GetPrediction(source, Q.CastDelay + Game.Ping))
            {
                Q.Cast(source.ServerPosition);
            }

            else if (LaneClearMenu.GetComboBoxValue("QSE") <= 1 && Q.IsReady() && LaneClearMenu.GetCheckBoxValue("qUse"))
            {
                Q.Cast(source.ServerPosition);
            }

            if (E.IsReady() && LaneClearMenu.GetCheckBoxValue("eUse") && source.IsValidTarget(E.Range))
            {
                E.Cast(source);
            }


        }
    }
}
