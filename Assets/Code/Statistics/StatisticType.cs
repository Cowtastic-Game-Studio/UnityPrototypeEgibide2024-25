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

        // Tempoeary cards
        [EnumMember(Value = "Used activator")]
        PlaceActivatorCards,

        [EnumMember(Value = "Used multiplayers")]
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

        [EnumMember(Value = "Purchased zones with cards")]
        ZonesWithCardsPurchased,

        // Resources adquired
        [EnumMember(Value = "Total adquired Milk")]
        MilkTotalAdquired,

        [EnumMember(Value = "Total adquired cereal")]
        CerealTotalAdquired,

        [EnumMember(Value = "Total adquired muuney")]
        MuuneyTotalAdquired,

        // Resources used
        [EnumMember(Value = "Total used milk")]
        MilkTotalUsed,

        [EnumMember(Value = "Total used cereal")]
        CerealTotalUsed,

        [EnumMember(Value = "Total used muuney")]
        MuuneyTotalUsed,

        [EnumMember(Value = "Used AP")]
        APUsed,
        MilkSelled
    }
}
