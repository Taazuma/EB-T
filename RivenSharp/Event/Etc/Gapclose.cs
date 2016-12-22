namespace RivenSharp.Event.Interrupters_Etc
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Events;
    #region

    using RivenSharp.Core;

    #endregion

    internal class Gapclose : RivenSharp.Core.Core
    {
        #region Public Methods and Operators
        
        public static void Gapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy || !sender.IsValidTarget(Spells.W.Range + sender.BoundingRadius))
            {
                return;
            }

            if (Spells.W.IsReady())
            {
                Spells.W.Cast(sender);
            }

            if (Qstack != 3)
            {
                return;
            }
            Spells.Q.Cast(e.End);
        }
        
        #endregion
    }
}