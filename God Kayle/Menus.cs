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
            FirstMenu = MainMenu.AddMenu("God "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("Just press Space to win");
            FirstMenu.AddLabel("The Addon will do his Job");
            FirstMenu.CreateSlider("Mana Manager", "manaSlider", 45);
            FirstMenu.Add("lc.MinionsE", new Slider("Min. Minions for E ", 2, 0, 10));
            FirstMenu.Add("hpR", new Slider("Use R at % HP", 25));
            FirstMenu.AddLabel("Safer Option");
            FirstMenu.AddSeparator(5);
            FirstMenu.CreateCheckBox("Heal/R Safe Allies?", "Saferali", false);
            FirstMenu.CreateCheckBox("Heal/R Safe Me?", "Saferme", true);
            FirstMenu.AddLabel("Clears Option");
            FirstMenu.CreateCheckBox("Use Q ?", "qUse", true);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.AddSeparator(5);
            DrawingsMenu.AddGroupLabel("Tracker Draws");
            DrawingsMenu.Add("me", new CheckBox("My Path", false));
            DrawingsMenu.Add("ally", new CheckBox("Ally Path", false));
            DrawingsMenu.Add("enemy", new CheckBox("Enemy Path", true));
            DrawingsMenu.AddLabel("Tracker Misc");
            DrawingsMenu.Add("toggle", new KeyBind("Toggle On/Off", true, KeyBind.BindTypes.PressToggle, 'G'));
            DrawingsMenu.Add("eta", new CheckBox("Estimated time of arrival (only me)", true));
            DrawingsMenu.Add("name", new CheckBox("Champion Name", true));
            DrawingsMenu.Add("thick", new Slider("Line Thickness", 2, 1, 5));
            DrawingsMenu.AddGroupLabel("Disable while use orbwalk");
            DrawingsMenu.Add("combo", new CheckBox("Combo", true));
            DrawingsMenu.Add("harass", new CheckBox("Harass", true));
            DrawingsMenu.Add("laneclear", new CheckBox("LaneClear", false));
            DrawingsMenu.Add("lasthit", new CheckBox("LastHit", true));
            DrawingsMenu.Add("flee", new CheckBox("Flee", false));
            DrawingsMenu.AddSeparator(15);

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", true));
            MiscMenu.AddSeparator(15);
            MiscMenu.CreateCheckBox("Activate Skin hack", "skinhax", false);
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Silver Kayle", "Unmasked Kayle", "Battleborn Kayle", "Judgement Kayle", "Aether Wing Kayle", "Riot Kayle", "Iron Inquisitor Kayle"));

        }
    }
}
