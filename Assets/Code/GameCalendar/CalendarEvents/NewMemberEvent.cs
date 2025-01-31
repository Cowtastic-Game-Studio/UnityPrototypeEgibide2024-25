using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class NewMemberEvent : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public NewMemberEvent() 
            : base("¡Te han robado!", "Algo o laguien ha entrado a tu granja y...", 1)
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
