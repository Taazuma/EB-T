using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace Eclipse
{
    internal static class Utils
    {
        public static Vector3 To3DPlayer(this Vector2 vector)
        {
            return new Vector3(vector.X, vector.Y, ObjectManager.Player.Position.Z);
        }

        public static bool IsWall(this Vector2 vector)
        {
            return NavMesh.GetCollisionFlags(vector.X, vector.Y).HasFlag(CollisionFlags.Wall);
        }

        public static bool IsWall(this Vector3 vector)
        {
            return NavMesh.GetCollisionFlags(vector.X, vector.Y).HasFlag(CollisionFlags.Wall);
        }

        public static float GetPath(Obj_AI_Base hero, Vector3 b)
        {
            var path = hero.GetPath(b);
            var lastPoint = path[0];
            var distance = 0f;
            foreach (var point in path.Where(point => !point.Equals(lastPoint)))
            {
                distance += lastPoint.Distance(point);
                lastPoint = point;
            }
            return distance;
        }

        public static Vector3 Extend(this Vector3 vector, Vector3 direction, float distance)
        {
            if (vector.To2D().Distance(direction.To2D()) == 0)
            {
                return vector;
            }

            var edge = direction.To2D() - vector.To2D();
            edge.Normalize();

            var v = vector.To2D() + edge * distance;
            return new Vector3(v.X, v.Y, vector.Z);
        }
    }
}