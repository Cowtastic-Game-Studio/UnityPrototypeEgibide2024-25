using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlagueEvent : CalendarEvent
    {
        private PlaceSpaceBehaviour[] spaceBehaviourList;

        private List<PlaceSpaceBehaviour> gardenList = new();
        private List<PlaceSpaceBehaviour> inactiveGardenList = new();


        public PlagueEvent() : base("Plaga", "Las zonas de cultivo han sido destrozadas, pierdes el uso de la mitad de tus huertos.", 1)
        {
        }

        public override void InitEvent()
        {
            spaceBehaviourList = GameObject.FindObjectsOfType(typeof(PlaceSpaceBehaviour)) as PlaceSpaceBehaviour[];
            foreach (PlaceSpaceBehaviour item in spaceBehaviourList)
            {
                if (item.GetType(item) == CardType.Seed && item.GetIsActive() == true)
                {
                    gardenList.Add(item);
                }
            }
        }

        public override void ApplyEffects()
        {
            for (int i = 0; i < gardenList.Count / 2; i++)
            {
                gardenList[i].SetIsActive(false);
                inactiveGardenList.Add(gardenList[i]);
            }
        }

        public override void EndEvent()
        {
            // Posible limpieza de efectos si la plaga tiene un impacto a largo plazo
            foreach (PlaceSpaceBehaviour item in inactiveGardenList)
            {
                item.SetIsActive(true);
            }

            StatisticsManager.Instance.UpdateByStatisticType(StatisticType.EventsCompleted, 1);
        }
    }
}