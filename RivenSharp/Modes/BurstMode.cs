namespace RivenSharp.Event.OrbwalkingModes
{
    #region

    using Core;
    using Eclipse;
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu.Values;
    #endregion
    using static Eclipse.Menus;
    internal class BurstMode : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        public static void Burst()
        {
            if (Player.Spellbook.CanUseSpell(Spells.Flash) == SpellState.Ready && MenuConfig.AlwaysF)
            {
                var selectedTarget = TargetSelector.GetTarget(Player.AttackRange + 625, DamageType.Physical);

                if (selectedTarget == null
                    || Player.Distance(selectedTarget.Position) > (Player.AttackRange + 625)
                    || Player.Distance(selectedTarget.Position) < Player.AttackRange
                    || (MenuConfig.Flash && selectedTarget.Health > Dmg.GetComboDamage(selectedTarget) && !Spells.R.IsReady())
                    || (!MenuConfig.Flash || (!Spells.R.IsReady() || !Spells.W.IsReady()))
                )
                {
                    return;
                }

                Usables.CastYoumoo();
                BackgroundData.CastE(selectedTarget);

                Player.Spellbook.CastSpell(SpellSlot.R, selectedTarget);
                EloBuddy.SDK.Core.DelayAction(() => BackgroundData.FlashW(selectedTarget), 170);
            }
            else
            {
                var target = TargetSelector.GetTarget(Player.AttackRange + 360, DamageType.Physical);

                if (target == null) return;

                if (!MenuConfig.AlwaysF)
                {
                    Usables.CastYoumoo();
                }

                if (Spells.R.IsReady() && Spells.R.Name == IsSecondR && Qstack > 1)
                {
                    var pred = Spells.R.GetPrediction(target);

                    if (pred.HitChance == EloBuddy.SDK.Enumerations.HitChance.High)
                    {
                        Player.Spellbook.CastSpell(SpellSlot.R, pred.CastPosition);
                    }
                }

                if (Spells.E.IsReady())
                {
                    BackgroundData.CastE(target);
                    EloBuddy.SDK.Core.DelayAction(Usables.CastHydra, 10);
                    //Spells.E.Cast(target.Position);
                }

                if (Spells.R.IsReady() && Spells.R.Name == IsFirstR)
                {
                    Player.Spellbook.CastSpell(SpellSlot.R, target);
                    //Spells.R.Cast(target);
                }

                if (!Spells.W.IsReady() || !BackgroundData.InRange(target))
                {
                    return;
                }

                BackgroundData.CastW(target);
                BackgroundData.DoubleCastQ(target);
            }
        }

        #endregion
    }
}
