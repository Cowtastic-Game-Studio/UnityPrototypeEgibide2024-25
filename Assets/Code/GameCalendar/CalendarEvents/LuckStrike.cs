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
            : base("Golpe de suerte", "Tu gatito ha encontrado algo... ostia tu cuanta munney", 1)
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
            Debug.Log("Se ha aadido Muuney: " + roundedMuuney);
        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
