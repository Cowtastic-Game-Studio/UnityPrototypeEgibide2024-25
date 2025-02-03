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

        [EnumMember(Value = "Purchased zones")]
        ZonesPurchased,

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

        [EnumMember(Value = "Total used customers")]
        CustomersTotalUsed,

        [EnumMember(Value = "Used AP")]
        APUsed,

        [EnumMember(Value = "Count uppgrade stable")]
        StableCountUpgrade,

        [EnumMember(Value = "Count uppgrade garden")]
        GardenCountUpgrade,

        [EnumMember(Value = "Count uppgrade tavern")]
        TavernCountUpgrade,

        [EnumMember(Value = "Count uppgrade fridge")]
        FridgeCountUpgrade,

        [EnumMember(Value = "Count uppgrade silo")]
        SiloCountUpgrade,

        // New Objectives
        [EnumMember(Value = "Fill all stable slots with cows")]
        StableFilledWithCowsMaxUpgrade,

        [EnumMember(Value = "Plant crops in all garden slots")]
        GardenFilledWithCropsMaxUpgrade,

        [EnumMember(Value = "Fill the entire tavern with customers")]
        TavernFilledWithCustomersMaxUpgrade,

        [EnumMember(Value = "Complete 3 events")]
        EventsCompleted,
    }
}
