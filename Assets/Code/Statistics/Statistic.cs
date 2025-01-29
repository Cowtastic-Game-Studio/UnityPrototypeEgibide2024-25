namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Statistic
    {
        public string Title { get; set; }
        public CardType Type { get; set; }
        public GameResource Resource { get; set; }
        public int Uses { get; set; }

        public Statistic(string title, CardType type, GameResource resource, int uses)
        {
            Title = title;
            Type = type;
            Resource = resource;
            Uses = uses;
        }
    }
}
