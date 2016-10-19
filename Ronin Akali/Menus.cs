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
            FirstMenu = MainMenu.AddMenu("Ronin "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "ronin");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            ComboMenu.AddGroupLabel("ComboMenu");
            ComboMenu.AddGroupLabel("ONLY USE ON COMBO");
            ComboMenu.AddSeparator(15);
            // --------------------------------------------------------------COMBO LOGICS-------------------------------------------------------------- //
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.AddLabel("ComboLogics");
            ComboMenu.CreateCheckBox("Normal One", "combo1", false);
            ComboMenu.AddGroupLabel("Combo Q - AA - R - E - W (when Low)");
            ComboMenu.AddSeparator(10);
            ComboMenu.CreateCheckBox("Extend Two", "combo2", true);
            ComboMenu.AddGroupLabel("Combo Q - AA - E - R - W (when Low)");
            ComboMenu.AddSeparator(10);
            ComboMenu.CreateCheckBox("Extend Three", "combo3", false);
            ComboMenu.AddGroupLabel("Combo R - Q - AA - E - W (when Low)");
            ComboMenu.CreateCheckBox("Extend Four", "combo4", false);
            ComboMenu.AddGroupLabel("Combo Q - AA - E - R");
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.CreateCheckBox(" - Use Q", "qUse");
            ComboMenu.CreateCheckBox(" - Use W", "wUse", false);
            ComboMenu.CreateCheckBox(" - Use E", "eUse");
            ComboMenu.CreateCheckBox(" - Use R", "rUse");
            ComboMenu.AddLabel("Humanizer Settings");
            ComboMenu.Add("Qdelay", new Slider("Q Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Edelay", new Slider("E Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Rdelay", new Slider("R Delay (ms)", 0, 0, 300));
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            HarassMenu.CreateCheckBox(" - Use Q", "qUse", false);
            HarassMenu.CreateCheckBox(" - Use E", "eUse");
            HarassMenu.AddGroupLabel("Settings");
            HarassMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 30);
            HarassMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            LaneClearMenu.CreateCheckBox(" - Use Q", "qUse", false);
            LaneClearMenu.CreateCheckBox(" - Use AA Reset", "aaclear");
            LaneClearMenu.CreateCheckBox(" - Use E", "eUse");
            LaneClearMenu.CreateCheckBox(" - Use R", "rUse", false);
            LaneClearMenu.AddGroupLabel("Settings");
            LaneClearMenu.CreateSlider("Mana must be higher than [{0}%] to use LaneClear spells", "manaSlider", 30);
            LaneClearMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            LasthitMenu.CreateCheckBox(" - Use Q", "qUse");
            LasthitMenu.AddGroupLabel("Settings");
            LasthitMenu.CreateSlider("Mana must be higher than [{0}%] to use LastHit spells", "manaSlider", 30);
            LasthitMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            JungleClearMenu.CreateCheckBox(" - Use Q", "qUse");
            JungleClearMenu.CreateCheckBox(" - Use AA Reset", "abclear");
            JungleClearMenu.CreateCheckBox(" - Use E", "eUse");
            JungleClearMenu.CreateCheckBox(" - Use R", "rUse", false);
            JungleClearMenu.AddGroupLabel("Settings");
            JungleClearMenu.CreateSlider("Mana must be higher than [{0}%] to use JungleClear spells", "manaSlider", 30);
            JungleClearMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            KillStealMenu.AddGroupLabel("KillSteal");
            KillStealMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            KillStealMenu.CreateCheckBox(" - Use Q", "qUse");
            KillStealMenu.CreateCheckBox(" - Use E", "eUse", false);
            KillStealMenu.CreateCheckBox(" - Use R", "rUse", false);
            KillStealMenu.AddGroupLabel("Settings");
            KillStealMenu.CreateSlider("Mana must be higher than [{0}%] to use Killsteal spells", "manaSlider", 30);
            KillStealMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw", false);
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw", false);
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw");
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.CreateCheckBox("W when Low", "wlow");
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", false));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("skinhax", new CheckBox("Activate Skin hack"));
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Crimson Akali", "Stinger Akali", "All-Star Akali", "Nurse Akali", "Blood Moon Akali", "Silverfang Akali", "Headhunter Akali", "Sashimi Akali"));

        }
        public static int skinId()
        {
            return MiscMenu["skin.Id"].Cast<Slider>().CurrentValue;
        }
        public static int Qdelay { get { return ComboMenu["Qdelay"].Cast<Slider>().CurrentValue; } }
        public static int Edelay { get { return ComboMenu["Edelay"].Cast<Slider>().CurrentValue; } }
        public static int Rdelay { get { return ComboMenu["Rdelay"].Cast<Slider>().CurrentValue; } }
    }
}
