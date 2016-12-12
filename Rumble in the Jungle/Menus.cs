using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using EloBuddy.SDK;


namespace Eclipse
{
    internal class Menus
    {
        public static Menu FirstMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClearMenu;
        public static Menu LasthitMenu;
        public static Menu JungleClearMenu;
        public static Menu KillStealMenu;
        public static Menu DrawingsMenu;
        public static Menu MiscMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public const string ComboMenuID = "combomenuid";
        public const string HarassMenuID = "harassmenuid";
        public const string LaneClearMenuID = "laneclearmenuid";
        public const string LastHitMenuID = "lasthitmenuid";
        public const string JungleClearMenuID = "jungleclearmenuid";
        public const string KillStealMenuID = "killstealmenuid";
        public const string DrawingsMenuID = "drawingsmenuid";
        public const string MiscMenuID = "miscmenuid";

        public static void CreateMenu()
        {
            FirstMenu = MainMenu.AddMenu("God "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddGroupLabel("Taazuma is back :) !");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.Add(SpellsManager.E.Slot + "hit", new ComboBox("E HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.Add(SpellsManager.R.Slot + "hit", new ComboBox("R HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.AddSeparator(12);
            FirstMenu.Add("eDelay", new Slider("   Delay between E", 2000, 0, 2990));
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.CreateCheckBox("Use E", "eUse");
			ComboMenu.CreateCheckBox("Use normal W", "wUse", false);
            ComboMenu.AddLabel("R Settings");
			ComboMenu.CreateCheckBox("Use R", "rUse");
            ComboMenu.AddLabel("R Logic Select");
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.Add("rlog", new ComboBox(" R Logic ", 2, "Crazy", "Mario", "Pred. Pos."));
            ComboMenu.Add("minR", new Slider("Minimun enemies to use R ?", 2, 0, 5));
            ComboMenu.Add("comboRKill", new CheckBox("Only use R if it will kill at least one ?", false));

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use Q", "qUse");
            HarassMenu.CreateCheckBox("Use E", "eUse");

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.Add("lc.MinionsQ", new Slider("Min. Minions for Q ", 2, 0, 10));
            LaneClearMenu.CreateCheckBox("Use E", "eUse");

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.CreateCheckBox("Use E", "eUse");

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "wUse", false);
            JungleClearMenu.CreateCheckBox("Use E", "eUse");

            KillStealMenu.AddGroupLabel("KillSteal BETA");
            KillStealMenu.CreateCheckBox("Use E", "eUse", false);
            KillStealMenu.CreateCheckBox("Use R", "rUse");

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw", false);
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw", false);
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw");
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw", false);
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw", false);
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw", false);
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.Add("usewgc", new CheckBox("Use W gapclosers", false));
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", false));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("skinhax", new CheckBox("Activate Skin hack"));
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Rumble in the Jungle", "Bilgerat Rumble", "Super Galaxy Rumble"));
            MiscMenu.AddSeparator(15);
            MiscMenu.AddGroupLabel("W Shield Settings:");
            MiscMenu.AddGroupLabel("Danger W Settings");
            MiscMenu.Add("minenemiesinrange", new Slider("Min enemies in the range determined below", 1, 1, 5));
            MiscMenu.Add("minrangeenemy", new Slider("Enemies must be in ({0}) range to be in danger", 1000, 600, 2500));
            MiscMenu.Add("considerspells", new CheckBox("Consider spells ?"));
            MiscMenu.Add("considerskilshots", new CheckBox("Consider SkillShots ?"));
            MiscMenu.Add("consideraas", new CheckBox("Consider Auto Attacks ?"));
            MiscMenu.AddSeparator();
            MiscMenu.AddGroupLabel("Dangerous Spells");
            foreach (var spell in Eclipse.DMGHandler.DangerousSpells.Spells.Where(x => EntityManager.Heroes.Enemies.Any(b => b.Hero == x.Hero)))
            {
                MiscMenu.Add(spell.Hero.ToString() + spell.Slot, new CheckBox(spell.Hero + " - " + spell.Slot + ".", spell.DefaultValue));
            }
        }
        public static int skinId()
        {
            return MiscMenu["skinID"].Cast<Slider>().CurrentValue;
        }
        public static int minenemiesinrange()
        {
            return MiscMenu["minenemiesinrange"].Cast<Slider>().CurrentValue;
        }
        public static int minrangeenemy()
        {
            return MiscMenu["minrangeenemy"].Cast<Slider>().CurrentValue;
        }
        public static int considerspells()
        {
            return MiscMenu["considerspells"].Cast<Slider>().CurrentValue;
        }
        public static int considerskilshots()
        {
            return MiscMenu["considerskilshots"].Cast<Slider>().CurrentValue;
        }
        public static int consideraas()
        {
            return MiscMenu["consideraas"].Cast<Slider>().CurrentValue;
        }
        public static int Edelay { get { return FirstMenu["eDelay"].Cast<Slider>().CurrentValue; } }
    }
}
