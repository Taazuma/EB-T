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
// Thanks to the Ninja
namespace Eclipse.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
                    var targetQ2 = TargetSelector.GetTarget(850, DamageType.Magical);
                    var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                    var te = TargetSelector.GetTarget(E.Range + W.Range, DamageType.Magical);
                    var targetE = TargetSelector.GetTarget(550, DamageType.Physical);
                    var targetE2 = TargetSelector.GetTarget(E2.Range, DamageType.Physical);
                    var predE2 = E2.GetPrediction(targetE2);
                    var predq2 = Q2.GetPrediction(targetQ2).HitChance >= Hitch.hitchance(Q2, FirstMenu);
                    var t = TargetSelector.GetTarget(Q2.Range, DamageType.Physical);
                    var reksaifury = Equals(Program._player.Mana, Program._player.MaxMana);
                    if (te == null) return;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #region Combo0
            // COMBO 0 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 0)
            {
                if (Program.IsBurrowed())
                {
                    if (ComboMenu.GetCheckBoxValue("UseEBCombo"))
                    {
                        if (E.IsReady() && te.IsValidTarget(E.Range + W.Range) && Program._player.Distance(te) > Q.Range)
                        {
                            var predE22 = SpellsManager.E2.GetPrediction(te);
                            E2.Cast(predE22.CastPosition);
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseQBCombo") && Q2.IsReady() && predq2)
                    {
                        var predQ2 = SpellsManager.Q2.GetPrediction(targetQ2);
                        Q2.Cast(predQ2.CastPosition);
                    }

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {
                        if (W.IsReady() && targetW.IsValidTarget(W.Range) && !Q2.IsReady())
                        {
                            W.Cast(t);
                        }
                    }
                }

                if (!Program.IsBurrowed())
                {
                    if (ComboMenu.GetCheckBoxValue("UseQCombo") && Program._player.IsInAutoAttackRange(targetE))
                    {
                        Q.Cast();
                    }

                    if (ComboMenu.GetCheckBoxValue("UseECombo"))
                    {
                        if (targetE.IsValidTarget(E.Range) && E.IsReady())
                        {
                            if (reksaifury)
                            {
                                E.Cast(targetE);
                            }
                            else if (Program._player.Mana < 100 && t.Health <= targetE.GetDamage(SpellSlot.E))
                            {
                                E.Cast(targetE);
                            }
                            else if (Program._player.Mana == 100 && t.Health <= targetE.GetDamage(SpellSlot.E))
                            {
                                E.Cast(targetE);
                            }
                            else if (t.Health <= targetE.GetDamage(SpellSlot.E))
                            {
                                E.Cast(targetE);
                            }
                        }
                    }
                }

                if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                {
                    if (!Q.IsReady() && !targetW.IsValidTarget(E.Range) && targetW.IsValidTarget(Q2.Range))
                    {
                        W.Cast();
                    }
                }
            }
            // COMBO 0 END --------------------------------------------------------------------------------
            #endregion Combo0

            #region Combo1
            // COMBO 1 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 1)
            {
                if (Program.IsBurrowed())
                {
                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {

                        if (targetW != null && targetW.IsValidTarget())
                        {
                            W.Cast();
                            return;
                        }
                    }

                    if (ComboMenu.GetCheckBoxValue("UseQBCombo") && Q2.IsReady())
                    {

                        if (targetQ2 != null && targetQ2.IsValidTarget())
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

                        if (Player.Instance.CountEnemiesInRange(E2.Range) < 1)
                        {

                            if (targetE2.IsValidTarget())
                            {
                                E2.Cast(targetE2.Position + 200);
                                return;

                            }
                        }
                    }
                }

                if (!Program.IsBurrowed())
                {
                    var targettE = TargetSelector.GetTarget(E.Range, DamageType.Physical);
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

                    if (Program._player.IsFacing(targetQ))
                    {
                        if (ComboMenu.GetCheckBoxValue("UseQCombo") && Q.IsReady() && target.IsValidTarget(Q.Range + 50))
                        {
                            Q.Cast();
                            return;
                        }
                    }

                      else if (ComboMenu.GetCheckBoxValue("UseQCombo") && Q.IsReady() && target.IsValidTarget(Q.Range -20))
                        {
                            Q.Cast();
                            return;
                        }

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {
                        if (Player.Instance.CountEnemiesInRange(400) == 0)
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
            // COMBO 1 END --------------------------------------------------------------------------------
            #endregion Combo1

            #region Combo2
            // COMBO 2 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 2)
            {
                // w2 cast
                if (Program.IsBurrowed() && W.IsReady())
                {
                    if (targetW.IsValidTarget(W.Range) && !targetW.IsZombie)
                        W.Cast();
                }
                // e cast
                if (!Program.IsBurrowed() && E.IsReady() && Program._player.ManaPercent >= 50)
                {
                    if (targetE.IsValidTarget() && !targetE.IsZombie)
                        E.Cast(targetE);
                }
                // q2 cast
                if (Program.IsBurrowed() && Q2.IsReady())
                {
                    if (targetQ2.IsValidTarget() && !targetQ2.IsZombie)
                    {
                        Q2.Cast(targetQ2);
                    }
                }
                // W1 cast
                if (!Program.IsBurrowed() && W.IsReady() && !Program._player.IsStunned || !Program._player.IsRooted || !Program._player.IsTaunted || !Program._player.IsCharmed)
                {
                    if (targetW.IsValidTarget() && !targetW.IsZombie)
                    {
                        if (!(targetW as Obj_AI_Base).HasBuff("reksaiknockupimmune"))
                            W.Cast();
                    }
                    else
                    {
                        if (targetQ.IsValidTarget() && !targetQ.IsZombie && !targetQ.HasBuff("reksaiknockupimmune"))
                            W.Cast();
                    }
                }
                //E2 cast
                if (Program.IsBurrowed() && E.IsReady() && ComboMenu.GetCheckBoxValue("UseEBCombo"))
                {
                    if (Player.Instance.CountEnemiesInRange(E2.Range) >= 0)
                    {
                        if (targetE.IsValidTarget() && !targetE.IsZombie && E2.GetPrediction(targetE).HitChance >= Hitch.hitchance(E2, FirstMenu))
                        {
                            E2.Cast(Game.CursorPos);
                        }
                    }
                }
            }
            // COMBO 2 END --------------------------------------------------------------------------------
            #endregion Combo2

            #region Combo3
            // COMBO 3 Beginn --------------------------------------------------------------------------------
            if (ComboMenu["Comba"].Cast<ComboBox>().CurrentValue == 3)
            {
                if (Program.IsBurrowed())
                {
                    if (Q2.IsReady() && ComboMenu.GetCheckBoxValue("UseQBCombo"))
                    {
                        foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(x => x.IsEnemy && !x.IsZombie && x.IsValidTarget(Q2.Range)))
                        {
                            if (Q2.GetPrediction(targetQ2).HitChance >= Hitch.hitchance(Q2, FirstMenu))
                            {
                                Q2.Cast(enemy.Position);
                            }
                        }
                    }
                    if (E2.IsReady() && ComboMenu.GetCheckBoxValue("UseEBCombo"))
                    {
                        foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(x => x.IsEnemy && !x.IsZombie && x.IsValidTarget(E2.Range)))
                        {
                            E.Cast(enemy.Position - 50);
                        }
                    }
                    if (W.IsReady() && !Q2.IsReady() && !E2.IsReady()) // Auto Switch
                    {
                        W.Cast();
                    }
                }
                if (!Program.IsBurrowed())
                {
                    if (Q.IsReady() && ComboMenu.GetCheckBoxValue("UseQCombo"))
                    {
                        foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(x => x.IsEnemy && !x.IsZombie))
                        {
                            if (targetQ.IsValidTarget(Q.Range - 10))
                            {
                                Q.Cast();
                            }
                        }
                    }
                    if (E.IsReady() && ComboMenu.GetCheckBoxValue("UseECombo"))
                    {
                        foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(x => x.IsEnemy && !x.IsZombie && x.IsValidTarget(E.Range)))
                        {
                            E.Cast(enemy);
                        }
                    }
                    if (W.IsReady() && !Q.IsReady() && !E.IsReady()) // Auto Switch
                    {
                        W.Cast();
                    }
                }
            }
            // COMBO 3 END --------------------------------------------------------------------------------
            #endregion Combo3

        }
    }
    }
