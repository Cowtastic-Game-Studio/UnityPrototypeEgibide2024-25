using UnityEngine;

public class CowDayEvent : CalendarEvent
{
    public CowDayEvent() : base("Día de las Vacas", "Las vacas producen el doble de leche.", 1)
    {
    }

    public override void ApplyEffects()
    {
        Debug.Log("¡Doble de leche de las vacas durante el Día de las Vacas!");
        // Lógica para aplicar el efecto de doble producción de leche
    }

    public override void EndEvent()
    {
        Debug.Log("El Día de las Vacas ha terminado.");
        // Limpiar los efectos si es necesario
    }
}
