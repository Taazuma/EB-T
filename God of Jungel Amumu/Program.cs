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
using Color = System.Drawing.Color;

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
        public static int start = 0;
        private static int drawTick;
        private static Sprite introImg;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Amumu") return;
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.god));
                Chat.Print("<b><font size='20' color='#4B0082'>God of Jungel Loaded</font><font size='20' color='#FFA07A'> Loaded</font></b>");
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            AbilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            InitializeSpells();
            CreateMenu();
            ModeManager.InitializeModes();
            Drawing.OnDraw += Drawing_OnDrawSpells;
            FpsBooster.Initialize();
        }

        #region Misc Drawings

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


        public static void Drawing_OnDrawSpells(EventArgs args)
        {
            if (DrawingsMenu["qDraw"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.AntiqueWhite, BorderWidth = 1, Radius = Q.Range }.Draw(_player.Position);
            }

            if (DrawingsMenu["wDraw"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.WhiteSmoke, BorderWidth = 1, Radius = W.Range }.Draw(_player.Position);
            }

            if (DrawingsMenu["eDraw"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.GhostWhite, BorderWidth = 1, Radius = E.Range }.Draw(_player.Position);
            }

            if (DrawingsMenu["rDraw"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.PapayaWhip, BorderWidth = 1, Radius = R.Range }.Draw(_player.Position);
            }
        }


        #endregion Misc Drawings

        public static bool getCheckBoxItem(Menu m, string item)
        {
            return m[item].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderItem(Menu m, string item)
        {
            return m[item].Cast<Slider>().CurrentValue;
        }

        public static bool getKeyBindItem(Menu m, string item)
        {
            return m[item].Cast<KeyBind>().CurrentValue;
        }

        public static bool WStatus()
        {
            if (Player.HasBuff("AuraofDespair"))
            {
                return true;
            }
            return false;
        }

        public static void WEnable()
        {
            if (!WStatus())
                SpellsManager.W.Cast();
            return;
        }

        public static void WDisable()
        {
            if (WStatus())
                SpellsManager.W.Cast();
            return;
        }

    }
}