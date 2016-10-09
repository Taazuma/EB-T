using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static RoninTune.Menus;
using static RoninTune.SpellsManager;

namespace RoninTune
{
    public static class _W_Advance
    {
        static _W_Advance()
        {
            new Advance("Vladimir", "vladimirhemoplaguedebuff", SpellSlot.R).Add();
            new Advance("Tristana", "tristanaechargesound", SpellSlot.E).Add();
            new Advance("Karma", "karmaspiritbind", SpellSlot.W).Add();
            new Advance("Karthus", "karthusfallenone", SpellSlot.R).Add();
            new Advance("Leblanc", "leblancsoulshackle", SpellSlot.E).Add();
            new Advance("Leblanc", "leblancsoulshackler", SpellSlot.R).Add();
            new Advance("Morgana", "soulshackles", SpellSlot.R).Add();
            new Advance("Zed", "zedultexecute", SpellSlot.R).Add();
            new Advance("Fizz", "fizzmarinerdoombomb", SpellSlot.R).Add();

            foreach (var dispell in Advance.GetDispellList().Where(d => EntityManager.Heroes.Enemies.Any(h => h.ChampionName.Equals(d.ChampionName))))
            {
               RoninTune.Menus.MiscMenu.Add(dispell.ChampionName + dispell.BuffName, new CheckBox(dispell.ChampionName + " - " + dispell.Slot, true));
            }

            Game.OnUpdate += OnUpdate;
        }

        static void OnUpdate(EventArgs args)
        {
            if (!W.IsReady() || MiscMenu.GetCheckBoxValue("wLogic"))
                return;

            foreach (var dispell in Dispells.Where(
                d =>
                    Player.HasBuff(d.BuffName) &&
                    MiscMenu[d.ChampionName + d.BuffName] != null &&
                    MiscMenu[d.ChampionName + d.BuffName].Cast<CheckBox>().CurrentValue
                ))
            {
                var buff = Player.GetBuff(dispell.BuffName);
                if (buff == null || !buff.IsValid || !buff.IsActive)
                    continue;
                var milisec = (buff.EndTime - Game.Time) * 1000f + dispell.Offset + 250;
                var Emilisec = W.CastDelay + Game.Ping / 2f;

                if (milisec < Emilisec)
                {
                    W.Cast();                  
                }
            }
        }

        public static List<Advance> Dispells
        {
            get { return Advance.GetDispellList(); }
        }

        public static void Initialize()
        {

        }
    }

    public class Advance
    {
        private static readonly List<Advance> DispellList = new List<Advance>();
        public string BuffName;
        public string ChampionName;
        public int Offset;
        public SpellSlot Slot;

        public Advance(string champName, string buff, SpellSlot slot, int offset = 0)
        {
            ChampionName = champName;
            BuffName = buff;
            Slot = slot;
            Offset = offset;
        }

        public void Add()
        {
            DispellList.Add(this);
        }

        public static List<Advance> GetDispellList()
        {
            return DispellList;
        }
    }
}
