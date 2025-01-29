namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Statistic
    {

        public StatisticType StatType { get; set; }
        public string Title { get; set; }
        public CardType Type { get; set; }
        public GameResource Resource { get; set; }
        public int Uses { get; set; }

        public Statistic(StatisticType statType, string title, CardType type, GameResource resource, int uses)
        {
            StatType = statType;
            Title = title;
            Type = type;
            Resource = resource;
            Uses = uses;
        }
    }
}
