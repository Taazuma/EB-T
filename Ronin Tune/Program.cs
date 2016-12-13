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
using static RoninTune.SpellsManager;
using static RoninTune.Menus;
using RoninTune.Modes;
using EloBuddy.SDK.Menu;
using Color = System.Drawing.Color;
using Ronin_Tune.Properties;

namespace RoninTune
{
    internal class Program
    {
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
        public const string ChampName = "Smite";
        public static Text TextKillable { get; private set; }

        private static int drawTick;
        private static Sprite introImg;
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Nocturne") return;
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.anime));
                Chat.Print("<b><font size='20' color='#4B0082'>Ronin Tune</font><font size='20' color='#FFA07A'> Loaded</font></b>");
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            _W.Initialize();
            _W_Advance.Initialize();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();
            Interrupter.OnInterruptableSpell += Program.Interrupter2_OnInterruptableTarget;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnNewPath += Obj_AI_Base_OnNewPath;
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

        public static int getBoxItem(Menu m, string item)
        {
            return m[item].Cast<ComboBox>().CurrentValue;
        }

        private static void Interrupter2_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (SpellsManager.W.IsReady() && sender.IsValidTarget(SpellsManager.W.Range) && RoninTune.Menus.MiscMenu.GetCheckBoxValue("UseWInt"))
            {
                SpellsManager.W.Cast(sender.Position);
            }
        }

        public static void Items()
        {
            if (Item.HasItem(3074) && Item.CanUseItem(3074)) // Hydra
                Item.UseItem(3074);
            if (Item.HasItem(3077) && Item.CanUseItem(3077)) // Tiamat
                Item.UseItem(3077);
            if (Item.HasItem(3748) && Item.CanUseItem(3748)) // Titanic Hydra
                Item.UseItem(3748);
        }

        public static void ItemsYuno()
        {
            if (Item.HasItem(3142) && Item.CanUseItem(3142)) // Youmuu's
                Item.UseItem(3142);
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
                Player.Instance.GetSpellSlotFromName("summonerdot") == SpellSlot.Summoner2) && RoninTune.SpellManager.Smite.IsReady())
                d += Player.Instance.GetSummonerSpellDamage(unit, DamageLibrary.SummonerSpells.Smite);

            if (ComboMenu.GetCheckBoxValue("qUse") && SpellsManager.Q.IsReady())
                d += Player.Instance.GetSpellDamage(unit, SpellSlot.Q);

            if (ComboMenu.GetCheckBoxValue("eUse") && SpellsManager.E.IsReady())
                d += Player.Instance.GetSpellDamage(unit, SpellSlot.E);

            if (SpellsManager.R.IsReady() && ComboMenu.GetCheckBoxValue("rUse"))
                d += Player.Instance.GetSpellDamage(unit, SpellSlot.R);

            if (_player.IsInAutoAttackRange(unit))
             d += Player.Instance.GetAutoAttackDamage(unit);

            return (float)d;
        }

        private static void Obj_AI_Base_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (!sender.IsMe)
                return;

            start = TickCount;
        }

        public static int TickCount
        {
            get
            {
                return Environment.TickCount & int.MaxValue;
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!DrawingsMenu["toggle"].Cast<KeyBind>().CurrentValue || !Enable())
                return;

            var ETA = DrawingsMenu["eta"].Cast<CheckBox>().CurrentValue;
            var Name = DrawingsMenu["name"].Cast<CheckBox>().CurrentValue;
            var Thickness = DrawingsMenu["thick"].Cast<Slider>().CurrentValue;

            foreach (var hero in EntityManager.Heroes.AllHeroes.Where(h => h.IsValid))
            {
                if (DrawingsMenu["me"].Cast<CheckBox>().CurrentValue && hero.IsMe)
                {
                    DrawPath(Player.Instance, Thickness, Color.LawnGreen);

                    if (ETA && Player.Instance.Path.Length > 1 && Player.Instance.IsMoving)
                        Drawing.DrawText(Player.Instance.Path[Player.Instance.Path.Length - 1].WorldToScreen(), Color.NavajoWhite, GetETA(Player.Instance), 10);

                    continue;
                }

                if (DrawingsMenu["ally"].Cast<CheckBox>().CurrentValue && hero.IsAlly && !hero.IsMe)
                {
                    DrawPath(hero, Thickness, Color.Orange);

                    if (hero.Path.Length > 1 && hero.IsMoving)
                    {
                        if (Name)
                            Drawing.DrawText(hero.Path[hero.Path.Length - 1].WorldToScreen(), Color.LightSkyBlue, hero.BaseSkinName, 10);

                        if (ETA && false)
                            Drawing.DrawText(hero.Path[hero.Path.Length - 1].WorldToScreen() + new Vector2(0, 20), Color.NavajoWhite, GetETA(hero), 10);
                    }

                    continue;
                }

                if (DrawingsMenu["enemy"].Cast<CheckBox>().CurrentValue && hero.IsEnemy)
                {
                    DrawPath(hero, Thickness, Color.Red);

                    if (hero.Path.Length > 1 && hero.IsMoving)
                    {
                        if (Name)
                            Drawing.DrawText(hero.Path[hero.Path.Length - 1].WorldToScreen(), Color.LightSkyBlue, hero.BaseSkinName, 10);

                        if (ETA && false)
                            Drawing.DrawText(hero.Path[hero.Path.Length - 1].WorldToScreen() + new Vector2(0, 20), Color.NavajoWhite, GetETA(hero), 10);
                    }

                    continue;
                }
            }
        }

        public static void DrawPath(AIHeroClient unit, int thickness, Color color)
        {
            if (!unit.IsMoving)
                return;

            for (var i = 1; unit.Path.Length > i; i++)
            {
                if (unit.Path[i - 1].IsValid() && unit.Path[i].IsValid() && (unit.Path[i - 1].IsOnScreen() || unit.Path[i].IsOnScreen()))
                {
                    Drawing.DrawLine(Drawing.WorldToScreen(unit.Path[i - 1]), Drawing.WorldToScreen(unit.Path[i]), thickness, color);
                }
            }
        }

        public static string GetETA(AIHeroClient unit)
        {
            float Distance = 0;

            if (unit.Path.Length > 1)
            {
                for (var i = 1; unit.Path.Length > i; i++)
                {
                    Distance += unit.Path[i - 1].Distance(unit.Path[i]);
                }
            }

            var ETA = (start + Distance / unit.MoveSpeed * 1000 - TickCount) / 1000;

            if (ETA <= 0)
                ETA = 0;

            return ETA.ToString("F2");
        }

        public static bool Enable()
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo && DrawingsMenu["combo"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass && DrawingsMenu["harass"].Cast<CheckBox>().CurrentValue)
                return false;
            if ((Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LaneClear || Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.JungleClear) && DrawingsMenu["laneclear"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LastHit && DrawingsMenu["lasthit"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Flee && DrawingsMenu["flee"].Cast<CheckBox>().CurrentValue)
                return false;
            return true;
        }


    }
}