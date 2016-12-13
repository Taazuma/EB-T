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
using Color = System.Drawing.Color;
using Eclipse;
using Eclipse_Template.Properties;

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
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        private static int[] AbilitySequence;
        public static int start = 0;
        private static int drawTick;
        private static Sprite introImg;
        public static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Camille") return;
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
            AbilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnNewPath += Obj_AI_Base_OnNewPath;
            FpsBooster.Initialize();
        }

        #region Image

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

#endregion Image

        #region Misc


        public static void LevelUpSpells() // Thanks iRaxe
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

        public static void HailHydra()
        {
            if (Item.HasItem(3074) && Item.CanUseItem(3074)) Item.UseItem(3074); //hydra
            if (Item.HasItem(3077) && Item.CanUseItem(3077)) Item.UseItem(3077); //tiamat
            if (Item.HasItem(3748) && Item.CanUseItem(3748)) Item.UseItem(3748); //titanic             
        }

        public static void Yomuus()
        {
            if (Item.HasItem(3142) && Item.CanUseItem(3142)) Item.UseItem(3142); Item.UseItem(3142);
        }

        public static void AAResetLane()
        {
            var Qmin = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderByDescending(m => m.Health).FirstOrDefault(m => m.IsValidTarget(Q.Range));
            if (Qmin == null || Qmin.IsInvulnerable || Qmin.MagicImmune)
            {
                return;
            }
            if (Q.IsReady() && Q.IsInRange(Qmin))
            {
                Q.Cast(Qmin);
                Orbwalker.ResetAutoAttack();
            }

            if (Q.IsReady() && Q.IsInRange(Qmin) && !Player.HasBuff("CamilleQPrimingStart"))
            {
                Q.Cast(Qmin);
                Orbwalker.ResetAutoAttack();
            }

            if (Q.IsReady() && Q.IsInRange(Qmin) && !Player.HasBuff("CamilleQPrimingComplete"))
            {
                Q.Cast(Qmin);
                Orbwalker.ResetAutoAttack();
            }
            var Imin = EntityManager.MinionsAndMonsters.GetLaneMinions().OrderByDescending(m => m.Health).FirstOrDefault(m => m.IsValidTarget(200));
        }
        public static void AAResetCombo()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }
            // Q Combo
            if (ComboMenu["qUse"].Cast<CheckBox>().CurrentValue && Q.IsReady() && target.IsValidTarget() && !Player.HasBuff("CamilleQPrimingStart"))
            {
                Q.Cast(target);
                Orbwalker.ResetAutoAttack();
            }

            if (ComboMenu["qUse"].Cast<CheckBox>().CurrentValue && Q.IsReady() && target.IsValidTarget())
            {
                Q.Cast(target);
                Orbwalker.ResetAutoAttack();
            }

        }

        #endregion Misc

        #region Draws
        // Draws

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
        #endregion Draws

    }
}