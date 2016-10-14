using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Eclipse
{
    public static class SpellsManager
    {

        private static AIHeroClient myHero
        {
            get { return Player.Instance; }
        }

        //Remenber of putting the correct type of the spell here
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;


        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 70)
            {
                AllowedCollisionCount = 1
            };

            W = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear);

            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Circular, 250, 1400, 335)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 3340, SkillShotType.Circular, 1000, int.MaxValue, 110)
            {
                AllowedCollisionCount = int.MaxValue
            };

        }


        #region Damages

        public static float GetDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            const DamageType damageType = DamageType.Magical;
            var AD = Player.Instance.FlatPhysicalDamageMod;
            var AP = Player.Instance.FlatMagicDamageMod;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] { 60, 110, 160, 210, 260 }[sLevel] + 0.7f * AP;
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] { 0, 0, 0, 0, 0 }[sLevel] + 0f * AD;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] { 60, 105, 150, 195, 240 }[sLevel] + 0.6f * AP;
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        dmg += new float[] { 300, 400, 500 }[sLevel] + 0.75f * AP;
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }

        public static float GetTotalDamage(this Obj_AI_Base target)
        {
            var dmg =
                Player.Spells.Where(
                    s => (s.Slot == SpellSlot.Q) || (s.Slot == SpellSlot.W) || (s.Slot == SpellSlot.E) || (s.Slot == SpellSlot.R) && s.IsReady)
                    .Sum(s => target.GetDamage(s.Slot));

            return dmg + Player.Instance.GetAutoAttackDamage(target);
        }

        public static float GetPassiveDamage(this Obj_AI_Base target)
        {
            var rawDamage = new float[] { 18, 26, 34, 42, 50, 58, 66, 74, 82, 90, 98, 106, 114, 122, 130, 138, 146, 154 }[Player.Instance.Level] +
                            0.2f * Player.Instance.FlatMagicDamageMod;

            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, rawDamage);
        }

        public static bool HasPassive(this Obj_AI_Base target)
        {
            return target.HasBuff("luxilluminatingfraulein");
        }

        #endregion Damages

        private static SpellSlot GetSlotFromComboBox(this int value)
        {
            switch (value)
            {
                case 0:
                    return SpellSlot.Q;
                case 1:
                    return SpellSlot.W;
                case 2:
                    return SpellSlot.E;
            }
            Chat.Print("Failed getting slot");
            return SpellSlot.Unknown;
        }
    }
}