using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class LuckStrike : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento LuckStrike.
        /// Donde el gatito del jugador le otorga una bolsa con el 10% de muuney que puede tener
        /// </summary>
        public LuckStrike()
            : base("Stroke of luck", "Your kitty has found something.. damn, that's a lot of muuney!", 1)
        {

        }

        public override void ApplyEffects()
        {
            // Calculo del 10 porciento del dinero total que puede tener el jugador
            int currentMaxMuuney = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Muuney);
            double tenPercentMuuney = currentMaxMuuney * 0.10;
            int roundedMuuney = Utils.RoundMuuney((int)tenPercentMuuney);

            // Añadir dinero y actualizar hud
            GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(roundedMuuney, GameResource.Muuney, true);
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            Debug.LogWarning("Added muuney: " + roundedMuuney);
            MessageManager.Instance.ShowMessage($"Te han robado {theft.amount} de leche.");
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
