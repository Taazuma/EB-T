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
using static RoninTune.SpellsManager;
using static RoninTune.Menus;
using System.Diagnostics;

namespace RoninTune.Modes
{
    internal class Active
    {
        public static readonly AIHeroClient Player = ObjectManager.Player;

        public static void Execute()
        {

            if (DrawingsMenu.GetCheckBoxValue("showkilla"))
                Indicator.DamageToUnit = Program.GetComboDamage;

            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range - 200, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

                    var predQ = Q.GetPrediction(target);
                    var qDamage = target.GetDamage(SpellSlot.Q);

                    if (KillStealMenu.GetCheckBoxValue("rUse") && R.IsReady() && rtarget.IsValidTarget(R.Range - 500))
                    {
                        R.Cast();
                        R1.Cast(rtarget);
                    }

                    if (KillStealMenu.GetCheckBoxValue("qUse") && Q.IsReady() && target.Health + target.AttackShield <= qDamage)
                    
                    {
                        if (predQ.HitChance >= HitChance.High)
                        {
                            Q.Cast(target.Position);
                        }
                    }

                    if (KillStealMenu.GetCheckBoxValue("eUse") && E.IsReady() && target.Health + target.AttackShield < Player.GetSpellDamage(target, SpellSlot.E))
                    {
                        E.Cast(target);
                    }

                }
            }

        }

