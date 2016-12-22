namespace RivenSharp.Event.OrbwalkingModes
{
    #region

    using System.Linq;

    using Core;
    using EloBuddy.SDK;
    using EloBuddy;
    using RivenSharp.Misc;
    #endregion

    internal class FleeMode : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        public static void Flee()
        {

            if (MenuConfig.WallFlee && Player.CountEnemiesInRange(1200) == 0)
            {
                var end = Player.ServerPosition.Extend(Game.CursorPos, 350).To3D();
                var isWallDash = FleeLogic.IsWallDash(end, 350);

                var eend = Player.ServerPosition.Extend(Game.CursorPos, 350).To3D();
                var wallE = FleeLogic.GetFirstWallPoint(Player.ServerPosition, eend);
                var wallPoint = FleeLogic.GetFirstWallPoint(Player.ServerPosition, end);

                Player.GetPath(wallPoint);

                if (Spells.Q.IsReady() && Qstack < 3)
                {
                    Spells.Q.Cast(Game.CursorPos);
                }

                if (Qstack != 3 || !isWallDash) return;

                if (Spells.E.IsReady() && wallPoint.Distance(Player.ServerPosition) <= Spells.E.Range)
                {
                    Spells.E.Cast(wallE);
                    EloBuddy.SDK.Core.DelayAction(() => Spells.Q.Cast(wallPoint), 190);
                }
               else if (wallPoint.Distance(Player.ServerPosition) <= 65)
                {
                    Spells.Q.Cast(wallPoint);
                }
                else
                {
                    EloBuddy.SDK.Orbwalker.MoveTo(wallPoint);
                }
            }
            else
            {
                var enemy = TargetSelector.GetTarget(RivenSharp.Core.Spells.Q.Range, DamageType.Physical);

                if (enemy == null || enemy.IsInvulnerable || enemy.MagicImmune)
                {
                    return;
                }

                var x = Player.Position.Extend(Game.CursorPos, 300).To3D();

                if (Spells.W.IsReady() && enemy.IsValidTarget(200))
                {
                        if (BackgroundData.InRange(enemy))
                        {
                            Spells.W.Cast();
                        }
                }

                if (Spells.Q.IsReady())// && !Player.IsDashing())
                {
                    Spells.Q.Cast(Game.CursorPos);
                }

                if (MenuConfig.FleeYomuu)
                {
                    Usables.CastYoumoo();
                }

                if (Spells.E.IsReady())// && !Player.IsDashing())
                {
                    Spells.E.Cast(x);
                }
            }
        }

        #endregion
    }
}
