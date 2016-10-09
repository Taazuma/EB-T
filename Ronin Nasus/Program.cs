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
using static Ronin.SpellsManager;
using static Ronin.Menus;
using EloBuddy.SDK.Menu;
using Ronin.Modes;

namespace Ronin
{
    internal class Program
    {
        // ReSharper disable once UnusedParameter.Local
        /// <summary>
        /// The firs thing that runs on the template
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        private static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        /// <summary>
        /// This event is triggered when the game loads
        /// </summary>
        /// <param name="args"></param>
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Nasus") return;
            Chat.Print("Welcome to the Ronin´s BETA ;)");
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            Game.OnUpdate += OnGameUpdate;
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();
            Interrupter.OnInterruptableSpell += Program.Interrupter2_OnInterruptableTarget;
            Interrupter.OnInterruptableSpell += Program.Interrupter3_OnInterruptableTarget;
        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        private static void Interrupter2_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (_player.IsDead) return;
            if (E.IsReady() && sender.IsValidTarget(E.Range) && MiscMenu.GetCheckBoxValue("UseEInt"))
            {
                var predE = E.GetPrediction(sender);
                E.Cast(sender.Position);
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            if (check(MiscMenu, "skinhax")) _player.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);
        }

        private static void Interrupter3_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (_player.IsDead) return;
            if (W.IsReady() && sender.IsValidTarget(W.Range) && MiscMenu.GetCheckBoxValue("UseWInt"))
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                W.Cast(wtarget);
            }
        }

    }
}