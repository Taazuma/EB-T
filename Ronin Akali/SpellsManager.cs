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

namespace Eclipse
{
    public static class SpellsManager
    {

        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;

        public static void InitializeSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 250, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

    

        private static SpellSlot GetSlotFromComboBox(this int value)
        {
            switch (value)
            {
                case 0:
                    return SpellSlot.Q;
                case 1:
                    return SpellSlot.W;
                case 2:
                    return SpellSlot.E;
            }
            Chat.Print("Failed getting slot");
            return SpellSlot.Unknown;
        }
    }
}