using EloBuddy;

namespace Eclipse
{
    class Utils
    {
        public static AIHeroClient getPlayer()
        {
            return ObjectManager.Player;
        }
        public static double square(double x)
        {
            return x * x;
        }
        public static double sqrt(double x)
        {
            return System.Math.Sqrt(x);
        }

    }
}
