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

        public static AIHeroClient myhero { get { return ObjectManager.Player; } }
        public static Spell.Targeted ignt = new Spell.Targeted(myhero.GetSpellSlotFromName("summonerdot"), 600);
        private static Menu menu;

        public static void OnUpdate(EventArgs args)
        {
            var target = TargetSelector.GetTarget(1000, DamageType.Physical, Player.Instance.Position);
            float HP5 = target.FlatHPRegenMod * 5, IgniteDMG = 50 + (20 * myhero.Level);
            if (ignt.Slot != SpellSlot.Unknown && ignt.IsReady() && myhero.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) > target.Health && ignt.IsInRange(target) && menu["active"].Cast<CheckBox>().CurrentValue)
            {
                if (IgniteDMG > (target.Health + HP5)) ignt.Cast(target);
            }
        }
        public static void OnDraw(EventArgs args)
        {
            if (menu["draw"].Cast<CheckBox>().CurrentValue && ignt.IsReady())
                Circle.Draw(SharpDX.Color.Red, ignt.Range, myhero.Position);
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
