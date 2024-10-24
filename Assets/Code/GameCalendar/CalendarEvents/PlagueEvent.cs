using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlagueEvent : CalendarEvent
    {
        public PlagueEvent() : base("Plaga", "Los cultivos han sido destruidos. Posibles p�rdidas de mejoras.", 2)
        {
        }

        public override void ApplyEffects()
        {
            Debug.Log("�Plaga! Los cultivos han sido destruidos.");
            // L�gica para destruir cultivos
        }

        public override void EndEvent()
        {
            Debug.Log("La plaga ha terminado.");
            // Posible limpieza de efectos si la plaga tiene un impacto a largo plazo
        }
    }
}