namespace RivenSharp.Event.OrbwalkingModes
{
    #region

    using Core;
    using EloBuddy.SDK;

    #endregion

    internal class JungleClearMode : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        public static void Jungleclear()
        {
            var mobs = EntityManager.MinionsAndMonsters.GetLaneMinions(
                EntityManager.UnitTeam.Enemy,
                Player.Position,
                Player.AttackRange + 240);

            if (mobs == null) return;

            foreach (var m in mobs)
            {
                if (!m.IsValid
                    || !Spells.E.IsReady() 
                    || !MenuConfig.JnglE 
                    )//|| Player.IsWindingUp)
                {
                    return;
                }

                Spells.E.Cast(m.Position);
                EloBuddy.SDK.Core.DelayAction(Usables.CastHydra, 10);
            }
        }

        #endregion
    }
}
