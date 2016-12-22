using EloBuddy;
using EloBuddy.SDK;
using RivenSharp.Misc;
using static RivenSharp.Core.Spells;

namespace RivenSharp.Managers
{
    public static class DamageManager
    {
        private const int MinusDamage = 10;

        public static float GetQDamage(this Obj_AI_Base target)
        {
            if (!Q.IsReady()) return 0f;

            var level = Helper.Me.Spellbook.GetSpell(SpellSlot.Q).Level - 1;
            var damage = new[] {0f, 0f, 0f, 0f, 0f}[level] + Helper.Me.TotalAttackDamage*0.0f;
            var dmgQ = Helper.Me.CalculateDamageOnUnit(target, DamageType.Mixed, damage);

            return dmgQ - MinusDamage;
        }

        public static float GetWDamage(this Obj_AI_Base target)
        {
            if (!W.IsReady()) return 0f;

            var level = Helper.Me.Spellbook.GetSpell(SpellSlot.W).Level - 1;
            var damage = new[] {0f, 0f, 0f, 0f, 0f}[level] + Helper.Me.TotalAttackDamage*0.0f;
            var dmgW = Helper.Me.CalculateDamageOnUnit(target, DamageType.Mixed, damage);

            return dmgW - MinusDamage;
        }

        public static float GetEDamage(this Obj_AI_Base target)
        {
            if (!E.IsReady()) return 0f;

            var level = Helper.Me.Spellbook.GetSpell(SpellSlot.E).Level - 1;
            var damage = new[] {0f, 0f, 0f, 0f, 0f}[level] + Helper.Me.TotalAttackDamage*0.0f;
            var dmgE = Helper.Me.CalculateDamageOnUnit(target, DamageType.Mixed, damage);

            return dmgE - MinusDamage;
        }

        public static float GetRDamage(this Obj_AI_Base target)
        {
            if (!R.IsReady()) return 0f;

            var level = Helper.Me.Spellbook.GetSpell(SpellSlot.R).Level - 1;
            var damage = new[] {0f, 0f, 0f}[level] + Helper.Me.TotalAttackDamage*0.0f;
            var dmgR = Helper.Me.CalculateDamageOnUnit(target, DamageType.Mixed, damage);

            return dmgR - MinusDamage;
        }

        public static float GetTotalDamage(this Obj_AI_Base target)
        {
            return target.GetQDamage() + target.GetWDamage() + target.GetEDamage() + target.GetRDamage();
        }
    }
}
