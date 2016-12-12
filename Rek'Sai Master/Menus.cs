using System.Drawing;
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
            FirstMenu = MainMenu.AddMenu("Master "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "master");
            FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("Status: 6.24");
            ComboMenu = FirstMenu.AddSubMenu("• Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("• Harass", HarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("• LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("• JungleClear", JungleClearMenuID);
            //KillStealMenu = FirstMenu.AddSubMenu("• KillSteal", KillStealMenuID);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.AddLabel("Logics");
            ComboMenu.AddSeparator(4);
            ComboMenu.Add("Comba", new ComboBox(" Combo Logic ", 1, "Normal", "Ninja´s", "HeavenStrike", "wFights"));
            ComboMenu.AddSeparator(4);
            ComboMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            ComboMenu.AddSeparator(7);
            ComboMenu.Add("UseQCombo", new CheckBox("Use Q Unburrowed"));
            ComboMenu.Add("UseQBCombo", new CheckBox("Use Q"));
            ComboMenu.AddSeparator(7);
            ComboMenu.Add("UseWCombo", new CheckBox("Use W"));
            ComboMenu.AddSeparator(7);
            ComboMenu.Add("UseECombo", new CheckBox("Use E Unburrowed"));
            ComboMenu.Add("UseEBCombo", new CheckBox("Use E burrowed"));
            ComboMenu.AddSeparator(7);
            ComboMenu.AddLabel("Pro Tips");
            ComboMenu.AddLabel("If you get problems with Q, turn on Ninja Q on MiscMenu");

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use Q", "qUse", false);
            HarassMenu.CreateCheckBox("Use Q2", "q2Use");
            HarassMenu.CreateCheckBox("Use E", "eUse", false);

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.CreateCheckBox("Use E", "eUse");

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.CreateCheckBox("Use Q", "qUse");

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use Q2", "q2Use");
            JungleClearMenu.CreateCheckBox("Use E", "eUse");

            //KillStealMenu.AddGroupLabel("KillSteal");
            //KillStealMenu.CreateCheckBox("Use Q2", "qUse");
            //KillStealMenu.CreateCheckBox("Use E2", "eUse");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.Add("AutoW", new CheckBox("Auto W"));
            MiscMenu.Add("AutoWHP", new Slider("Use W if HP is <= ", 25, 1));
            MiscMenu.Add("AutoWMP", new Slider("Use W if Fury is >= ", 100, 1));
            MiscMenu.Add("Inter_W", new CheckBox("Use W to Interrupter"));
            MiscMenu.Add("turnburrowed", new CheckBox("Turn Burrowed if do nothing"));
            MiscMenu.CreateCheckBox("Ninja Q Logic", "ninjaq", false);
            MiscMenu.AddSeparator(12);
            MiscMenu.CreateCheckBox("Activate Skin hack", "skinhax", false);
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 0, "Default", "Eternum Rek'Sai", "Pool Party Rek'Sai"));
            MiscMenu.AddSeparator(12);
            MiscMenu.Add("escapeterino", new KeyBind("Escape|WallJump", false, KeyBind.BindTypes.HoldActive, 'T'));
            MiscMenu.AddSeparator(12);


            DrawingsMenu.AddGroupLabel("Settings");
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
            DrawingsMenu.AddLabel("Pro Tips");
            DrawingsMenu.AddLabel("Disable Drawings if you get FPS Drops or Crash/Black Screen");
        }
    }
}
