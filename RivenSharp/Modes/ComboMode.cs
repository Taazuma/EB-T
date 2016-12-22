namespace RivenSharp.Event.OrbwalkingModes
{
    #region

    using Core;
    using EloBuddy;
    using EloBuddy.SDK;

    #endregion
    using static Eclipse.Menus;
    using Eclipse;
    using EloBuddy.SDK.Menu.Values;
    internal class ComboMode : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        private static bool InWRange(GameObject target) => (Player.HasBuff("RivenFengShuiEngine") && target != null) ? 330 >= Player.Distance(target.Position) : 265 >= Player.Distance(target.Position);

        public static void Combo()
        {
            var targetAquireRange = Spells.R.IsReady() ? Player.AttackRange + 390 : Player.AttackRange + 370;
            var target = TargetSelector.GetTarget(targetAquireRange, DamageType.Physical);
            //var target = TargetSelector.GetTarget(targetAquireRange, DamageType.Physical, Player.Position);

            if (target == null || !target.IsValidTarget() || target.Type != Player.Type) return;

            if (Spells.R.IsReady() && Spells.R.Name == IsSecondR)
            {
                var pred = Spells.R.GetPrediction(target);//, true, collisionable: new[] { CollisionableObjects.YasuoWall });

                if (pred.HitChance != EloBuddy.SDK.Enumerations.HitChance.High || target.HasBuff(BackgroundData.InvulnerableList.ToString()))// || Player.IsWindingUp)
                {
                    //return;
                }
                else
                {
                    if ((!MenuConfig.OverKillCheck && Qstack > 1) || MenuConfig.OverKillCheck
                    && (target.HealthPercent <= 40
                    && !Spells.Q.IsReady() && Qstack == 1 || target.Distance(Player) >= Player.AttackRange + 310))
                    {
                        Player.Spellbook.CastSpell(SpellSlot.R, pred.UnitPosition);
                    }
                }
            }

            #region Q3 Wall

            if (Qstack == 3 && target.Distance(Player) >= Player.AttackRange && target.Distance(Player) <= 650 && MenuConfig.Q3Wall && Spells.E.IsReady())
            {
                var wallPoint = FleeLogic.GetFirstWallPoint(Player.Position, Player.Position.Extend(target.Position, 650).To3D());//TODO: Fix this...

                Player.GetPath(wallPoint);

                if (!Spells.E.IsReady() || wallPoint.Distance(Player.Position) > Spells.E.Range || !wallPoint.IsValid())
                {
                    //return;
                }
                else
                {
                    Player.Spellbook.CastSpell(SpellSlot.E, wallPoint);

                    if (Spells.R.IsReady() && Spells.R.Name == IsFirstR)
                    {
                        Player.Spellbook.CastSpell(SpellSlot.R, target);
                    }

                    EloBuddy.SDK.Core.DelayAction(() => Player.Spellbook.CastSpell(SpellSlot.Q, wallPoint), 190);

                    if (wallPoint.Distance(Player.Position) <= 100)
                    {
                        Player.Spellbook.CastSpell(SpellSlot.Q, wallPoint);
                    }
                }
            }
            #endregion

            if (Spells.E.IsReady())
            {
                //Chat.Print("I casted E toward " + target.Name);
                Usables.CastYoumoo();

                if (MenuConfig.AlwaysR && Spells.R.IsReady() && !Spells.R.IsOnCooldown && Spells.R.Name == IsFirstR)
                {
                    Player.Spellbook.CastSpell(SpellSlot.R, target);
                }
                else
                {
                    Player.Spellbook.CastSpell(SpellSlot.E, target);
                    //if (Player.Distance(target) < 350)

                    EloBuddy.SDK.Core.DelayAction(Usables.CastHydra, 10);
                }
            }

            if (Spells.W.IsReady() && BackgroundData.InRange(target))
            {
                if (MenuConfig.Doublecast && Spells.Q.IsReady() && Qstack != 2)
                {
                    BackgroundData.CastW(target);
                    BackgroundData.DoubleCastQ(target);
                }
                else
                {
                    BackgroundData.CastW(target);
                }
            }
            //Added....
            if (Spells.Q.IsReady() && Spells.Q.IsInRange(target))
            {
                BackgroundData.CastQ(target);
                //Player.Spellbook.CastSpell(SpellSlot.Q, target);
            }
    }

        #endregion
    }
}
