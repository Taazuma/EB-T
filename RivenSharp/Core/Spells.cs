namespace RivenSharp.Core
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    #region

    #endregion

    internal class Spells : Core
    {
        #region Static Fields

        public static SpellSlot Flash;

        public static SpellSlot Ignite;

        #endregion

        #region Public Properties
        
        public static Spell.Skillshot Q = new Spell.Skillshot(SpellSlot.Q, 150, SkillShotType.Circular, 250, 2200, 100);
        public static Spell.Active W
        {
            get { return new Spell.Active(SpellSlot.W, (uint)((Player.HasBuff("RivenFengShuiEngine") ? 200 : 120))); }
        }
        public static Spell.Skillshot E = new Spell.Skillshot(SpellSlot.E, 310, SkillShotType.Linear);

        //public static Spell.Active R = new Spell.Active(SpellSlot.R);
        public static Spell.Skillshot R = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Cone, 250, 1600, 125);
        
        #endregion

        #region Public Methods and Operators

        public static void Load()
        {
            //Q.SetSkillshot(0.25f, 100f, 2200f, false, SkillshotType.SkillshotCircle);
            //R.SetSkillshot(0.25f, (float)(45 * 0.5), 1600, false, SkillshotType.SkillshotCone);

            Ignite = Player.GetSpellSlotFromName("SummonerDot");
            Flash = Player.GetSpellSlotFromName("SummonerFlash");
        }

        #endregion
    }
}