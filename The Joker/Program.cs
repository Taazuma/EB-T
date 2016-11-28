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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using Eclipse.Modes;
using EloBuddy.SDK.Menu;
using TheJoker.Properties;
using SharpDX;
using Color = System.Drawing.Color;
using Colour = SharpDX.Color;

namespace Eclipse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        public static AIHeroClient player
        {
            get { return ObjectManager.Player; }

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
        public static bool hasGhost = false;
        public static bool GhostDelay;
        public static int GhostRange = 2200;
        public static int LastAATick;
        public static float cloneTime, lastBox;
        public static int start = 0;
        public static bool isDangerousSpell(string spellName, Obj_AI_Base target, Obj_AI_Base hero, Vector3 end, float spellRange)
        {
            if (spellName == "CurseofTheSadMummy")
            {
                if (player.Distance(hero.Position) <= 600f)
                {
                    return true;
                }
            }
            if (IsFacing(target, player.Position) &&
                (spellName == "EnchantedCrystalArrow" || spellName == "rivenizunablade" ||
                 spellName == "EzrealTrueshotBarrage" || spellName == "JinxR" || spellName == "sejuaniglacialprison"))
            {
                if (player.Distance(hero.Position) <= spellRange - 60)
                {
                    return true;
                }
            }
            if (spellName == "InfernalGuardian" || spellName == "UFSlash" ||
                (spellName == "RivenW" && player.HealthPercent < 25))
            {
                if (player.Distance(end) <= 270f)
                {
                    return true;
                }
            }
            if (spellName == "BlindMonkRKick" || spellName == "SyndraR" || spellName == "VeigarPrimordialBurst" ||
                spellName == "AlZaharNetherGrasp" || spellName == "LissandraR")
            {
                if (target.IsMe)
                {
                    return true;
                }
            }
            if (spellName == "TristanaR" || spellName == "ViR")
            {
                if (target.IsMe || player.Distance(target.Position) <= 100f)
                {
                    return true;
                }
            }
            if (spellName == "GalioIdolOfDurand")
            {
                if (player.Distance(hero.Position) <= 600f)
                {
                    return true;
                }
            }
            if (target != null && target.IsMe)
            {
                if (isTargetedCC(spellName) && spellName != "NasusW" && spellName != "ZedUlt")
                {
                    return true;
                }
            }
            return false;
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
        private static int drawTick;
        private static Sprite introImg;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Shaco") return;
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.anime));
                Chat.Print("<b><font size='20' color='#4B0082'>Joker Shaco</font><font size='20' color='#FFA07A'> Loaded</font></b>");
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            Chat.Print("Have Fun with Playing ! by TaaZ");
            AbilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Game.OnUpdate += OnGameUpdate;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCaster;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnNewPath += Obj_AI_Base_OnNewPath;
            FpsBooster.Initialize();
        }


        private static void OnGameUpdate(EventArgs args)
        {
            if (check(MiscMenu, "skinhax")) _player.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);

            if (!ShacoClone)
            {
                cloneTime = System.Environment.TickCount;
            }
            if (ShacoClone && !GhostDelay && MiscMenu["autoMoveClone"].Cast<CheckBox>().CurrentValue)
            {
                moveClone();
            }
            Core.DelayAction(() =>
            {
                if (MiscMenu["lvlup"].Cast<CheckBox>().CurrentValue) LevelUpSpells();
            }, Lvldelay);
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

        public static bool ShacoClone
        {
            get { return _player.Spellbook.GetSpell(SpellSlot.R).Name == "HallucinateGuide"; }
        }

        public static bool ShacoStealth
        {
            get { return _player.HasBuff("Deceive"); }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (ShacoClone)
            {
                var clone = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(m => m.Name == player.Name && !m.IsMe);

                if (args == null || clone == null)
                {
                    return;
                }
                if (hero.NetworkId != clone.NetworkId)
                {
                    return;
                }
                LastAATick = Core.GameTickCount;
            }

            if (args == null || hero == null)
            {
                return;
            }
            if (MiscMenu["usercc"].Cast<CheckBox>().CurrentValue && hero is AIHeroClient && hero.IsEnemy &&
                _player.Distance(hero) < Q.Range &&
                isDangerousSpell(
                    args.SData.Name, args.Target as AIHeroClient, hero, args.End, float.MaxValue))
            {
                R2.Cast();
            }

            if (hero.IsMe && args.SData.Name == "JackInTheBox")
            {
                lastBox = System.Environment.TickCount;
            }

        }

        private static void Obj_AI_Base_OnProcessSpellCaster(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly) return;

            if (MiscMenu.GetCheckBoxValue("evade"))
            { 
            //Need to calc Delay/Time for misille to hit !

            if (DangerDB.TargetedList.Contains(args.SData.Name))
            {
                if (args.Target.IsMe)
                    R.Cast();
            }

            if (DangerDB.CircleSkills.Contains(args.SData.Name))
            {
                if (player.Distance(args.End) < args.SData.LineWidth)
                    R.Cast();
            }

            if (DangerDB.Skillshots.Contains(args.SData.Name))
            {
                if (new Geometry.Polygon.Rectangle(args.Start, args.End, args.SData.LineWidth).IsInside(player))
                {
                    R.Cast();
                }
            }

                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (DangerDB.TargetedList.Contains(args.SData.Name))
                {
                    if (args.Target.IsMe)
                        Q.Cast(target.Position - 30);
                }

                if (DangerDB.CircleSkills.Contains(args.SData.Name))
                {
                    if (player.Distance(args.End) < args.SData.LineWidth)
                        Q.Cast(target.Position - 30);
                }

                if (DangerDB.Skillshots.Contains(args.SData.Name))
                {
                    if (new Geometry.Polygon.Rectangle(args.Start, args.End, args.SData.LineWidth).IsInside(player))
                    {
                        Q.Cast(target.Position - 30);
                    }
                }

            }
        }

        public static bool IsFacing(Obj_AI_Base source, Vector3 target, float angle = 90)
        {
            if (source == null || !target.IsValid())
            {
                return false;
            }
            return
                (double)
                    Geometry.AngleBetween(
                        Geometry.Perpendicular(Extensions.To2D(source.Direction)), Extensions.To2D(target - source.Position)) <
                angle;
        }

        public static List<string> invulnerable =new List<string>(new string[]
        {
                    "sionpassivezombie", "willrevive", "BraumShieldRaise", "UndyingRage", "PoppyDiplomaticImmunity",
                    "LissandraRSelf", "JudicatorIntervention", "ZacRebirthReady", "AatroxPassiveReady", "Rebirth",
                    "alistartrample", "NocturneShroudofDarknessShield", "SpellShield"
        });

        public static List<string> TargetedCC =new List<string>(new string[]
     {
                    "TristanaR", "BlindMonkRKick", "AlZaharNetherGrasp", "VayneCondemn", "JayceThunderingBlow", "Headbutt",
                    "Drain", "BlindingDart", "RunePrison", "IceBlast", "Dazzle", "Fling", "MaokaiUnstableGrowth",
                    "MordekaiserChildrenOfTheGrave", "ZedUlt", "LuluW", "PantheonW", "ViR", "JudicatorReckoning",
                    "IreliaEquilibriumStrike", "InfiniteDuress", "SkarnerImpale", "SowTheWind", "PuncturingTaunt",
                    "UrgotSwap2", "NasusW", "VolibearW", "Feast", "NocturneUnspeakableHorror", "Terrify", "VeigarPrimordialBurst"
    });

        public static bool isTargetedCC(string Spellname)
        {
            return TargetedCC.Contains(Spellname);
        }

        public static Obj_AI_Minion Clone
        {
            get
            {
                if (player.Spellbook.GetSpell(SpellSlot.R).Name != "HallucinateGuide") return null;
                return ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(m => m.Name == player.Name && !m.IsMe);
            }
        }

        public static bool CanCloneAttack(Obj_AI_Minion clone)
        {
            if (clone != null)
            {
                return Core.GameTickCount >=
                       LastAATick + Game.Ping + 100 + (clone.AttackDelay - clone.AttackCastDelay) * 1000;
            }
            return false;
        }

        public static void RSaver()
        {
            var HealthR = MiscMenu["AutoWHP"].Cast<Slider>().CurrentValue;
            if (R.IsLearned && Player.Instance.CountEnemiesInRange(R.Range) >= 1 || _player.HealthPercent != HealthR && R.IsReady() && ComboMenu.GetCheckBoxValue("rLow"))
            {
                R.Cast();
            }
        }

        public static void moveClone()
        {
            var Gtarget = TargetSelector.GetTarget(2200, DamageType.Physical);
            switch (MiscMenu["ghostTarget"].Cast<Slider>().CurrentValue)
            {
                case 0:
                    Gtarget = TargetSelector.GetTarget(2200, DamageType.Physical);
                    break;
                case 1:
                    Gtarget =
                        ObjectManager.Get<AIHeroClient>()
                            .Where(i => i.IsEnemy && !i.IsDead && player.Distance(i) <= 2200)
                            .OrderBy(i => i.Health)
                            .FirstOrDefault();
                    break;
                case 2:
                    Gtarget =
                        ObjectManager.Get<AIHeroClient>()
                            .Where(i => i.IsEnemy && !i.IsDead && player.Distance(i) <= 2200)
                            .OrderBy(i => player.Distance(i))
                            .FirstOrDefault();
                    break;
                default:
                    break;
            }

            if (Clone != null && Gtarget != null && Gtarget.IsValid && !Clone.Spellbook.IsAutoAttacking)
            {
                if (CanCloneAttack(Clone))
                {
                    R.Cast(Gtarget);
                }
                else if (player.HealthPercent > 25)
                {
                    var prediction = Prediction.Position.PredictUnitPosition(Gtarget, 2);
                    R.Cast(
                        Gtarget.Position.Extend(prediction.To3D(), Gtarget.GetAutoAttackRange()));
                }

                GhostDelay = true;
                Core.DelayAction(() => GhostDelay = false, 200);
            }
        }

        public static bool CheckWalls(AIHeroClient target)
        {
            var step = player.Distance(target) / 15;
            for (int i = 1; i < 16; i++)
            {
                if (player.Position.Extend(target.Position, step * i).IsWall())
                {
                    return true;
                }
            }
            return false;
        }

        public static void HandleW(AIHeroClient target)
        {
            var turret =ObjectManager.Get<Obj_AI_Turret>().OrderByDescending(t => t.Distance(target)).FirstOrDefault(t => t.IsEnemy && t.Distance(target) < 3000 && !t.IsDead);
            if (turret != null)
            {
                CastW(target, target.Position, turret.Position);
            }
            else
            {
                if (target.IsMoving)
                {
                    var pred = W.GetPrediction(target);
                    if (pred.HitChance >= HitChance.High)
                    {
                        CastW(target, target.Position, pred.UnitPosition);
                    }
                }
                else
                {
                    W.Cast(player.Position.Extend(target.Position, W.Range - player.Distance(target)));
                }
            }
        }


        public static void CastW(AIHeroClient target, Vector3 from, Vector3 to)
        {
            var positions = new List<Vector3>();

            for (int i = 1; i < 11; i++)
            {
                positions.Add(from.Extend(to, 42 * i));
            }
            var best =
                positions.OrderByDescending(p => p.Distance(target.Position))
                    .FirstOrDefault(
                        p => !p.IsWall() && p.Distance(player.Position) < W.Range && p.Distance(target.Position) > 350);
            if (best != null && best.IsValid())
            {
                W.Cast(best);
            }
        }

        public static Obj_AI_Base getClone()
        {
            Obj_AI_Base Clone = null;
            foreach (var unit in ObjectManager.Get<Obj_AI_Base>().Where(clone => !clone.IsMe && clone.Name == player.Name))
            {
                Clone = unit;
            }

            return Clone;

        }

        public static float ComboDamage(AIHeroClient hero)
        {
            double damage = 0;

            if (E.IsReady())
            {
                damage += player.GetSpellDamage(hero, SpellSlot.E);
            }
            return (float)damage;
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