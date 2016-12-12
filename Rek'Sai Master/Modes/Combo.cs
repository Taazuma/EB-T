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
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var Target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                    if (Target == null || Target.IsInvulnerable || Target.MagicImmune)
                    {
                           return;
                    }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region C0
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
            {
                if (Program.burrowed)
                {
                    if (ComboMenu.GetCheckBoxValue("UseEBCombo"))
                    {
                        var te = TargetSelector.GetTarget(Q.Range + W.Range, DamageType.Physical);
                        if (E2.IsReady() && te.IsValidTarget(E2.Range + W.Range) && Program._player.Distance(te) > Q.Range)
                        {
                            var predictionE2 = E2.GetPrediction(te);
                            if (predictionE2.HitChance >= HitChance.High) E2.Cast(predictionE2.CastPosition - 50);
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseQBCombo"))
                    {
                        var tbq = TargetSelector.GetTarget(Q.Range + W.Range, DamageType.Physical);
                        var predictionQ2 = Q2.GetPrediction(tbq);
                        if (Q2.IsReady() && tbq.IsValidTarget(Q2.Range) && predictionQ2.HitChance >= HitChance.High) Q2.Cast(predictionQ2.CastPosition);
                    }

                        if (ComboMenu.GetCheckBoxValue("UseWCombo"))
                    {
                        var tw = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                        if (W.IsReady() && tw.IsValidTarget(W.Range) && !Q2.IsReady())
                        {
                            W.Cast();
                        }
                    }
                }

                if (!Program.burrowed)
                {
                    if (ComboMenu.GetCheckBoxValue("UseQCombo"))
                    {
                        var tq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                        if (Q.IsReady() && tq.IsValidTarget(Q.Range)) Q.Cast();
                    }

                    if (ComboMenu.GetCheckBoxValue("UseECombo"))
                    {
                        var te = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                        if (te.IsValidTarget(E.Range) && E.IsReady())
                        {
                            if (Q.IsOnCooldown)
                            {
                                E.Cast(te);
                            }
                            else if (_player.Mana < 100 && te.Health <= te.GetDamage(SpellSlot.E))
                            {
                                E.Cast(te);
                            }
                            else if (te.Health <= SpellsManager.GetTotalDamage(te))
                            {
                                E.Cast(te);
                            }
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseWCombo")&& W.IsReady())
                    {
                        var tw = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                        if (!Q.IsReady() && !tw.IsValidTarget(E.Range) && tw.IsValidTarget(Q2.Range)) W.Cast();
                    }
                }
            }
        
            #endregion C0

            #region C1
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {
                ComboMenu.Add("comboE2Distance", new Slider("Use E (Burrowed) when 0 enemies in range:", 550, 100, 750));

                if (Player.Instance.CountEnemiesInRange(800) > 0)
                {

                    Program.Yomuus();
                }

                if (Program.burrowed)
                {
                    var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
                    var targetQ2 = TargetSelector.GetTarget(850, DamageType.Magical);
                    var targetE = TargetSelector.GetTarget(550, DamageType.Physical);
                    var targetE2 = TargetSelector.GetTarget(E2.Range, DamageType.Physical);
                    var predE2 = E2.GetPrediction(targetE2);

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {
                        if (targetW != null && targetW.IsValidTarget(W.Range))
                        {
                            W.Cast();
                            return;
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseQBCombo") && Q2.IsReady())
                    {

                        if (targetQ2 != null && targetQ2.IsValidTarget(Q2.Range))
                        {
                            var predictionQ2 = Q2.GetPrediction(targetQ2);
                            if (predictionQ2.HitChance >= HitChance.Medium)
                            {
                                Q2.Cast(predictionQ2.CastPosition);
                                return;
                            }
                        }

                    }

                    if (ComboMenu.GetCheckBoxValue("UseEBCombo") && E2.IsReady())
                    {

                        if (Player.Instance.CountEnemiesInRange(ComboMenu.GetSliderValue("comboE2Distance")) < 1)
                        {

                            if (targetE2.IsValidTarget())
                            {
                                E2.Cast(targetE2.Position + 200);
                                return;

                            }
                        }
                    }
                }

                if (!Program.burrowed)
                {
                    var targetE = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                    var lastTarget = Orbwalker.LastTarget;
                    var target = TargetSelector.GetTarget(300, DamageType.Physical);

                    if (ComboMenu.GetCheckBoxValue("UseECombo") && E.IsReady())
                    {

                        if (targetE != null && targetE.IsValidTarget())
                        {
                            E.Cast(targetE);
                            return;
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {
                        if (Player.Instance.CountEnemiesInRange(300) == 0)
                        {
                            W.Cast();
                            return;
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {
                        if (lastTarget.IsValidTarget(Player.Instance.BoundingRadius + 250) && !target.HasBuff("reksaiknockupimmune"))
                        {
                            W.Cast();
                            return;
                        }
                    }
                }
            }
            #endregion C1               

        

            }
    }
    }
