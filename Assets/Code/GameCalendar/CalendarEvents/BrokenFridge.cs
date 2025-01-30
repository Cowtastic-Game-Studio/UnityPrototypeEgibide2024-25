using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class BrokenFridge : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public BrokenFridge() 
            : base("Frigorifico roto", "El frigo la ha espichado mas te vale vender toda la leche hpy por que si no la pierdes", 1)
        {

        }

        public override void ApplyEffects()
        {
            int currentMuuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);

            if (currentMuuney > 0)
            {
                double twentyPercent = currentMuuney * 0.20f;
                int roundedMuuney = Utils.RoundMuuney(twentyPercent);

                GameManager.Instance.Tabletop.StorageManager.WasteMuuney(roundedMuuney);
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
            }
        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
