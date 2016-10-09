using System;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace RoninSkarner
{
    public static class Config
    {
        private const string MenuName = "Smite";

        private static readonly Menu smiterMenu;
        private static readonly CheckBox _smiteEnabled;
        private static readonly KeyBind _smiteEnabledToggle;
        private static readonly CheckBox _smiteEnemies;
        private static readonly CheckBox _smiteEnemiesChallenging;
        private static readonly CheckBox _drawSmiteStatus;
        private static readonly CheckBox _drawSmiteable;
        private static readonly CheckBox _drawRange;
        private static readonly Slider _keepSmiteNumber;

        public static Menu MainMenu
        {
            get { return smiterMenu; }
        }

        public static bool SmiteEnabled
        {
            get { return _smiteEnabled.CurrentValue; }
        }

        public static bool SmiteEnabledToggle
        {
            get { return _smiteEnabledToggle.CurrentValue; }
        }

        public static bool SmiteEnemies
        {
            get { return _smiteEnemies.CurrentValue; }
        }

        public static bool SmiteEnemiesChallenging
        {
            get { return _smiteEnemiesChallenging.CurrentValue; }
        }

        public static bool DrawSmiteStatus
        {
            get { return _drawSmiteStatus.CurrentValue; }
        }

        public static bool DrawSmiteable
        {
            get { return _drawSmiteable.CurrentValue; }
        }

        public static bool DrawSmiteRange
        {
            get { return _drawRange.CurrentValue; }
        }

        public static int KeepSmiteNumber
        {
            get { return _keepSmiteNumber.CurrentValue; }
        }

        static Config()
        {
            smiterMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu(MenuName, MenuName.ToLower());
            smiterMenu.AddGroupLabel("Welcome to Smite");
            smiterMenu.AddLabel("Created by Haker");
            smiterMenu.AddLabel("Feel free to send me any suggestions you might have.");
            smiterMenu.AddGroupLabel("Smite Status");
            _smiteEnabled = smiterMenu.Add("vSmiteEnabled", new CheckBox("Enabled always"));
            _smiteEnabledToggle = smiterMenu.Add("vSmiteEnabledToggle", new KeyBind("Enabled (Toggle Key)", false, KeyBind.BindTypes.PressToggle, 'K'));
            _smiteEnemies = smiterMenu.Add("vSmiteEnemies", new CheckBox("KS with Smite"));
            _smiteEnemiesChallenging = smiterMenu.Add("vSmiteEnemiesChallenging", new CheckBox("Smite enemies with Challenging Smite on AA"));
            _keepSmiteNumber = smiterMenu.Add("vSmiteKeepSmiteNumber",
                new Slider("Smite enemies only if you have more than {0} charges", 1, 0, 2));
            smiterMenu.AddGroupLabel("Monsters to smite");
            smiterMenu.AddLabel("Select monsters you want to smite");
            smiterMenu.Add("vSmiteSRU_Baron", new CheckBox("Baron Nashor"));
            smiterMenu.Add("vSmiteSRU_Dragon_Elder", new CheckBox("Elder Dragon"));
            smiterMenu.Add("vSmiteSRU_Dragon_Air", new CheckBox("Air Dragon"));
            smiterMenu.Add("vSmiteSRU_Dragon_Earth", new CheckBox("Fire Dragon"));
            smiterMenu.Add("vSmiteSRU_Dragon_Fire", new CheckBox("Earth Dragon"));
            smiterMenu.Add("vSmiteSRU_Dragon_Water", new CheckBox("Water Dragon"));
            smiterMenu.Add("vSmiteSRU_Red", new CheckBox("Red"));
            smiterMenu.Add("vSmiteSRU_Blue", new CheckBox("Blue"));
            smiterMenu.Add("vSmiteSRU_Gromp", new CheckBox("Gromp"));
            smiterMenu.Add("vSmiteSRU_Murkwolf", new CheckBox("Murkwolf"));
            smiterMenu.Add("vSmiteSRU_Krug", new CheckBox("Krug"));
            smiterMenu.Add("vSmiteSRU_Razorbeak", new CheckBox("Razorbeak"));
            smiterMenu.Add("vSmiteSru_Crab", new CheckBox("Crab"));
            smiterMenu.Add("vSmiteSRU_RiftHerald", new CheckBox("Rift Herald"));
            smiterMenu.AddGroupLabel("Drawing");
            _drawSmiteStatus = smiterMenu.Add("vSmiteDrawSmiteStatus", new CheckBox("Draw Smite Status"));
            _drawSmiteable = smiterMenu.Add("vSmiteDrawSmiteable", new CheckBox("Draw Smiteable Monsters"));
            _drawRange = smiterMenu.Add("vSmiteDrawRange", new CheckBox("Draw Smite Range"));
            DebugMenu.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class DebugMenu
        {
            private static readonly Menu MenuDebug;
            private static readonly CheckBox _debugChat;
            private static readonly CheckBox _debugConsole;

            public static bool DebugChat
            {
                get { return _debugChat.CurrentValue; }
            }
            public static bool DebugConsole
            {
                get { return _debugConsole.CurrentValue; }
            }

            static DebugMenu()
            {
                MenuDebug = Config.smiterMenu.AddSubMenu("Debug");
                MenuDebug.AddLabel("This is for debugging purposes only.");
                _debugChat = MenuDebug.Add("debugChat", new CheckBox("Show debug messages in chat", false));
                _debugConsole = MenuDebug.Add("debugConsole", new CheckBox("Show debug messages in console", false));
            }

            public static void Initialize()
            {

            }
        }
    }
}
