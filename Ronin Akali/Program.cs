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
using RoninAkali.Properties;
using Colour = SharpDX.Color;

namespace Eclipse
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
        private static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        private static int[] AbilitySequence;
        public static int start = 0;
        public const string ChampName = "Smite";
        private static int drawTick;
        private static Sprite introImg;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Akali") return;
            Core.DelayAction(() =>
            {
                introImg = new Sprite(TextureLoader.BitmapToTexture(Resources.anime));
                Chat.Print("<b><font size='20' color='#4B0082'>Ronin Akali</font><font size='20' color='#FFA07A'> Loaded</font></b>");
                Drawing.OnDraw += DrawingOnOnDraw;
                Core.DelayAction(() =>
                {
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }, 7000);
            }, 2000);
            AbilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            FpsBooster.Initialize();
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Game.OnUpdate += OnGameUpdate;
            Game.OnTick += GameOnTick;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnNewPath += Obj_AI_Base_OnNewPath;
            SpellManager.Initialize();
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading Smite.", System.Drawing.Color.MediumVioletRed);
                return;
            }
            Config.Initialize();
            ModeManagerSmite.Initialize();
            Events.Initialize();
        }

        private static void OnGameUpdate(EventArgs args)
        {
            if (check(MiscMenu, "skinhax")) _player.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);
        }

        private static void GameOnTick(EventArgs args)
        {
            if (MiscMenu["lvlup"].Cast<CheckBox>().CurrentValue) LevelUpSpells();
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
        }// Thanks iRaxe

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy) return;

            if (sender.IsValidTarget(SpellsManager.R.Range) && SpellsManager.W.IsReady())
            {
                SpellsManager.W.Cast(Player.Instance);
            }
        }

        //#region Wall

        //public static Vector2? GetFirstWallPoint(Vector3 from, Vector3 to, float step = 25)
        //{
        //    return GetFirstWallPoint(from.To2D(), to.To2D(), step);
        //}

        //public static Vector2? GetFirstWallPoint(Vector2 from, Vector2 to, float step = 25)
        //{
        //    var direction = (to - from).Normalized();

        //    for (float d = 0; d < from.Distance(to); d = d + step)
        //    {
        //        var testPoint = from + d * direction;
        //        var flags = NavMesh.GetCollisionFlags(testPoint.X, testPoint.Y);
        //        if (flags.HasFlag(CollisionFlags.Wall) || flags.HasFlag(CollisionFlags.Building))
        //        {
        //            return from + (d - step) * direction;
        //        }
        //    }

        //    return null;
        //}

        //public static void Escapeterino()
        //{
        //    // Walljumper credits to Hellsing
        //    // We need to define a new move position since jumping over walls
        //    // requires you to be close to the specified wall. Therefore we set the move
        //    // point to be that specific piont. People will need to get used to it,
        //    // but this is how it works.
        //    var wallCheck = GetFirstWallPoint(_player.Position, Game.CursorPos);

        //    // Be more precise
        //    if (wallCheck != null) wallCheck = GetFirstWallPoint((Vector3)wallCheck, Game.CursorPos, 5);

        //    // Define more position point
        //    var movePosition = wallCheck != null ? (Vector3)wallCheck : Game.CursorPos;

        //    // Update fleeTargetPosition
        //    var tempGrid = NavMesh.WorldToGrid(movePosition.X, movePosition.Y);

        //    // Only calculate stuff when our Q is up and there is a wall inbetween
        //    if (W.IsReady() && wallCheck != null)
        //    {
        //        // Get our wall position to calculate from
        //        var wallPosition = movePosition;

        //        // Check 300 units to the cursor position in a 160 degree cone for a valid non-wall spot
        //        var direction = (Game.CursorPos.To2D() - wallPosition.To2D()).Normalized();
        //        float maxAngle = 80;
        //        var step = maxAngle / 20;
        //        float currentAngle = 0;
        //        float currentStep = 0;
        //        var jumpTriggered = false;
        //        while (true)
        //        {
        //            // Validate the counter, break if no valid spot was found in previous loops
        //            if (currentStep > maxAngle && currentAngle < 0) break;

        //            // Check next angle
        //            if ((currentAngle == 0 || currentAngle < 0) && currentStep != 0)
        //            {
        //                currentAngle = currentStep * (float)Math.PI / 180;
        //                currentStep += step;
        //            }

        //            else if (currentAngle > 0) currentAngle = -currentAngle;

        //            Vector3 checkPoint;

        //            // One time only check for direct line of sight without rotating
        //            if (currentStep == 0)
        //            {
        //                currentStep = step;
        //                checkPoint = wallPosition + W.Range * direction.To3D();
        //            }
        //            // Rotated check
        //            else checkPoint = wallPosition + W.Range * direction.Rotated(currentAngle).To3D();

        //            // Check if the point is not a wall
        //            if (!checkPoint.IsWall())
        //            {
        //                // Check if there is a wall between the checkPoint and wallPosition
        //                wallCheck = GetFirstWallPoint(checkPoint, wallPosition);
        //                if (wallCheck != null)
        //                {
        //                    // There is a wall inbetween, get the closes point to the wall, as precise as possible
        //                    var wallPositionOpposite =
        //                        (Vector3)GetFirstWallPoint((Vector3)wallCheck, wallPosition, 5);

        //                    //// Check if it's worth to jump considering the path length
        //                    //if (_player.GetPath(wallPositionOpposite).ToList().ToLookup().PathLength()
        //                    //    - _player.Distance(wallPositionOpposite) > 200) //200
        //                    //{
        //                    // Check the distance to the opposite side of the wall
        //                    if (_player.Distance(wallPositionOpposite, true)
        //                        < Math.Pow(W.Range + 200 - _player.BoundingRadius / 2, 2))
        //                    {
        //                        // Make the jump happen
        //                        W.Cast(wallPositionOpposite);

        //                        // Update jumpTriggered value to not orbwalk now since we want to jump
        //                        jumpTriggered = true;

        //                        break;
        //                    }
        //                    //}

        //                    //else
        //                    //{
        //                    //    // yolo
        //                    //    Render.Circle.DrawCircle(Game.CursorPos, 35, Color.Red, 2);
        //                    //}
        //                }
        //            }
        //        }
        //        // Check if the loop triggered the jump, if not just orbwalk
        //        if (!jumpTriggered)
        //        {
        //            Orbwalker.OrbwalkTo(Game.CursorPos);
        //        }
        //    }

        //    // Either no wall or W on cooldown, just move towards to wall then
        //    else
        //    {
        //        Orbwalker.OrbwalkTo(Game.CursorPos);
        //        if (W.IsReady()) W.Cast(Game.CursorPos);
        //    }
        //}
        //#endregion Wall



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