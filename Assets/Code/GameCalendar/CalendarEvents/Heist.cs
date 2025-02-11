using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Heist : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public Heist()
            : base("¡Te han robado!", "Algo o laguien ha entrado a tu granja y...", 1)
        {

        }

        public override void ApplyEffects()
        {
            int currentMuuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);
            int currentCereal = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Cereal);
            int currentMilk = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Milk);

            // Opciones de recursos a robar
            List<(GameResource type, int amount)> possibleThefts = new List<(GameResource, int)>
            {
                (GameResource.Milk, 2),
                (GameResource.Cereal, 3),
                (GameResource.Muuney, 5)
            };

            // Filtrar solo recursos que pueden ser robados
            List<(GameResource type, int amount)> validThefts = new List<(GameResource, int)>();

            if (currentMilk >= 2) validThefts.Add((GameResource.Milk, 2));
            if (currentCereal >= 3) validThefts.Add((GameResource.Cereal, 3));
            if (currentMuuney >= 5) validThefts.Add((GameResource.Muuney, 5));

            if (validThefts.Count == 0)
            {
                GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(1, GameResource.Muuney, true);
                //Debug.LogWarning("Since you didn't have anything to steal, they took pity on you and left you some money.");
                MessageManager.Instance.ShowMessage("Since you didn't have anything to steal, they took pity on you and left you some money.");
            }
            else
            {
                // Elegir un recurso al azar para robar
                var theft = validThefts[Random.Range(0, validThefts.Count)];

                switch (theft.type)
                {
                    case GameResource.Milk:
                        GameManager.Instance.Tabletop.StorageManager.RemoveResourceDownToMin(theft.amount, GameResource.Milk);
                        MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
                        //Debug.LogWarning($"Te han robado {theft.amount} de leche.");
                        break;

                    case GameResource.Cereal:
                        GameManager.Instance.Tabletop.StorageManager.RemoveResourceDownToMin(theft.amount, GameResource.Cereal);
                        //Debug.LogWarning($"Te han robado {theft.amount} de cereal.");
                        MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de cereal.");
                        break;

                    case GameResource.Muuney:
                        GameManager.Instance.Tabletop.StorageManager.WasteMuuney(theft.amount);
                        // Debug.LogWarning($"Te han robado {theft.amount} de muuney.");
                        MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de muuney.");
                        break;
                }
            }

            // TODO Aviso
            // Esto se ejecutara independientemente del resultado ya que la idea de los diseadores
            // Es la de que si no te pueden robar nada te den uno de munney por pobre y desgraciado
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
        }

        public override void EndEvent()
        {
            StatisticsManager.Instance.UpdateByStatisticType(StatisticType.EventsCompleted, 1);
        }

        public override void InitEvent()
        {

        }
    }
}
