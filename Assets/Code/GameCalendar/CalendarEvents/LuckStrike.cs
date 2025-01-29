using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class LuckStrike : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public LuckStrike() 
            : base("Golpe de suerte", "Tu gatito ha encontrado algo... ostia tu cuanta munney", 1)
        {

        }

        public override void ApplyEffects()
        {
            int currentMaxMuuney = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Muuney);
            double tenPercentMuuney = currentMaxMuuney * 0.10;
            int roundedMuuney = Utils.RoundMuuney((int)tenPercentMuuney);
            
            GameManager.Instance.Tabletop.HUDManager.UpdateResources();

            Debug.LogError("Rounded Muuney: " + roundedMuuney);
        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
