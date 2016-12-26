using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
namespace Eclipse
{
    internal class Menus
    {
        public static Menu FirstMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu AutoHarassMenu;
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
        public const string AutoHarassMenuID = "autoharassmenuid";
        public const string LaneClearMenuID = "laneclearmenuid";
        public const string LastHitMenuID = "lasthitmenuid";
        public const string JungleClearMenuID = "jungleclearmenuid";
        public const string KillStealMenuID = "killstealmenuid";
        public const string DrawingsMenuID = "drawingsmenuid";
        public const string MiscMenuID = "miscmenuid";

        public static void CreateMenu()
        {
            FirstMenu = MainMenu.AddMenu("God "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "god");
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.Add(Q.Slot + "hit", new ComboBox("Q HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            //LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            //KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);

            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.AddLabel("ComboLogics");
            ComboMenu.Add("Comba", new ComboBox(" Combo Logic ", 0, "GankCombo", "Teamfight Normal"));
            ComboMenu.AddSeparator(10);
            ComboMenu.AddLabel("Information:");
            ComboMenu.AddSeparator(5);
            ComboMenu.AddLabel("Q - when CC -> R - W - E");
            ComboMenu.AddSeparator(5);
            ComboMenu.AddLabel("Q - R - W - E");
            ComboMenu.AddSeparator(10);
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.CreateCheckBox("Use E", "eUse");
            ComboMenu.AddSeparator(10);
            ComboMenu.CreateCheckBox("Use W", "wUse");
            ComboMenu.CreateCheckBox("Use R", "rUse");
            ComboMenu.AddSeparator(10);
            ComboMenu.Add("enemyr", new Slider("Enemy in R for Teamfight", 3, 1, 5));
            ComboMenu.AddSeparator(10);
            ComboMenu.AddLabel("Humanizer Settings");
            ComboMenu.Add("Qdelay", new Slider("Q Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Wdelay", new Slider("W Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Edelay", new Slider("E Delay (ms)", 0, 0, 300));

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use E", "eUse");
            HarassMenu.AddGroupLabel("Settings");
            HarassMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 50);

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use E", "eUse");
            LaneClearMenu.AddGroupLabel("Settings");
            LaneClearMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 50);

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "wUse");
            JungleClearMenu.CreateCheckBox("Use E", "eUse");
            JungleClearMenu.AddGroupLabel("Settings");
            JungleClearMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 20);

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddSeparator(7);
            MiscMenu.Add("smartW", new CheckBox("Automatic disable W (Smart)"));
            MiscMenu.Add("WSdelay", new Slider("Smart W Delay (ms)", 0, 0, 400));
            MiscMenu.AddSeparator(12);
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", false));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("skinhax", new CheckBox("Activate Skin hack"));
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 0, "Default", "Sad Robot Amumu", "Little Knight Amumu", "Emumu", "Almost-Prom King Amumu", "Pharaoh Amumu", "Surprise Party Amumu", "Re-Gifted Amumu", "Vancouver Amumu"));

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
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

        }
        public static int Qdelay { get { return ComboMenu["Qdelay"].Cast<Slider>().CurrentValue; } }
        public static int Wdelay { get { return ComboMenu["Wdelay"].Cast<Slider>().CurrentValue; } }
        public static int Edelay { get { return ComboMenu["Edelay"].Cast<Slider>().CurrentValue; } }
        public static int WSdelay { get { return MiscMenu["WSdelay"].Cast<Slider>().CurrentValue; } }
    }
}
