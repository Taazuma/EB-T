using EloBuddy;
using EloBuddy.SDK;

namespace Eclipse
{
    public static class SpellDamage
    {
        public static float GetTotalDamage(AIHeroClient target)
        {
            // Auto attack
            var damage = Player.Instance.GetAutoAttackDamage(target);

            // Q
            if (SpellsManager.Q.IsReady())
            {
                damage += SpellsManager.Q.GetRealDamage(target);
            }

            // W
            if (SpellsManager.W.IsReady())
            {
                damage += SpellsManager.W.GetRealDamage(target);
            }

            // E
            if (SpellsManager.E.IsReady())
            {
                damage += SpellsManager.E.GetRealDamage(target);
            }

            // R
            if (SpellsManager.R.IsReady())
            {
                damage += SpellsManager.R.GetRealDamage(target);
            }

            return damage;
        }

        public static float GetRealDamage(this Spell.SpellBase spell, Obj_AI_Base target)
        {
            return spell.Slot.GetRealDamage(target);
        }

        public static float GetRealDamage(this SpellSlot slot, Obj_AI_Base target)
        {
            // Helpers
            var spellLevel = Player.Instance.Spellbook.GetSpell(slot).Level;
            const DamageType damageType = DamageType.Magical;
            float damage = 0;

            // Validate spell level
            if (spellLevel == 0)
            {
                return 0;
            }
            spellLevel--;

            switch (slot)
            {
                case SpellSlot.Q:

                    damage = (new [] { 6.25f, 11.25f, 16.25f, 21.25f, 26.25f }[spellLevel] + 0.0835f * Player.Instance.FlatMagicDamageMod) * 10.5f;
                    break;

                case SpellSlot.W:

                    damage = new float[] { 0, 0, 0, 0, 0 }[spellLevel] + 0.0f * Player.Instance.FlatMagicDamageMod;
                    break;

                case SpellSlot.E:

                    damage = new float[] { 45, 70, 95, 120, 145 }[spellLevel] + 0.4f * Player.Instance.FlatMagicDamageMod;
                    break;

                case SpellSlot.R:

                    damage = (new float[] { 130, 185, 240 }[spellLevel] + 0.3f * Player.Instance.FlatMagicDamageMod) * 3.5f;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage) - 10;
        }
    }
}