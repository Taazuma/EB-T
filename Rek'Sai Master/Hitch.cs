namespace Eclipse
{
    using System;
    using System.Collections.Generic;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Notifications;

    using SharpDX;

    internal abstract class Hitch
    {
        //Thanks to Def. Kappa ^)

        public static HitChance hitchance(Spell.SpellBase spell, Menu m)
        {
            switch (m[spell.Slot + "hit"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    {
                        return HitChance.High;
                    }
                case 1:
                    {
                        return HitChance.Medium;
                    }
                case 2:
                    {
                        return HitChance.Low;
                    }
            }
            return HitChance.Unknown;
        }

       



        public static bool IsCC(Obj_AI_Base target)
        {
            return target.IsStunned || target.IsRooted || target.IsTaunted || target.IsCharmed || target.Spellbook.IsChanneling
                   || target.HasBuffOfType(BuffType.Charm) || target.HasBuffOfType(BuffType.Knockback) || target.HasBuffOfType(BuffType.Knockup)
                   || target.HasBuffOfType(BuffType.Snare) || target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Suppression)
                   || target.HasBuffOfType(BuffType.Taunt);
        }

    }
}