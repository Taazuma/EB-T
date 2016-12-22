namespace RivenSharp.Core
{
    using EloBuddy;
    #region

    using EloBuddy.SDK;

    #endregion

    internal class Usables : Core
    {
        #region Public Methods and Operators

        private static bool HasTitanAndIsReady() => (Item.HasItem(ItemId.Titanic_Hydra) && Item.CanUseItem(ItemId.Titanic_Hydra));
        private static bool HasRavenousHydraAndIsReady() => (Item.HasItem(ItemId.Ravenous_Hydra_Melee_Only) && Item.CanUseItem(ItemId.Ravenous_Hydra_Melee_Only));
        private static bool HasTiamatAndIsReady() => (Item.HasItem(ItemId.Tiamat) && Item.CanUseItem(ItemId.Tiamat));

        public static void CastHydra()
        {
            if (HasRavenousHydraAndIsReady())
            {
                Item.UseItem(ItemId.Ravenous_Hydra_Melee_Only);
            }
            if (HasTitanAndIsReady())
            {
                Item.UseItem(ItemId.Titanic_Hydra);
            }
            if (HasTiamatAndIsReady())
            {
                Item.UseItem(ItemId.Tiamat);
            }
        }

        public static void CastYoumoo()
        {
            if (Item.HasItem(ItemId.Youmuus_Ghostblade) && Item.CanUseItem(ItemId.Youmuus_Ghostblade))
                Item.UseItem(ItemId.Youmuus_Ghostblade);
        }

        #endregion
    }
}