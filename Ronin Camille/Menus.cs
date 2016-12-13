using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.IO;
using System.Media;
using System.Net;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using static Eclipse.SpellsManager;
using Eclipse_Template.Properties;

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
            FirstMenu = MainMenu.AddMenu("Ronin "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            FirstMenu.AddLabel("Cast Flee Key (T) do your E -> Press Spacebar");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            //HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.CreateCheckBox("Use E", "eUse");
            ComboMenu.AddSeparator(5);
            ComboMenu.CreateCheckBox("Use W", "wUse");
			ComboMenu.CreateCheckBox("Use R", "rUse");
            ComboMenu.AddSeparator(5);
            ComboMenu.AddLabel("Use ultimate on");
            foreach (var enemies in EntityManager.Heroes.Enemies.Where(i => !i.IsMe))
            {
                ComboMenu.Add("r.ult" + enemies.ChampionName, new CheckBox("" + enemies.ChampionName, false));
            }
            ComboMenu.AddSeparator(12);
            ComboMenu.AddLabel("E Settings");
            ComboMenu.Add("EC", new ComboBox(" E Position ", 0, "Only Mouse"));
            ComboMenu.AddLabel("Cast Flee Key (T) -> then after E (CurSorPos) -> Press Spacebar");
            ComboMenu.AddSeparator(12);
            ComboMenu.AddLabel("Humanizer");
            ComboMenu.Add("Qdelay", new Slider("Q Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Wdelay", new Slider("W Delay (ms)", 0, 0, 300));
            ComboMenu.Add("Edelay", new Slider("E Delay (ms)", 100, 0, 300));
            ComboMenu.Add("Rdelay", new Slider("R Delay (ms)", 0, 0, 300));

            AutoHarassMenu.AddGroupLabel("AutoHarass");
            AutoHarassMenu.CreateCheckBox("Use W", "wUse");
            AutoHarassMenu.AddGroupLabel("Settings");
            AutoHarassMenu.CreateKeyBind("Enable/Disable AutoHarass", "autoHarassKey", 'Z', 'U');
            AutoHarassMenu.CreateSlider("Mana must be higher than [{0}%] to use AutoHarass spells", "manaSlider", 80);

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.CreateCheckBox("Use W", "wUse");
            LaneClearMenu.AddGroupLabel("Settings");
            LaneClearMenu.Add("lc.MinionsW", new Slider("Min. Minions W ", 3, 0, 10));
            LaneClearMenu.CreateSlider("Mana must be higher than [{0}%] to use LaneClear spells", "manaSlider", 70);

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.CreateCheckBox("Use Q", "qUse");
            LasthitMenu.AddGroupLabel("Settings");
            LasthitMenu.CreateSlider("Mana must be higher than [{0}%] to use Lasthit spells", "manaSlider", 30);

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "WUse");
            JungleClearMenu.CreateCheckBox("Use E", "eUse", false);
            JungleClearMenu.AddGroupLabel("Settings");
            JungleClearMenu.CreateSlider("Mana must be higher than [{0}%] to use JungleClear spells", "manaSlider", 15);

            KillStealMenu.AddGroupLabel("KillSteal");
            KillStealMenu.CreateCheckBox("Use W", "wUse", false);
            KillStealMenu.AddGroupLabel("Settings");
            KillStealMenu.CreateSlider("Mana must be higher than [{0}%] to use KS spells", "manaSlider", 25);
            KillStealMenu.AddLabel("Disable this if you get Problems ! May dont work at the moment");

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
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 0, "Default", "Program Camille"));

        }
        public static int Qdelay { get { return ComboMenu["Qdelay"].Cast<Slider>().CurrentValue; } }
        public static int Wdelay { get { return ComboMenu["Wdelay"].Cast<Slider>().CurrentValue; } }
        public static int Edelay { get { return ComboMenu["Edelay"].Cast<Slider>().CurrentValue; } }
        public static int Rdelay { get { return ComboMenu["Rdelay"].Cast<Slider>().CurrentValue; } }
    }
}
