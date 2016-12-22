namespace RivenSharp.Draw
{
    #region

    using System;
    using System.Drawing;

    using Core;
    using EloBuddy.SDK.Rendering;
    using EloBuddy;

    #endregion

    internal class DrawMisc : Core
    {
        #region Public Methods and Operators

        public static void RangeDraw(EventArgs args)
        {
            if (Player.IsDead)
            {
                return;
            }

            if (MenuConfig.DrawCb)
            {
                if (Spells.E.IsReady())
                {
                    Circle.Draw(Spells.Q.IsReady() ? SharpDX.Color.DodgerBlue : SharpDX.Color.DarkSlateGray, 370 + Player.AttackRange, Player);
                }
                else
                {
                    Circle.Draw(Spells.Q.IsReady() ? SharpDX.Color.LightBlue : SharpDX.Color.DarkSlateGray, Player.AttackRange, Player);
                }
            }
            
            if (MenuConfig.DrawBt && Spells.Flash != SpellSlot.Unknown && Player.Spellbook.CanUseSpell(Spells.Flash) == SpellState.Ready)
            {
                Circle.Draw(SharpDX.Color.Orange, Player.AttackRange + 625, Player);
            }

            var pos = Drawing.WorldToScreen(Player.Position);

            var offset = 0;

            if (MenuConfig.DrawAlwaysR)
            {
                offset += 20;
                Drawing.DrawText(pos.X - 20, pos.Y + offset, Color.White, "Use R1  (     )");
                Drawing.DrawText(pos.X + 41, pos.Y + offset, MenuConfig.AlwaysR  ? Color.Green : Color.Red, MenuConfig.AlwaysR ? "On" : "Off");
            }

            if(MenuConfig.DrawBurst)
            {
                offset += 20;
                Drawing.DrawText(pos.X - 20, pos.Y + offset, Color.White, "Burst Mode  (     )");
                Drawing.DrawText(pos.X + 67, pos.Y + offset, MenuConfig.BurstEnabled ? Color.Green : Color.Red, MenuConfig.BurstEnabled ? "On" : "Off");

                if (MenuConfig.ForceFlash)
                {
                    offset += 20;
                    Drawing.DrawText(pos.X - 20, pos.Y + offset, Color.White, "Use Flash  (     )");
                    Drawing.DrawText(pos.X + 59, pos.Y + offset, MenuConfig.AlwaysF ? Color.Green : Color.Red, MenuConfig.AlwaysF ? "On" : "Off");
                }
            }

        }

        #endregion
    }
}