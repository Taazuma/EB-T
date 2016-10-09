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
using static RoninTune.Menus;
using static RoninTune.SpellsManager;
using System.Drawing;
using Color = SharpDX.Color;
using Font = System.Drawing.Font;
using FontColor = System.Drawing.Color;

namespace RoninTune
{
    public static class Events
    {
        private static Text Text;

        static Events()
        {
            Text = new Text("", new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)) { Color = System.Drawing.Color.White };
            Drawing.OnDraw += OnDraw;
            Orbwalker.OnAttack += OrbwalkerOnOnAttack;
        }

        private static void OrbwalkerOnOnAttack(AttackableUnit target, EventArgs args)
        {
            if (MiscMenu.GetCheckBoxValue("fjgl") && target is AIHeroClient && SpellsManager.HasChallengingSmite() && 
                SpellsManager.Smite.IsReady())
            {
                var enemyHero = (AIHeroClient) target;
                SpellsManager.Smite.Cast(enemyHero);
            }
        }


        public static void Initialize()
        {
        }

        private static void OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
            {
                return;
            }
            

        }

    }
}
