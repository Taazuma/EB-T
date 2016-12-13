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
using SharpDX;
using Eclipse;
using static Eclipse.Menus;

namespace Eclipse.Modes
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;

            Active.Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass) && playerMana > FirstMenu.GetSliderValue("manaSlider"))
            {
                Harass.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Flee.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && playerMana > FirstMenu.GetSliderValue("manaSlider"))
            {
                LaneClear.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                JungleClear.Execute();
            }

            if (Program.check(MiscMenu, "skinhax")) Program._player.SetSkinId((int)MiscMenu["skinID"].Cast<ComboBox>().CurrentValue);
            if (Program.check(MiscMenu, "lvlup")) Program.LevelUpSpells();

        }
    }
}
