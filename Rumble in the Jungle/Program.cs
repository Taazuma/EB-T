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
        private static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        private static int[] AbilitySequence;
        public static AIHeroClient myhero { get { return ObjectManager.Player; } }
        public float lastE;
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Rumble") return;
            Chat.Print("Have Fun with Playing ! by TaaZ");
            AbilitySequence = new int[] { 3, 2, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Game.OnUpdate += OnGameUpdate;
            Game.OnTick += GameOnTick;
            SpellManager.Initialize();
            Gapcloser.OnGapcloser += AntiGapCloser;

            DMGHandler.DamageHandler.Initialize();
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading Smite.", System.Drawing.Color.Red);
                return;
            }
            Config.Initialize();
            ModeManagerSmite.Initialize();
            Events.Initialize();
        }

        private static void OnGameUpdate(EventArgs args)
        {
            if (check(MiscMenu, "skinhax")) myhero.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);
        }

        private static void GameOnTick(EventArgs args)
        {
            if (MiscMenu["lvlup"].Cast<CheckBox>().CurrentValue) LevelUpSpells();
        }

        private static bool Silenced
        {
            get { return myhero.HasBuff("rumbleoverheat"); }
        }

        private static bool ActiveQ
        {
            get { return myhero.HasBuff("RumbleFlameThrower"); }
        }

        public static bool ActiveE
        {
            get { return myhero.HasBuff("RumbleGrenade"); }
        }

        private static void AntiGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs W)
        {
            if (!W.Sender.IsValidTarget() || !MiscMenu["usewgc"].Cast<CheckBox>().CurrentValue || W.Sender.Type != _player.Type || !W.Sender.IsEnemy)
            {
                return;
            }

            SpellsManager.W.Cast();
        }

        private static void LevelUpSpells() // Thanks iRaxe
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
        }// Thanks Iraxe

    }
}