using UnityEngine;

public class HarvestDayEvent : CalendarEvent
{
    public HarvestDayEvent() : base("D�a de la Cosecha", "Los cultivos generan el doble de recursos al cosechar.", 1)
    {
    }

    // Implementa los efectos del evento
    public override void ApplyEffects()
    {
        Debug.Log("�Doble de recursos al cosechar cultivos durante el D�a de la Cosecha!");
        // Aqu� ir�a la l�gica para aplicar el efecto de cosecha doble
    }

    // Opcionalmente puedes sobreescribir EndEvent si el evento tiene efectos que terminan
    public override void EndEvent()
    {
        Debug.Log("El D�a de la Cosecha ha terminado.");
        // Limpiar los efectos del evento, si es necesario
    }
}
