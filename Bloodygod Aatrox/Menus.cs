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
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.AddGroupLabel("Prediction");
            FirstMenu.AddSeparator(5);
            FirstMenu.AddGroupLabel("Q Settings");
            FirstMenu.Add(SpellsManager.Q.Slot + "hit", new ComboBox("Q HitChance", 1, "High", "Medium", "Low"));
            FirstMenu.AddGroupLabel("E Settings");
            FirstMenu.Add(SpellsManager.E.Slot + "hit", new ComboBox("E HitChance", 1, "High", "Medium", "Low"));
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddStringList("Q1", "Use Q", new[] { "Never", "Only on target", "On any enemy" }, 1);
            ComboMenu.AddSeparator(5);
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
            ComboMenu.AddLabel("Change Hitchange on the First Menu!");

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
            DrawingsMenu.Add("nodraw", new CheckBox("Disable All Drawings", false));
            DrawingsMenu.AddSeparator();
            DrawingsMenu.Add("drawQ", new CheckBox("Draw Q Range", false));
            DrawingsMenu.Add("drawW", new CheckBox("Draw W Range", false));
            DrawingsMenu.Add("drawR", new CheckBox("Draw R Range", false));
            DrawingsMenu.Add("drawonlyrdy", new CheckBox("Draw Only Ready Spells", true));

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
    }
}
