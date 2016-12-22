using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Eclipse
{
    internal class Menus
    {
        public static Menu FirstMenu;
        public static Menu AniMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu AutoHarassMenu;
        public static Menu LaneClearMenu;
        public static Menu LasthitMenu;
        public static Menu JungleClearMenu;
        public static Menu KillStealMenu;
        public static Menu DrawingsMenu;
        public static Menu MiscMenu;
        public static Menu FleeMenu;

        public const string AniMenuID = "animenuid";
        public const string ComboMenuID = "combomenuid";
        public const string LaneClearMenuID = "laneclearmenuid";
        public const string LastHitMenuID = "lasthitmenuid";
        public const string JungleClearMenuID = "jungleclearmenuid";
        public const string KillStealMenuID = "killstealmenuid";
        public const string DrawingsMenuID = "drawingsmenuid";
        public const string MiscMenuID = "miscmenuid";
        public const string FleeMenuID = "fleemenuid";

        public static void CreateMenu()
        {
            FirstMenu = MainMenu.AddMenu("Sharp "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
            FirstMenu.AddLabel("About");
            FirstMenu.AddLabel("Version: 6.24");
            FirstMenu.AddLabel("Sharped by DaisyTaazu");
            FirstMenu.AddLabel("This Addon is based on Nechrito Riven Kappa ^)");
            AniMenu = FirstMenu.AddSubMenu("Animation", AniMenuID);
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);
            FleeMenu = FirstMenu.AddSubMenu("Flee", FleeMenuID);

            AniMenu.AddGroupLabel("Animation Settings");
            AniMenu.AddSeparator(7);
            AniMenu.Add("CancelPing", new CheckBox("Include Ping", true));
            AniMenu.Add("EmoteList", new ComboBox("Emotes", new[] { "Laugh", "Taunt", "Joke", "Dance", "None" }, 3));

            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.AddSeparator(7);
            ComboMenu.Add("Q3Wall", new CheckBox("Q3 Over Wall", true));
            ComboMenu.Add("FlashOften", new CheckBox("Flash Burst Frequently", true));
            ComboMenu.Add("OverKillCheck", new CheckBox("R2 Max Damage", true));
            ComboMenu.Add("Doublecast", new CheckBox("Fast Combo, less dmg", true));
            ComboMenu.Add("AlwaysR", new KeyBind("Use R (Toggle)", true, KeyBind.BindTypes.PressToggle, 'G'));
            ComboMenu.Add("AlwaysF", new KeyBind("Use Flash (Toggle)", true, KeyBind.BindTypes.PressToggle, 'L'));
            ComboMenu.Add("BurstEnabled", new KeyBind("Enable Burst Combo (Toggle)", false, KeyBind.BindTypes.PressToggle, 'H'));

            LaneClearMenu.AddGroupLabel("LaneClear Settings");
            LaneClearMenu.AddSeparator(7);
            LaneClearMenu.Add("LaneEnemy", new CheckBox("Stop If Nearby Enemy", true));
            LaneClearMenu.Add("laneQFast", new CheckBox("Fast Clear", true));
            LaneClearMenu.Add("LaneQ", new CheckBox("Use Q", true));
            LaneClearMenu.Add("LaneW", new CheckBox("Use W", true));
            LaneClearMenu.Add("LaneE", new CheckBox("Use E", true));

            JungleClearMenu.AddGroupLabel("JungleClear Settings");
            JungleClearMenu.AddSeparator(7);
            JungleClearMenu.Add("JungleQ", new CheckBox("Use Q", true));
            JungleClearMenu.Add("JungleW", new CheckBox("Use W", true));
            JungleClearMenu.Add("JungleE", new CheckBox("Use E", true));


            KillStealMenu.AddGroupLabel("KillSteal Settings");
            KillStealMenu.AddSeparator(7);
            KillStealMenu.Add("ignite", new CheckBox("Use Ignite", true));
            KillStealMenu.Add("ksW", new CheckBox("Use W", true));
            KillStealMenu.Add("ksR2", new CheckBox("Use R2", true));
            KillStealMenu.Add("ksQ", new CheckBox("Use Q", true));

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.AddSeparator(7);
            MiscMenu.Add("GapcloserMenu", new CheckBox("Anti-Gapcloser", true));
            MiscMenu.Add("InterruptMenu", new CheckBox("Interrupter", true));
            MiscMenu.Add("KeepQ", new CheckBox("Keep Q Alive", true));
            MiscMenu.Add("QMove", new KeyBind("Q Move to mouse", false, KeyBind.BindTypes.HoldActive, 'K'));


            DrawingsMenu.AddGroupLabel("Draw");
            DrawingsMenu.AddSeparator(7);
            DrawingsMenu.Add("FleeSpot", new CheckBox("Draw Flee Spots", true));
            DrawingsMenu.Add("Dind", new CheckBox("Damage Indicator", true));
            DrawingsMenu.Add("DrawForceFlash", new CheckBox("Flash Status", true));
            DrawingsMenu.Add("DrawAlwaysR", new CheckBox("R Status", true));
            DrawingsMenu.Add("DrawBurst", new CheckBox("Burst Status", true));
            DrawingsMenu.Add("DrawCB", new CheckBox("Combo Engage", true));
            DrawingsMenu.Add("DrawBT", new CheckBox("BurstMode Engage", false));
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

            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("WallFlee", new CheckBox("WallJump in Flee", true));
            FleeMenu.Add("FleeYoumuu", new CheckBox("Use Youmuu's Ghostblade", true));


        }
    }
}
