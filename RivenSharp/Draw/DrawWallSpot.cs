namespace RivenSharp.Draw
{
    #region

    using System;
    using System.Drawing;

    using Core;
    
    using EloBuddy.SDK;
    using EloBuddy;
    using EloBuddy.SDK.Rendering;

    #endregion

    internal class DrawWallSpot : RivenSharp.Core.Core
    {
        #region Public Methods and Operators

        public static void WallDraw(EventArgs args)
        {
            if (!MenuConfig.FleeSpot)
            {
                return;
            }

            var end = Player.ServerPosition.Extend(Game.CursorPos, 350).To3D();
            var isWallDash = FleeLogic.IsWallDash(end, 350);

            var wallPoint = FleeLogic.GetFirstWallPoint(Player.ServerPosition, end);

            if (!isWallDash || wallPoint.Distance(Player.ServerPosition) > 600)
            {
                return;
            }

            Circle.Draw(SharpDX.Color.DarkSlateGray, 60, wallPoint);
            Circle.Draw(SharpDX.Color.White, 60, end);
        }

        #endregion
    }
}