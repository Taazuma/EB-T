using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using System.Linq;

namespace Eclipse
{
    internal class DrawingsManager
    {
        public static void InitializeDrawings()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            DamageIndicator.Init();
        }
        //public static Obj_AI_Minion Minion;
        //private static AIHeroClient Hero = ObjectManager.Player;
        /// Normal Drawings will not ovewrite any of LOL Sprites
        //</summary>
        // <param name="args"></param>
        private static void Drawing_OnDraw(EventArgs args)
        {

        }

        /// <summary>
        /// This one will overwrite LOL sprites like menus, healthbar and etc
        /// </summary>
        /// <param name="args"></param>
        private static void Drawing_OnEndScene(EventArgs args)
        {

        }
    }
}
