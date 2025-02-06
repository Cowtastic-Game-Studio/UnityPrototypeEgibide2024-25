namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class BrokenFridgeEvent : CalendarEvent
    {
        public BrokenFridgeEvent() : base("Broken Fridge", "The fridge is broken! All the stored milk will be lost if it is not sold today.", 1)
        {
        }

        public override void InitEvent()
        {
        }

        public override void ApplyEffects()
        {
        }

        public override void EndEvent()
        {
            int currentMilk = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Milk);

            GameManager.Instance.Tabletop.StorageManager.RemoveResourceDownToMin(currentMilk, GameResource.Milk);
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();

            StatisticsManager.Instance.UpdateByStatisticType(StatisticType.EventsCompleted, 1);
        }
    }
}