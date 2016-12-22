namespace RivenSharp.Core
{
    #region

    using System.Collections.Generic;

    using EloBuddy;
    using EloBuddy.SDK;

    #endregion

    /// <summary>
    /// The core.
    /// </summary>
    internal class BackgroundData : Core
    {
        #region Static Fields

        private static AttackableUnit Unit { get; set; }

        private static bool doublecastQ;

        private static bool canQ;

        private static bool canW;

        /// <summary>
        ///     The e anti spell.
        /// </summary>
        public static List<string> AntigapclosingSpells = new List<string>
                                                    {
                                                        "MonkeyKingSpinToWin", "KatarinaRTrigger", "HungeringStrike",
                                                        "TwitchEParticle", "RengarPassiveBuffDashAADummy",
                                                        "RengarPassiveBuffDash", "IreliaEquilibriumStrike",
                                                        "BraumBasicAttackPassiveOverride", "gnarwproc",
                                                        "hecarimrampattack", "illaoiwattack", "JaxEmpowerTwo",
                                                        "JayceThunderingBlow", "RenektonSuperExecute",
                                                        "vaynesilvereddebuff"
                                                    };

        /// <summary>
        ///     The targeted anti spell.
        /// </summary>
        public static List<string> TargetedSpells = new List<string>
                                                           {
                                                               "MonkeyKingQAttack", "YasuoDash", "FizzPiercingStrike",
                                                               "RengarQ", "GarenQAttack", "GarenRPreCast",
                                                               "PoppyPassiveAttack", "viktorqbuff", "FioraEAttack",
                                                               "TeemoQ"
                                                           };

        /// <summary>
        ///     The w anti spell.
        /// </summary>
        public static List<string> InterrupterSpell = new List<string>
                                                    {
                                                        "RenektonPreExecute", "TalonCutthroat", "IreliaEquilibriumStrike",
                                                        "XenZhaoThrust3", "KatarinaRTrigger", "KatarinaE",
                                                    };

        public static List<string> InvulnerableList = new List<string>
                                                           {
                                                               "FioraW", "kindrednodeathbuff", "Undying Rage", "JudicatorIntervention"
                                                           };

        #endregion

        #region Public Properties

        private static int AnyItem =>
             Item.CanUseItem(3077) && Item.HasItem(3077)
            ? 3077
             : Item.CanUseItem(3074) && Item.HasItem(3074)
              ? 3074
               : Item.CanUseItem(3748) && Item.HasItem(3748)
                ? 3748 
                 : 0;

        public static bool R1 { get; set; }


        public static bool InRange(AttackableUnit x)
        {
            return ObjectManager.Player.HasBuff("RivenFengShuiEngine")
            ? Player.Distance(x) <= 200 + x.BoundingRadius
            : Player.Distance(x) <= 125 + x.BoundingRadius;
        }
        #endregion

        #region Public Methods and Operators

        public static void ForceSkill()
        {
            if (Unit == null)
            {
                return;
            }

            if (canQ)
            {
                if (Item.CanUseItem(AnyItem) && AnyItem != 0 && Qstack == 3)
                {
                    Item.UseItem(AnyItem);
                    EloBuddy.SDK.Core.DelayAction(() => Player.Spellbook.CastSpell(SpellSlot.Q, Unit.Position), 1);
                }
                else
                {
                    Player.Spellbook.CastSpell(SpellSlot.Q, Unit.Position);
                }
            }

            if (canW)
            {
                Player.Spellbook.CastSpell(SpellSlot.W, Unit);

                if (doublecastQ && Spells.Q.IsReady() && Qstack == 1)
                {
                    var delay = Spells.R.IsReady() ? 190 : 90;

                    EloBuddy.SDK.Core.DelayAction(() => Player.Spellbook.CastSpell(SpellSlot.Q, Unit.Position), delay);
                }
            }

            if (!R1 || !Spells.R.IsReady())
            {
                return;
            }
            Player.Spellbook.CastSpell(SpellSlot.R, Player);//TODO: Make sure this shouldnt be Unit...
        }

        public static void DoubleCastQ(AttackableUnit x)
        {
            Unit = x;
            doublecastQ = true;
            EloBuddy.SDK.Core.DelayAction(() => doublecastQ = false, 300);
        }

        public static void CastQ(AttackableUnit x)
        {
            Unit = x;
            canQ = true;
        }

        public static void CastE(AttackableUnit x)
        {
            Unit = x;
        }

        public static void FlashW(AIHeroClient target)
        {
            //var target = TargetSelector.SelectedTarget;
            if (target == null) return;
            Spells.W.Cast();

            EloBuddy.SDK.Core.DelayAction(() => Player.Spellbook.CastSpell(Spells.Flash, target.Position), 10);
            EloBuddy.SDK.Core.DelayAction(() => DoubleCastQ(target), 30);
        }

        public static void CastW(Obj_AI_Base x)
        {
            canW = Spells.W.IsReady() && InRange(x) && !x.HasBuff("FioraW");
            EloBuddy.SDK.Core.DelayAction(() => canW = false, 500);
        }

        public static void ForceR()
        {
            R1 = Spells.R.IsReady() && Spells.R.Name == IsFirstR;
            EloBuddy.SDK.Core.DelayAction(() => R1 = false, 500);
        }

        public static void OnCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            var argsName = args.SData.Name;

            if (argsName.Contains("RivenTriCleave"))
            {
                canQ = false;
                doublecastQ = false;
            }

            if (argsName.Contains("RivenMartyr"))
            {
                canW = false;
                doublecastQ = true;
                EloBuddy.SDK.Core.DelayAction(() => doublecastQ = false, 300);
            }

            if (argsName == IsFirstR)
            {
                R1 = false;
            }

            var target = args.Target as Obj_AI_Base;
            {
                Orbwalker.ResetAutoAttack();
                if (Spells.W.IsReady())
                {
                    var target2 = TargetSelector.GetTarget(Spells.W.Range, DamageType.Physical);
                    if (target2 != null || Orbwalker.ActiveModesFlags != Orbwalker.ActiveModes.None)
                    {
                        Player.Spellbook.CastSpell(SpellSlot.W);
                    }
                }
                return;
            }
        }
        #endregion
    }
}