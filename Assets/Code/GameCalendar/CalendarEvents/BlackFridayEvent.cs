using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class BlackFridayEvent : CalendarEvent
    {
        private Dictionary<CardTemplate, int> originalCosts = new();

        public BlackFridayEvent() : base("Black Friday", "Today it's Black Friday you have a 50% discount in the market.", 1)
        {
        }


        public override void InitEvent()
        {

        }



        public override void ApplyEffects()
        {
            GameManager.Instance.Tabletop.NewMarketManager.discountPercentage = 0.5f;
        }

        public override void EndEvent()
        {
            GameManager.Instance.Tabletop.NewMarketManager.discountPercentage = 1f;
        }
    }
}
