namespace RivenSharp.Draw
{
    #region

    using System;
    using System.Linq;

    using Core;

    using SharpDX;
    using EloBuddy.SDK;
    using EloBuddy;
    using static RivenSharp.Core.Spells;
    #endregion

    internal class DrawDmg
    {
        private static readonly HpBarIndicator Indicator = new HpBarIndicator();

        private static Vector2 BarOffset = new Vector2(0, 15);
        private const int BarWidth = 104;
        private const int LineThickness = 10;

        public static void DmgDraw(EventArgs args)
        {
            var targeter = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);

            if (targeter == null || targeter.IsInvulnerable || targeter.MagicImmune)
            {
                return;
            }
            if (Player.Instance.CountEnemiesInRange(1500) >= 2 && targeter.Type == Player.Instance.Type && targeter.IsValidTarget())
            {
                if (!MenuConfig.Dind || ObjectManager.Player.IsDead)
                {
                    return;
                }

                var damage = Dmg.GetComboDamage(targeter) * .85;
                //Chat.Print(enemy.BaseSkinName);
                var tempOffset = 0;
                if (targeter.BaseSkinName == "Annie") tempOffset -= 12;
                if (targeter.BaseSkinName == "Jhin") tempOffset -= 14;

                var damagePercentage = ((targeter.TotalShieldHealth() - 0.9 * damage) > 0 ? (targeter.TotalShieldHealth() - damage) : 0) / (targeter.MaxHealth + targeter.AllShield + targeter.AttackShield + targeter.MagicShield);
                var currentHealthPercentage = targeter.TotalShieldHealth() / (targeter.MaxHealth + targeter.AllShield + targeter.AttackShield + targeter.MagicShield);

                var startPoint = new Vector2((int)(targeter.HPBarPosition.X + BarOffset.X + damagePercentage * BarWidth), (int)(targeter.HPBarPosition.Y + BarOffset.Y + tempOffset) - 5);
                var endPoint = new Vector2((int)(targeter.HPBarPosition.X + BarOffset.X + currentHealthPercentage * BarWidth) + 1, (int)(targeter.HPBarPosition.Y + BarOffset.Y + tempOffset) - 5);

                Drawing.DrawLine(startPoint, endPoint, LineThickness, System.Drawing.Color.Chartreuse);

                //Indicator.Unit = enemy;
                //Indicator.DrawDmg(Dmg.GetComboDamage(enemy),  enemy.Health <= Dmg.GetComboDamage(enemy) * .85 ? Color.LawnGreen : Color.Yellow);
            }
        }
    }
}