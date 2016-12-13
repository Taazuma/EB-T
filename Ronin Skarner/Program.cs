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
using static RoninSkarner.SpellsManager;
using static RoninSkarner.Menus;
using EloBuddy.SDK.Menu;
using RoninSkarner.Modes;
using RoninSkarner.Properties;

namespace RoninSkarner
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
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }

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
        public static int start = 0;
        private static int drawTick;
        private static Sprite introImg;
        public static bool check(Menu submenu, string sig)
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
            if (Player.Instance.ChampionName != "Skarner") return;
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.anime));
                Chat.Print("<b><font size='20' color='#4B0082'>Ronin Skarner</font><font size='20' color='#FFA07A'> Loaded</font></b>");
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            _W.Initialize();
            _W_Advance.Initialize();
            DrawingsManager.InitializeDrawings();
            Interrupter.OnInterruptableSpell += ModeManager.Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += ModeManager.AntiGapCloser;
            FpsBooster.Initialize();
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading Smite.", System.Drawing.Color.Red);
                return;
            }
            Config.Initialize();
            ModeManagerSmite.Initialize();
            Events.Initialize();
        }

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