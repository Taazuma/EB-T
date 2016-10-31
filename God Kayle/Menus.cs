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
            FirstMenu = MainMenu.AddMenu("God "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
			FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("Just press Space to win");
            FirstMenu.AddLabel("The Addon will do his Job");
            FirstMenu.CreateSlider("Mana Manager", "manaSlider", 50);
            FirstMenu.Add("lc.MinionsE", new Slider("Min. Minions for E ", 2, 0, 10));
            FirstMenu.AddLabel("Safer Option");
            FirstMenu.AddSeparator(5);
            FirstMenu.CreateCheckBox("Heal/R Safe Allies?", "Saferali", false);
            FirstMenu.CreateCheckBox("Heal/R Safe Me?", "Saferme", true);
            //ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            //HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            //LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            //LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            //JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            //KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);

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
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.CreateCheckBox("Show if Enemy Killable", "showkilla");
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw", false);
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw", false);
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw", false);
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw", false);
            DrawingsMenu.AddSeparator(15);
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
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddLabel("Level Up Function");
            MiscMenu.Add("lvlup", new CheckBox("Auto Level Up Spells", true));
            MiscMenu.AddSeparator(15);
            MiscMenu.CreateCheckBox("Activate Skin hack", "skinhax", false);
            MiscMenu.Add("skinID", new ComboBox("Skin Hack", 1, "Default", "Silver Kayle", "Unmasked Kayle", "Battleborn Kayle", "Judgement Kayle", "Aether Wing Kayle", "Riot Kayle", "Iron Inquisitor Kayle"));

        }
        public static int skinId()
        {
            return MiscMenu["skin.Id"].Cast<Slider>().CurrentValue;
        }
    }
}
