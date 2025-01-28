using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class HarvestDayEvent : CalendarEvent
    {

        public CardType cardType;

        public HarvestDayEvent() : base("D�a de la Cosecha", "Los cultivos generan el doble de recursos al cosechar.", 1)
        {
        }

        public override void InitEvent()
        {
        }

        // Implementa los efectos del evento
        public override void ApplyEffects()
        {
            Debug.Log("�Doble de recursos al cosechar cultivos durante el D�a de la Cosecha!");

        }

        // Opcionalmente puedes sobreescribir EndEvent si el evento tiene efectos que terminan
        public override void EndEvent()
        {
            Debug.Log("El D�a de la Cosecha ha terminado.");
            // Limpiar los efectos del evento, si es necesario
        }
    }
}