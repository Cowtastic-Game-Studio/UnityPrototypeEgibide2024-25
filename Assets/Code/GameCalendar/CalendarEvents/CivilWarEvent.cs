using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CivilWarEvent : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        /// <param name="eventName">Nombre del evento.</param>
        /// <param name="eventDescription">Descripción del evento.</param>
        /// <param name="eventDuration">Duración del evento en días.</param>
        /// <param name="cardType">Tipo de carta afectada durante el evento.</param>
        /// <param name="resourceMultiplier">Multiplicador de recursos durante el evento.</param>
        public CivilWarEvent() 
            : base("La guerra estalla y tu pagas", "El reino entra en guerra y el rey te exige el 20% de tu Muuuney", 1)
        {

        }

        public override void ApplyEffects()
        {

        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
