using UnityEngine;

public class CowDayEvent : CalendarEvent
{
    public CowDayEvent() : base("D�a de las Vacas", "Las vacas producen el doble de leche.", 1)
    {
    }

    public override void ApplyEffects()
    {
        Debug.Log("�Doble de leche de las vacas durante el D�a de las Vacas!");
        // L�gica para aplicar el efecto de doble producci�n de leche
    }

    public override void EndEvent()
    {
        Debug.Log("El D�a de las Vacas ha terminado.");
        // Limpiar los efectos si es necesario
    }
}
