using UnityEngine;

public class BrokenFridgeEvent : CalendarEvent
{
    public BrokenFridgeEvent() : base("Nevera Rota", "Perder�s toda la leche almacenada si no la vendes hoy.", 1)
    {
    }

    public override void ApplyEffects()
    {
        Debug.Log("�La nevera se ha roto! Toda la leche almacenada se perder� si no se vende.");
        // L�gica para la p�rdida de leche almacenada si no se vende al final del d�a
    }

    public override void EndEvent()
    {
        Debug.Log("El evento de la nevera rota ha terminado.");
        // Limpiar los efectos si es necesario
    }
}
