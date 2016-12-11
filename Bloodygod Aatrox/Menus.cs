using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

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
            FirstMenu = MainMenu.AddMenu("God " + Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "bloodygod");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            //LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            //KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.CreateCheckBox("Use W", "wUse");
            ComboMenu.AddSeparator(5);
            ComboMenu.Add("combo.minw", new Slider("Min hp to Blood Thirst / Blood Price (W Spell)", 50, 0, 100));
            ComboMenu.Add("combo.maxw", new Slider("Max hp to Blood Thirst / Blood Price (W Spell)", 80, 0, 100));
            ComboMenu.AddSeparator(5);
            ComboMenu.CreateCheckBox("Use E", "eUse");
            ComboMenu.AddSeparator(5);
            ComboMenu.CreateCheckBox("Use R", "rUse");
            ComboMenu.Add("RS", new ComboBox(" R Usage ", 1, "On Cast", "Faceing", "Low HP"));
            ComboMenu.AddSeparator(5);
            ComboMenu.Add("combo.REnemies", new Slider("R Min Enemies >=", 2, 1, 5));
            ComboMenu.AddSeparator(5);

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use Q", "qUse");
            HarassMenu.AddSeparator(5);
            HarassMenu.CreateCheckBox("Use E", "eUse");

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.Add("lc.MinionsQ", new Slider("Min. Minions for Dark Flight ", 3, 0, 10));
            LaneClearMenu.AddSeparator(5);
            LaneClearMenu.CreateCheckBox("Use E", "eUse");
            LaneClearMenu.Add("lc.MinionsE", new Slider("Min. Minions for Blades of Torment ", 3, 0, 10));

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "wUse");
            JungleClearMenu.Add("jungle.minw", new Slider("Min hp to Blood Thirst / Blood Price (W Spell)", 50, 0, 100));
            JungleClearMenu.Add("jungle.maxw", new Slider("Max hp to Blood Thirst / Blood Price (W Spell)", 80, 0, 100));
            JungleClearMenu.CreateCheckBox("Use E", "eUse");

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw", false);
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw", false);
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw", false);
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw", false);
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw", false);
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw", false);
            DrawingsMenu.CreateCheckBox("Show if the Target is killable", "showkilla", false);
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.Add("interrupt.q", new CheckBox("Blades of Torment (Q Spell) to Interrupt"));
            MiscMenu.Add("gapcloser.e", new CheckBox("Blades of Torment (E Spell) on Incoming Gapcloser"));
            MiscMenu.AddSeparator(13);
            MiscMenu.AddLabel("Level Up Function"); 
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", false));
            MiscMenu.AddSeparator(13);
            MiscMenu.CreateCheckBox("Activate Skin hack", "skinhax", false);
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Justikar Aatrox", "Mecha Aatrox", "Sea Hunter Aatrox"));


        }
        public static int skinId()
        {
            return MiscMenu["skin.Id"].Cast<Slider>().CurrentValue;
        }
    }
}
