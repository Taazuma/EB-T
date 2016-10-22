using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Eclipse
{
    class Igniter
    {   

//Credits T7

        public static AIHeroClient myhera { get { return ObjectManager.Player; } }
        public static Spell.Targeted ignt = new Spell.Targeted(myhera.GetSpellSlotFromName("summonerdot"), 600);
        private static Menu menu;

        public static void OnUpdate(EventArgs args)
        {
            var targeter = TargetSelector.GetTarget(1000, DamageType.Physical, Player.Instance.Position);
            float HP5 = targeter.FlatHPRegenMod * 5, IgniteDMG = 50 + (20 * myhera.Level);

            if (ignt.Slot != SpellSlot.Unknown && ignt.IsReady() && myhera.GetSummonerSpellDamage(targeter, DamageLibrary.SummonerSpells.Ignite) > targeter.Health && ignt.IsInRange(targeter) && menu["active"].Cast<CheckBox>().CurrentValue)
            {
                if (IgniteDMG > (targeter.Health + HP5)) ignt.Cast(targeter);
            }

        }
        public static void OnDraw(EventArgs args)
        {
            if (menu["draw"].Cast<CheckBox>().CurrentValue && ignt.IsReady())
                Circle.Draw(SharpDX.Color.Red, ignt.Range, myhera.Position);
        }

        public static bool HasIgnite()
        {
            return ignt != null;
        }

        public static void Menu()
        {
            menu = MainMenu.AddMenu("Ignite Helper", "ignitemenu");
            menu.Add("active", new CheckBox("Use Ignite", true));
            menu.Add("draw", new CheckBox("Draw ignite Range", false));       
        }

    }
}
