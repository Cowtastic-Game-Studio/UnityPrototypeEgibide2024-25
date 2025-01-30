namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Statistic
    {

        public StatisticType StatType { get; set; }
        public string Title { get; set; }
        public CardType CardType { get; set; }
        public GameResource Resource { get; set; }
        public int Uses { get; set; }

        public bool IsUsed { get; set; }

        public Statistic(StatisticType statType, CardType type, GameResource resource, int uses, bool isUsed)
        {
            StatType = statType;
            CardType = type;
            Resource = resource;
            Uses = uses;
            IsUsed = isUsed;
        }
    }
}
