namespace RivenSharp.Event.Interrupters_Etc
{
    using EloBuddy;
    using EloBuddy.SDK.Events;
    #region

    using RivenSharp.Core;

    #endregion

    internal class Interrupt2 : Core
    {
        #region Public Methods and Operators
        
        public static void OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!MenuConfig.InterruptMenu || !sender.IsEnemy || !sender.IsValid || !Spells.W.IsInRange(sender) || sender.HasBuff("FioraW"))
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

            Spells.Q.Cast(sender);
        }
        
        #endregion
    }
}