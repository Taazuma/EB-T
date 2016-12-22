namespace RivenSharp.Event.Misc
{
    #region

    using System;

    using EloBuddy.SDK;
    using EloBuddy;
    using Core;
    using static RivenSharp.Core.Spells;
    using static RivenSharp.Managers.DamageManager;
    #endregion

    internal class KillSteal : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        public static void Update(EventArgs args)
        {
            var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Physical);

            if (target == null || target.IsDead || target.HasBuff(BackgroundData.InvulnerableList.ToString()))
            {
                return;
            }

            if (Spells.W.IsReady()
                && MenuConfig.KsW 
                && target.Health <= Managers.DamageManager.GetWDamage(target)
                && BackgroundData.InRange(target))
            {
                BackgroundData.CastW(target);
            }

            if (Spells.R.IsReady() && Spells.R.Name == IsSecondR && MenuConfig.KsR2)
            {
                var rDmg = Dmg.RDmg(target);
                //Chat.Print("Ult dmg= " + rDmg);
                if (rDmg != 0 && (target.Health + target.TotalShieldHealth()) < rDmg)
                {
                    var pred = Spells.R.GetPrediction(target);
                    Player.Spellbook.CastSpell(SpellSlot.R, pred.CastPosition);
                }
            }

            if (target.Health < Managers.DamageManager.GetQDamage(target) && Spells.Q.IsReady() && Qstack != 3)
            {
                Player.Spellbook.CastSpell(SpellSlot.Q, target.Position);
            }

            if (Player.Spellbook.CanUseSpell(Spells.Ignite) != SpellState.Ready || !MenuConfig.Ignite)
            {
                return;
            }

            if (target.IsValidTarget(600f) && Dmg.IgniteDamage(target) >= target.Health)
            {
                Player.Spellbook.CastSpell(Spells.Ignite, target);
            }
        }

        #endregion
    }
}