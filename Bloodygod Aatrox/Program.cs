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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using Eclipse.Modes;
using EloBuddy.SDK.Menu;

namespace Eclipse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        private static int[] AbilitySequence;


        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Aatrox") return;
            Chat.Print("Have Fun with Playing ! by TaaZ");
            AbilitySequence = new int[] { 3, 2, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            ModeManager.InitializeModes();
            Menus.CreateMenu();
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += AntiGapCloser;
            FpsBooster.Initialize();
        }

        private static void AntiGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!e.Sender.IsValidTarget() || !MiscMenu["gapcloser.e"].Cast<CheckBox>().CurrentValue || e.Sender.Type != _player.Type || !e.Sender.IsEnemy)
            {
                return;
            }

            E.Cast(e.Sender);
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsValidTarget(Q.Range) || e.DangerLevel != DangerLevel.High || e.Sender.Type != _player.Type || !e.Sender.IsEnemy)
            {
                return;
            }
            if (Q.IsReady() && MiscMenu["interrupt.q"].Cast<CheckBox>().CurrentValue)
            {
                Q.Cast(sender);
            }
        }

        public static void LevelUpSpells()
        {
            var qL = _player.Spellbook.GetSpell(SpellSlot.Q).Level + qOff;
            var wL = _player.Spellbook.GetSpell(SpellSlot.W).Level + wOff;
            var eL = _player.Spellbook.GetSpell(SpellSlot.E).Level + eOff;
            var rL = _player.Spellbook.GetSpell(SpellSlot.R).Level + rOff;
            if (qL + wL + eL + rL >= ObjectManager.Player.Level) return;
            var level = new[] { 0, 0, 0, 0 };
            for (var i = 0; i < ObjectManager.Player.Level; i++)
            {
                level[AbilitySequence[i] - 1] = level[AbilitySequence[i] - 1] + 1;
            }
            if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
        }

        public static float GetComboDamage(AIHeroClient unit)
        {
            return GetComboDamage(unit, 0);
        }

        public static float GetComboDamage(AIHeroClient unit, int maxStacks)
        {
            //Thanks to Joker Basic Template //
            var d = 2 * Player.Instance.GetAutoAttackDamage(unit);

            if ((Player.Instance.GetSpellSlotFromName("summonerdot") == SpellSlot.Summoner1 ||
                Player.Instance.GetSpellSlotFromName("summonerdot") == SpellSlot.Summoner2))
                d += Player.Instance.GetSummonerSpellDamage(unit, DamageLibrary.SummonerSpells.Ignite);

            if (ComboMenu.GetCheckBoxValue("qUse") && SpellsManager.Q.IsReady())
                d += Player.Instance.GetSpellDamage(unit, SpellSlot.Q);

            if (ComboMenu.GetCheckBoxValue("eUse") && SpellsManager.E.IsReady())
                d += Player.Instance.GetSpellDamage(unit, SpellSlot.E);

            if (SpellsManager.R.IsReady() && ComboMenu.GetCheckBoxValue("rUse"))
                d += 2 * Player.Instance.GetAutoAttackDamage(unit);

            return (float)d;
        }

    }
}