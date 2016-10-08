namespace Eclipse
{
    using System;
    using System.Collections.Generic;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Notifications;
    using System.IO;
    using System.Linq;
    using System.Media;
    using System.Net;
    using EloBuddy.SDK.Constants;
    using EloBuddy.SDK.Events;
    using EloBuddy.SDK.Rendering;
    using SharpDX;
    using static Eclipse.SpellsManager;
    using static Eclipse.Menus;

    internal abstract class Hitch
    {
        //Thanks to Def. Kappa ^)

        public static HitChance hitchance(Spell.SpellBase spell, Menu m)
        {
            switch (m[spell.Slot + "hit"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    {
                        return HitChance.High;
                    }
                case 1:
                    {
                        return HitChance.Medium;
                    }
                case 2:
                    {
                        return HitChance.Low;
                    }
            }
            return HitChance.Unknown;
        }


        public static bool ShouldOverload(SpellSlot slot) //CREDITS MARIO
        {
            switch (slot)
            {
                case SpellSlot.Q:
                    return !SpellsManager.W.IsReady() && !SpellsManager.E.IsReady() && !SpellsManager.R.IsReady();
                case SpellSlot.W:
                    return !SpellsManager.Q.IsReady() && !SpellsManager.E.IsReady() && !SpellsManager.R.IsReady();
                case SpellSlot.E:
                    return !SpellsManager.Q.IsReady() && !SpellsManager.W.IsReady() && !SpellsManager.R.IsReady();
                case SpellSlot.R:
                    return !SpellsManager.Q.IsReady() && !SpellsManager.W.IsReady() && !SpellsManager.E.IsReady();
            }
            return false;
        } //CREDITS MARIO


        public static void CastR(AIHeroClient target, int minimunE) //CREDITS MARIO
        {

            if (target != null && target.CountEnemiesInRange(1000) == 1 && minimunE == 1)
            {
                if (target.IsMoving)
                {
                    var initPos = target.Position.To2D() - 125 * target.Direction.To2D().Perpendicular();
                    var endPos = target.Position.Extend(initPos.To3D(), -1000);

                    Player.CastSpell(SpellSlot.R, initPos.To3D(), endPos.To3D());
                }
                else
                {
                    var initPos = target.Position.To2D() - 490 * target.Direction.To2D().Perpendicular();
                    var endPos = target.Position.Extend(initPos.To3D(), -510);

                    Player.CastSpell(SpellSlot.R, initPos.To3D(), endPos.To3D());

                }

            }

            if (target != null && target.CountEnemiesInRange(1000) > 1 && minimunE > 1)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget()).Select(enemy => enemy.Position.To2D()).ToList();

                var initPos = target.Position.To2D() - 200 * target.Direction.To2D().Perpendicular();
                var endPos = GetBestEnPos(enemies, SpellsManager.R.Width, 990, minimunE, initPos);
                Chat.Print("Casting Ult");

                Player.CastSpell(SpellSlot.R, initPos.To3D(), endPos.To3D());
            }
        } //CREDITS MARIO

        public static Vector2 GetBestEnPos(List<Vector2> enemies, float width, float range, int minenemies, Vector2 initpos) //CREDITS MARIO
        {
            var enemyCount = 0;
            var startPos = initpos;
            var result = new Vector2();

            var posiblePositions = new List<Vector2>();
            posiblePositions.AddRange(enemies);

            var max = enemies.Count;
            for (var i = 0; i < max; i++)
            {
                for (var j = 0; j < max; j++)
                {
                    if (enemies[j] != enemies[i])
                    {
                        posiblePositions.Add((enemies[j] + enemies[i]) / 2);
                    }
                }
            }

            foreach (var pos in posiblePositions)
            {
                if (pos.Distance(startPos, true) <= range * range)
                {
                    var endPos = startPos.Extend(pos, range);

                    var count =
                        enemies.Count(pos2 => pos2.Distance(startPos, endPos, true, true) <= width * width);

                    if (count >= enemyCount && count >= minenemies)
                    {
                        result = endPos;
                        enemyCount = count;
                    }
                }
            }

            return new Vector2(result.X, result.Y);
        } //CREDITS MARIO


        public static bool IsCC(Obj_AI_Base target)
        {
            return target.IsStunned || target.IsRooted || target.IsTaunted || target.IsCharmed || target.Spellbook.IsChanneling
                   || target.HasBuffOfType(BuffType.Charm) || target.HasBuffOfType(BuffType.Knockback) || target.HasBuffOfType(BuffType.Knockup)
                   || target.HasBuffOfType(BuffType.Snare) || target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Suppression)
                   || target.HasBuffOfType(BuffType.Taunt);
        }

    }
}