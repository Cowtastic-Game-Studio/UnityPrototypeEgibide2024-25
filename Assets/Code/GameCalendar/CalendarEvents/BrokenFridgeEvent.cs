using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class BrokenFridgeEvent : CalendarEvent
    {
        public BrokenFridgeEvent() : base("Nevera Rota", "Perderas toda la leche almacenada si no la vendes hoy.", 1)
        {
        }

        public override void InitEvent()
        {
        }

        public override void ApplyEffects()
        {
            Debug.Log("La nevera se ha roto! Toda la leche almacenada se perdera si no se vende.");
        }

        public override void EndEvent()
        {
            int currentMilk = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Milk);

            GameManager.Instance.Tabletop.StorageManager.RemoveResourceDownToMin(currentMilk, GameResource.Milk);
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            Debug.Log("El evento de la nevera rota ha terminado.");

            StatisticsManager.Instance.UpdateByStatisticType(StatisticType.EventsCompleted, 1);
        }
    }
}