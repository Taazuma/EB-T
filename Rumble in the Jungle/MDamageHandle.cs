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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using System.Diagnostics;
//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario//Credits Mario

namespace Eclipse.DMGHandler
{
    public static class DamageHandler
    {
        public static bool ReceivingAA;
        public static bool ReceivingSkillShot;
        public static bool ReceivingSpell;
        public static bool ReceivingDangSpell;

        public static void Initialize()
        {
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }
        
        private static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(!MiscMenu.GetCheckBoxValue("consideraas"))return;
            var hero = sender as AIHeroClient;
            if (hero == null) return;
            if(hero.IsAlly) return;

            if (args.Target.IsMe)
            {
                ReceivingAA = true;
                Core.DelayAction(() => ReceivingAA = false, 80);
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var hero = sender as AIHeroClient;
            if (hero == null || hero.IsAlly) return;

            var dangerousspell =
                DangerousSpells.Spells.FirstOrDefault(
                    x =>
                        x.Hero == hero.Hero && args.Slot == x.Slot &&
                        MiscMenu[x.Hero.ToString() + x.Slot].Cast<CheckBox>().CurrentValue);
            //SkilShot
            if (args.Target == null)
            {
                if (!MiscMenu.GetCheckBoxValue("considerskilshots")) return;
                var projection = Player.Instance.Position.To2D().ProjectOn(args.Start.To2D(), args.End.To2D());

                if (projection.IsOnSegment &&
                    projection.SegmentPoint.Distance(Player.Instance.Position) <=
                    args.SData.CastRadius + Player.Instance.BoundingRadius + 30)
                {
                    if (dangerousspell != null)
                    {
                        ReceivingDangSpell = true;
                        Core.DelayAction(() => ReceivingDangSpell = false, 80);
                        return;
                    }

                    ReceivingSkillShot = true;
                    Core.DelayAction(() => ReceivingSkillShot = false, 80);
                }
            }
            //Targetted spell
            else
            {
                if (!MiscMenu.GetCheckBoxValue("considerspells")) return;
                if (args.Target.IsMe)
                {
                    ReceivingSpell = true;
                    Core.DelayAction(() => ReceivingSpell = false, 80);
                }

                if (dangerousspell != null && ReceivingSpell)
                {
                    ReceivingDangSpell = true;
                    Core.DelayAction(() => ReceivingDangSpell = false, 80);
                }
            }
        }

        #region Extensions

        public static bool InDanger(this AIHeroClient hero, int HealthPercent)
        {
            if (ReceivingDangSpell)
            {
                return true;
            }
            if (ReceivingSpell || ReceivingAA || ReceivingSkillShot)
            {
                if (Player.Instance.HealthPercent < HealthPercent)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Extensions
    }
}
