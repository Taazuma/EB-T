using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Eclipse.Misc.Spells
{
    public static class SummonerSpells
    {
        public static Spell.Targeted Ignite;
        public static bool PlayerHasIgnite;
        public static Spell.Active Cleanse;
        public static bool PlayerHasCleanse;
        public static Spell.Targeted Exhaust;
        public static bool PlayerHasExhaust;
        public static Spell.Skillshot Flash;
        public static bool PlayerHasFlash;
        public static Spell.Active Ghost;
        public static bool PlayerHasGhost;
        public static Spell.Active Barrier;
        public static bool PlayerHasBarrier;
        public static Spell.Active Heal;
        public static bool PlayerHasHeal;
        public static Spell.Skillshot PoroThrower;
        public static bool PlayerHasPoroThrower;

        public static void InitializeSummonerSpells()
        {
            //Barrier
            var barrier = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerbarrier"));
            if (barrier != null)
            {
                Barrier = new Spell.Active(barrier.Slot);
                PlayerHasBarrier = true;
            }

            //Cleanase
            var cleanse = Player.Instance.GetSpellSlotFromName("summonerboost");
            if (cleanse != SpellSlot.Unknown)
            {
                Cleanse = new Spell.Active(cleanse);
                PlayerHasCleanse = true;
            }
            
            //Exhaust
            var exhaust = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerexhaust"));
            if (exhaust != null)
            {
                Exhaust = new Spell.Targeted(exhaust.Slot, 650);
                PlayerHasExhaust = true;
            }

            //Flash
            var flash = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerflash"));
            if (flash != null)
            {
                Flash = new Spell.Skillshot(flash.Slot, 425, SkillShotType.Circular);
                PlayerHasFlash = true;
            }

            //Ghost
            var ghost = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerghost"));
            if (ghost != null)
            {
                Ghost = new Spell.Active(ghost.Slot);
                PlayerHasGhost = true;
            }

            //Ignite
            var ignite = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerdot"));
            if (ignite != null)
            {
                Ignite = new Spell.Targeted(ignite.Slot, 600);
                PlayerHasIgnite = true;
            }

            //Smite
            var smite = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonersmite"));
            if (smite != null)
            {
                Smite = new Spell.Targeted(smite.Slot, 570);
                PlayerHasSmite = true;
            }
            //Heal
            var heal = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerheal"));
            if (heal != null)
            {
                Heal = new Spell.Active(heal.Slot, 550);
                PlayerHasHeal = true;
            }

            //Snow Ball
            var snowball = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonersnowball"));
            if (snowball != null)
            {
                PoroThrower = new Spell.Skillshot(snowball.Slot, (uint)snowball.SData.CastRange-50, SkillShotType.Linear, 0,
                    1500, (int)snowball.SData.LineWidth)
                {
                    MinimumHitChance = HitChance.High,
                    AllowedCollisionCount = 0
                };
                PlayerHasPoroThrower = true;
            }

            //Poro Mark(Event mode)
            var poro = Player.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerporothrow"));
            if (poro != null)
            {
                PoroThrower = new Spell.Skillshot(poro.Slot, (uint)poro.SData.CastRange-50, SkillShotType.Linear, 0,
                    1500, (int)poro.SData.LineWidth)
                {
                    MinimumHitChance = HitChance.High,
                    AllowedCollisionCount = 0
                };
                PlayerHasPoroThrower = true;
            }
        }

        public static float IgniteDamage()
        {
            return 50 + (20 * Player.Instance.Level);
        }

        public static float GetTotalDamage(Obj_AI_Base target)
        {
            var damage = Player.Spells.Where(s => (s.Slot == SpellSlot.Q || s.Slot == SpellSlot.W || s.Slot == SpellSlot.E || s.Slot == SpellSlot.R) && s.IsReady).Sum(s => Player.Instance.GetSpellDamage(target, s.Slot));
            return (damage + Player.Instance.GetAutoAttackDamage(target)) - 10;
        }

        #region Smite

        public static Spell.Targeted Smite;
        public static bool PlayerHasSmite;

        public static string[] MonsterSmiteables =
        {
            "TT_Spiderboss", "TTNGolem", "TTNWolf", "TTNWraith",
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "SRU_Dragon", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald"
        };

        public static float SmiteDamage()
        {
            return
                new float[] {390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000}[
                    Player.Instance.Level];
        }

        public static float SmiteKSDamage()
        {
            return 20 + 8*Player.Instance.Level;
        }

        #endregion Smite
    }
}
