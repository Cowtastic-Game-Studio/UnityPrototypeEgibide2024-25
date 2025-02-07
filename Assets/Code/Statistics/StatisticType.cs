using System.Runtime.Serialization;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public enum StatisticType
    {
        // Cards
        [EnumMember(Value = "Milked cows")]
        CowsMilked,

        [EnumMember(Value = "Harvested seeds")]
        SeedsHarvested,

        [EnumMember(Value = "Served customers")]
        CustomersServed,

        // Temporary cards
        [EnumMember(Value = "Used activator")]
        PlaceActivatorCards,

        [EnumMember(Value = "Used multiplier")]
        PlaceMultiplierCards,

        [EnumMember(Value = "Used helpers")]
        HelperCards,

        [EnumMember(Value = "Used temporary cards")]
        TemporaryUsedCards,

        [EnumMember(Value = "Total used Cards")]
        CardsTotalUsed,

        // Market
        [EnumMember(Value = "Discarded Cards")]
        CardsDiscarded,

        [EnumMember(Value = "Purchased cards")]
        CardsPurchased,

        [EnumMember(Value = "Purchased zone upgrade")]
        ZonesUpgradePurchased,

        [EnumMember(Value = "Purchased zones with cards")]
        ZonesWithCardsPurchased,

        // Resources acquired
        [EnumMember(Value = "Total acquired Milk")]
        MilkTotalAcquired,

        [EnumMember(Value = "Total acquired cereal")]
        CerealTotalAcquired,

        [EnumMember(Value = "Total acquired muuney")]
        MuuneyTotalAcquired,

        // Resources used
        [EnumMember(Value = "Total used milk")]
        MilkTotalUsed,

        [EnumMember(Value = "Total used cereal")]
        CerealTotalUsed,

        [EnumMember(Value = "Total used muuney")]
        MuuneyTotalUsed,

        [EnumMember(Value = "Used AP")]
        APUsed,

        // Mission globals
        [EnumMember(Value = "Count upgrade stable")]
        StableCountUpgrade,

        [EnumMember(Value = "Count upgrade garden")]
        GardenCountUpgrade,

        [EnumMember(Value = "Count upgrade tavern")]
        TavernCountUpgrade,

        [EnumMember(Value = "Count upgrade fridge")]
        FridgeCountUpgrade,

        [EnumMember(Value = "Count upgrade silo")]
        SiloCountUpgrade,

        [EnumMember(Value = "Fill all stable slots with cows")]
        StableFull,

        [EnumMember(Value = "Plant crops in all garden slots")]
        FarmFull,

        [EnumMember(Value = "Fill the entire tavern with customers")]
        ShopFull,

        [EnumMember(Value = "Complete events")]
        EventsCompleted,
    }
}
