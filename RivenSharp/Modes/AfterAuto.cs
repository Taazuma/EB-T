namespace RivenSharp.Event.OrbwalkingModes
{
    #region

    using System.Linq;

    using Core;

    //using Orbwalking = Orbwalking;
    using EloBuddy;
    using EloBuddy.SDK;
    using RivenSharp.Misc;
    #endregion

    internal class AfterAuto : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        // Jungle, Combo etc.
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe || !Orbwalker.IsAutoAttacking)//!Orbwalking.IsAutoAttack(args.SData.Name))
            {
                return;
            }
            var a = TargetSelector.GetTarget(Helper.Me.AttackRange + 360, DamageType.Physical);

            if (a == null || a.IsInvulnerable || a.MagicImmune)
            {
                return;
            }

            if (a.HasBuff("FioraW") && Qstack == 3)
                {
                    return;
                }
                if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && !MenuConfig.BurstEnabled)
                {
                    if (Spells.Q.IsReady())
                    {
                        Usables.CastYoumoo();
                        BackgroundData.CastQ(a);
                    }
                }
                if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    if (Qstack == 2)
                    {
                        BackgroundData.CastQ(a);
                    }
                }

                if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || !MenuConfig.BurstEnabled) return;

                if (Spells.Q.IsReady())
                {
                    BackgroundData.CastQ(a);
                }
            
            if(!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            //if (Orbwalker.ActiveMode != Orbwalking.OrbwalkingMode.LaneClear)
            {
                return;
            }

            if (args.Target is Obj_AI_Minion)
            {
                if (MenuConfig.LaneEnemy && ObjectManager.Player.CountEnemiesInRange(1500) > 0)
                {
                    return;
                }
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Position, Player.AttackRange + 360);
                foreach (var m in minions)
                {
                    if (!MenuConfig.LaneQ || (m.IsUnderEnemyturret() && ObjectManager.Player.CountEnemiesInRange(1500) >= 1))
                    {
                        return;
                    }

                    if (Spells.Q.IsReady())
                    {
                        BackgroundData.CastQ(m);
                    }
                }
                var mobs = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Position, 360);
                if (mobs == null)
                {
                    return;
                }

                foreach (var m in mobs)
                {
                    if (MenuConfig.JnglQ && Spells.Q.IsReady())
                    {
                        BackgroundData.CastQ(m);
                    }

                    if (!Spells.W.IsReady() || !MenuConfig.JnglW || Player.HasBuff("RivenFeint") || !BackgroundData.InRange(m))
                    {
                        return;
                    }

                    BackgroundData.CastW(m);
                }
            }

            if (!Spells.Q.IsReady() || !MenuConfig.LaneQ)
            {
                return;
            }

            var nexus = args.Target as Obj_HQ;

            if (nexus != null && nexus.IsValid)
            {
                IsGameObject = true;
                Spells.Q.Cast(nexus.Position - 500);
            }

            var inhib = args.Target as Obj_BarracksDampener;

            if (inhib != null && inhib.IsValid)
            {
                IsGameObject = true;
                Spells.Q.Cast(inhib.Position - 250);
            }

            var turret = args.Target as Obj_AI_Turret;

            if (turret == null || !turret.IsValid)
            {
                return;
            }

            IsGameObject = true;
            Spells.Q.Cast(turret.Position - 250);
        }

        #endregion
    }
}