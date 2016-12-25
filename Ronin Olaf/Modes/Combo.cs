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

namespace Eclipse.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
            //////////////////////////////////////////////////////////////////////////////////
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Mixed);
            var target = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);
            var useQ = ComboMenu.GetCheckBoxValue("qUse");
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            //////////////////////////////////////////////////////////////////////////////////
            // Lets Beginn this here

            #region 1vs1
            //Begin
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
            {
                if (target == null || target.IsInvulnerable || target.MagicImmune)
                {
                    return;
                }
                var pos = Q.GetPrediction(target).CastPosition.Extend(Player.Instance.Position, -80);
                if (useQ && Q.IsReady() && target.IsValidTarget(Q.Range))
                {
                    Core.DelayAction(delegate
                    {
                        if (Program._player.Distance(target) > 375)
                        {
                            Q.Cast(pos.To3DWorld());
                        }
                        else
                        {
                            Q.Cast(target.Position);
                        }
                    }, Qdelay);
                }
            }

            Core.DelayAction(delegate
            {
                if (useW && W.IsReady() && target.IsValidTarget(E.Range))
            {
                W.Cast();
            }
            }, Wdelay);

            Core.DelayAction(delegate
            {
                if (useE && E.IsReady() && target.IsValidTarget(E.Range) && Program._player.Distance(etarget) > Player.Instance.GetAutoAttackRange(etarget))
            {
                E.Cast(etarget);
            }
            }, Edelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && target.IsValidTarget(R.Range) && Program._player.HealthPercent <= 20)
            {
                R.Cast();
            }
            }, Rdelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Taunt) || Player.HasBuffOfType(BuffType.Knockup))
            {
                    R.Cast();
            }
            }, Rdelay);

            //End
            #endregion 1vs1

            #region Gank
            // Beginn
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {

                if (Ghost != null && Ghost.IsReady())
                {
                    Ghost.Cast();
                }

                if (target == null || target.IsInvulnerable || target.MagicImmune)
                {
                    return;
                }

                if (Smite != null && Smite.IsReady() && target.IsValidTarget(Smite.Range))
                {
                    Smite.Cast(target);
                }

                if (Ignite != null && Ignite.IsReady() && target.IsValidTarget(Ignite.Range))
                {
                    Ignite.Cast(target);
                }

                Eclipse.Activator.Items.Youmuus.Cast();

                Core.DelayAction(delegate
                {
                    if (useW && W.IsReady() && target.IsValidTarget(E.Range))
                    {
                        W.Cast();
                    }
                }, Wdelay);

                var pos = Q.GetPrediction(target).CastPosition.Extend(Player.Instance.Position, -80);
                if (useQ && Q.IsReady() && target.IsValidTarget(Q.Range))
                {
                    Core.DelayAction(delegate
                    {
                        if (Program._player.Distance(target) > 375)
                        {
                            Q.Cast(pos.To3DWorld());
                        }
                        else
                        {
                            Q.Cast(target.Position);
                        }
                    }, Qdelay);
                }
            }

            Core.DelayAction(delegate
            {
                if (useE && E.IsReady() && target.IsValidTarget(E.Range) && Program._player.Distance(etarget) > Player.Instance.GetAutoAttackRange(etarget))
                {
                    E.Cast(etarget);
                }
            }, Edelay);

            Eclipse.Activator.Items.CastItems(target);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && target.IsValidTarget(R.Range) && Program._player.HealthPercent <= 20)
                {
                    R.Cast();
                }
            }, Rdelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Taunt) || Player.HasBuffOfType(BuffType.Knockup))
                {
                    R.Cast();
                }
            }, Rdelay);


            // End
            #endregion Gank

            #region Team
            //Beginn
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 2)
            {
                if (target == null || target.IsInvulnerable || target.MagicImmune)
                {
                    return;
                }
                var pos = Q.GetPrediction(target).CastPosition.Extend(Player.Instance.Position, -80);
                if (useQ && Q.IsReady() && target.IsValidTarget(Q.Range))
                {
                    Core.DelayAction(delegate
                    {
                        if (Program._player.Distance(target) > 375)
                        {
                            Q.Cast(pos.To3DWorld());
                        }
                        else
                        {
                            Q.Cast(target.Position);
                        }
                    }, Qdelay);
                }
            }

            Core.DelayAction(delegate
            {
                if (useW && W.IsReady() && target.IsValidTarget(E.Range))
                {
                    W.Cast();
                }
            }, Wdelay);

            Core.DelayAction(delegate
            {
                if (useE && E.IsReady() && target.IsValidTarget(E.Range) && Program._player.Distance(etarget) > Player.Instance.GetAutoAttackRange(etarget))
                {
                    E.Cast(etarget);
                }
            }, Edelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && Player.Instance.CountEnemiesInRange(R.Range) >= 4 && MiscMenu.GetCheckBoxValue("rtUse"))
                {
                    R.Cast();
                }
            }, Rdelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && target.IsValidTarget(R.Range) && Program._player.HealthPercent <= 20)
                {
                    R.Cast();
                }
            }, Rdelay);

            Core.DelayAction(delegate
            {
                if (useR && R.IsReady() && Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Taunt) || Player.HasBuffOfType(BuffType.Knockup))
                {
                    R.Cast();
                }
            }, Rdelay);


            //End
            #endregion Team

            // The End


        }
    }
}