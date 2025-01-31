using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class VentDayEvent : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public VentDayEvent() 
            : base("El veterinario a venido", "Ahora tus vacas duraran un dia mas... no te encariñes con ellas moriran como todos nostros algun dia :D", 1)
        {

        }

        public override void ApplyEffects()
        {
            foreach (GameObject card in GameManager.Instance.Tabletop.CardManager.PlayedDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();

                if (cardBH.Type == CardType.Cow) 
                {
                    cardBH.IncreaseLifeCycleDays(1);
                }
            }

            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
