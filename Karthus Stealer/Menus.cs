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
            FirstMenu = MainMenu.AddMenu("K+ "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddGroupLabel("Ultimate Settings");
            FirstMenu.Add("UltKS", new CheckBox("Ultimate KillSteal R", true));
            FirstMenu.Add("UltMode", new ComboBox("Ult Logic", 0, "Kappa Logic"));
            FirstMenu.AddGroupLabel("Kappa Ultimate Logic Settings");
            FirstMenu.Add("RnearE", new CheckBox("Block Ult when Enemies Near My Champion?"));
            FirstMenu.Add("RnearEn", new Slider("Min Enemies Near to block Cast R", 1, 1, 5));
            FirstMenu.Add("Rranged", new Slider("Range to detect Enemies to block Cast R", 1600, 100, 3000));
            FirstMenu.AddGroupLabel("Kill Steal Settings");
            FirstMenu.Add("KS", new CheckBox("Kill Steal Q"));
            FirstMenu.AddLabel("Recommended Range (1600 >)");
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);

   //         ComboMenu.AddGroupLabel("Combo");
   //         ComboMenu.CreateCheckBox("Use Q", "qUse");
   //         ComboMenu.CreateCheckBox("Use E", "eUse");
			//ComboMenu.CreateCheckBox("Use W", "wUse");
			//ComboMenu.CreateCheckBox("Use R", "rUse");

   //         HarassMenu.AddGroupLabel("Harass");
   //         HarassMenu.CreateCheckBox("Use Q", "qUse");
   //         HarassMenu.CreateCheckBox("Use E", "eUse");
   //         HarassMenu.AddGroupLabel("Settings");
   //         HarassMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 30);

   //         AutoHarassMenu.AddGroupLabel("AutoHarass");
   //         AutoHarassMenu.CreateCheckBox("Use Q", "qUse");
   //         AutoHarassMenu.CreateCheckBox("Use E", "eUse");
   //         AutoHarassMenu.AddGroupLabel("Settings");
   //         AutoHarassMenu.CreateSlider("Mana must be higher than [{0}%] to use AutoHarass spells", "manaSlider", 30);

   //         LaneClearMenu.AddGroupLabel("LaneClear");
   //         LaneClearMenu.CreateCheckBox("Use Q", "qUse");
   //         LaneClearMenu.CreateCheckBox("Use E", "eUse");
   //         LaneClearMenu.AddGroupLabel("Settings");
   //         LaneClearMenu.CreateSlider("Mana must be higher than [{0}%] to use LaneClear spells", "manaSlider", 30);

   //         LasthitMenu.AddGroupLabel("Lasthit");
   //         LasthitMenu.CreateCheckBox("Use Q", "qUse");
   //         LasthitMenu.CreateCheckBox("Use E", "eUse");
   //         LasthitMenu.AddGroupLabel("Settings");
   //         LasthitMenu.CreateSlider("Mana must be higher than [{0}%] to use Lasthit spells", "manaSlider", 30);

   //         JungleClearMenu.AddGroupLabel("JungleClear");
   //         JungleClearMenu.CreateCheckBox("Use Q", "qUse");
   //         JungleClearMenu.CreateCheckBox("Use E", "eUse");
   //         JungleClearMenu.AddGroupLabel("Settings");
   //         JungleClearMenu.CreateSlider("Mana must be higher than [{0}%] to use JungleClear spells", "manaSlider", 30);

   //         KillStealMenu.AddGroupLabel("KillSteal");
   //         KillStealMenu.CreateCheckBox("Use Q", "qUse");
   //         KillStealMenu.CreateCheckBox("Use E", "eUse");
   //         KillStealMenu.CreateCheckBox("Use R", "rUse");
   //         KillStealMenu.AddGroupLabel("Settings");
   //         KillStealMenu.CreateSlider("Mana must be higher than [{0}%] to use KS spells", "manaSlider", 30);

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.AddLabel("Tracker Draws");
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
            DrawingsMenu.Add("harass", new CheckBox("Harass", false));
            DrawingsMenu.Add("laneclear", new CheckBox("LaneClear", false));
            DrawingsMenu.Add("lasthit", new CheckBox("LastHit", false));
            DrawingsMenu.Add("flee", new CheckBox("Flee", false));

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", false));
            MiscMenu.AddSeparator(15);
            MiscMenu.CreateCheckBox("Activate Skin hack", "skinhax", false);
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Rumble in the Jungle", "Bilgerat Rumble", "Super Galaxy Rumble"));

        }
    }
}
