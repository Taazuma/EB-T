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
using Eclipse_Template.Properties;

namespace Eclipse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        public class UnitData
        {
            public static string Name;

            public static int StartTime;

            public static void GetName(AIHeroClient unit)
            {
                Name = unit.BaseSkinName;
            }

            public static void GetStartTime(int time)
            {
                StartTime = time;
            }
        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }

        }
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        private static int[] AbilitySequence;
        private static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        private static int drawTick;
        private static Sprite introImg;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Lux") return;
            Chat.Print("Have Fun with Playing ! by TaaZ");
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.anime));
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            AbilitySequence = new int[] { 3, 1, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Game.OnTick += GameOnTick;
            SpellManager.Initialize();
            Interrupter.OnInterruptableSpell += Program.Interrupter_OnInterruptableSpell;
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading Smite.", System.Drawing.Color.Red);
                return;
            }
            Config.Initialize();
            ModeManagerSmite.Initialize();
            Events.Initialize();
        }


        private static void GameOnTick(EventArgs args)
        {
            if (check(MiscMenu, "skinhax")) _player.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);
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


        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base unit, Interrupter.InterruptableSpellEventArgs spell)
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Mixed);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);

            if (MiscMenu["interruptq"].Cast<CheckBox>().CurrentValue && Q.IsReady() && Q.GetPrediction(qtarget).HitChance >= HitChance.High)
            {
                if (unit.Distance(_Player.ServerPosition, true) <= Q.Range && Q.GetPrediction(qtarget).HitChance >= HitChance.High)
                {
                    Q.Cast(qtarget);
                }
            }
        }

        // Draws

        private static void DrawingOnOnDraw(EventArgs args)
        {
            if (drawTick == 0)
                drawTick = Environment.TickCount;

            int timeElapsed = Environment.TickCount - drawTick;
            introImg.CenterRef = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f).To3D();

            int dt = 300;
            if (timeElapsed <= dt)
                introImg.Scale = new Vector2(timeElapsed * 1f / dt, timeElapsed * 1f / dt);
            introImg.Draw(new Vector2(Drawing.Width / 2f - 1415 / 2f, Drawing.Height / 2f - 750 / 2f));
        }

    }
}