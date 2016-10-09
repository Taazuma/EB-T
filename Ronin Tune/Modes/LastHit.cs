
using static RoninTune.Menus;
using static RoninTune.SpellsManager;

namespace RoninTune.Modes
{
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class LastHit
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
            W.TryToCast(W.GetLastHitMinion(), LasthitMenu);
            E.TryToCast(E.GetLastHitMinion(), LasthitMenu);
        }
    }
}