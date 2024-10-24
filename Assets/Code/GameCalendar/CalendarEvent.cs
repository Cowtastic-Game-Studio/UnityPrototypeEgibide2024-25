using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{

    //[CreateAssetMenu(fileName = "NewCalendarEvent", menuName = "Calendar/Event")]
    public abstract class CalendarEvent // : ScriptableObject
    {
        // Nombre del evento
        public string eventName { get; private set; }

        // Descripción del evento
        public string eventDescription { get; private set; }

        // Duración del evento en días
        public int duration { get; private set; }

        // Estado del evento
        public bool isActive { get; private set; }

        // Constructor para inicializar el evento
        public CalendarEvent(string name, string description, int duration)
        {
            this.eventName = name;
            this.eventDescription = description;
            this.duration = duration;
            this.isActive = false;
        }

        // Método para activar el evento
        public virtual void TriggerEvent()
        {
            isActive = true;
            Debug.Log($"{eventName} ha comenzado. {eventDescription}");
            ApplyEffects();
        }

        // Método abstracto para aplicar los efectos específicos del evento
        public abstract void ApplyEffects();

        // Método para finalizar el evento
        public virtual void EndEvent()
        {
            isActive = false;
            Debug.Log($"Evento {eventName} ha terminado.");
        }
    }
}