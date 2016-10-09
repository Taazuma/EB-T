using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace RoninTune.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Targeted Smite
        {
            get { return SpellManager.Smite; }
        }

        protected bool HasSmite
        {
            get { return SpellManager.HasSmite(); }
        }

        protected AIHeroClient _Player
        {
            get { return Player.Instance; }
        }

        protected Vector3 _PlayerPos
        {
            get { return Player.Instance.Position; }
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
