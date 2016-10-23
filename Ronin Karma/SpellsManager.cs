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

        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Spell.Active R;


        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 1500, 100);
            W = new Spell.Targeted(SpellSlot.W, 675);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R, 1100);
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

        public static float GetAlliesDamagesNear(this Obj_AI_Base target, float percent = 0.7f, int range = 700)
        {
            var dmg = 0f;
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };

            foreach (var a in EntityManager.Heroes.Allies.Where(a => a.IsInRange(target, range)))
            {
                dmg += a.GetAutoAttackDamage(target);
                dmg += a.Spellbook.Spells.Where(s => slots.Contains(s.Slot) && s.IsReady).Sum(s => a.GetSpellDamage(target, s.Slot));
            }
            return dmg * percent;
        }

        public static float GetEnemiesDamagesNear(this Obj_AI_Base target, float percent = 0.7f, int range = 700)
        {
            var dmg = 0f;
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };

            foreach (var a in EntityManager.Heroes.Allies.Where(a => a.IsInRange(target, range)))
            {
                dmg += a.GetAutoAttackDamage(target);
                dmg += a.Spellbook.Spells.Where(s => slots.Contains(s.Slot) && s.IsReady).Sum(s => a.GetSpellDamage(target, s.Slot));
            }
            return dmg * percent;
        }

        public static float GetTotalDamageEBDB(this Obj_AI_Base target)
        {
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            var dmg =
                Player.Instance.Spellbook.Spells.Where(s => s.IsReady && slots.Contains(s.Slot))
                    .Sum(s => Player.Instance.GetSpellDamage(target, s.Slot));
            var aaDmg = Orbwalker.CanAutoAttack ? Player.Instance.GetAutoAttackDamage(target) : 0f;
            return dmg + aaDmg;
        }

        public static float GetEchoLudenDamage(this Obj_AI_Base target)
        {
            var dmg = 0f;
            var echo = new Item(ItemId.Ludens_Echo);

            if (echo.IsOwned() && Player.GetBuff("itemmagicshankcharge").Count == 100)
            {
                dmg += Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (float)(100 + 0.1 * Player.Instance.FlatMagicDamageMod));
            }
            return dmg;
        }

        public static float GetSheenDamage(this Obj_AI_Base target)
        {
            var sheenItems = new List<Item>
            {
                new Item(ItemId.Lich_Bane),
                new Item(ItemId.Trinity_Force),
                new Item(ItemId.Iceborn_Gauntlet),
                new Item(ItemId.Sheen)
            };
            var item = sheenItems.FirstOrDefault(i => i.IsReady() && i.IsOwned());
            if (item != null)
            {
                var AD = Player.Instance.FlatPhysicalDamageMod;
                var AP = Player.Instance.FlatMagicDamageMod;
                switch (item.Id)
                {
                    case ItemId.Lich_Bane:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, AD * 0.75f + AP * 0.5f);
                    case ItemId.Trinity_Force:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD * 2f);
                    case ItemId.Iceborn_Gauntlet:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD * 1.25f);
                    case ItemId.Sheen:
                        return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, AD * 1f);
                }
            }

            return 0f;
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