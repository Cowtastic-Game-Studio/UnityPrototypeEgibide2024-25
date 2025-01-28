using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    /// <summary>
    /// Evento generico que multiplica la optencion de un tipo de recurso
    /// </summary>
    public class ResourceMultipleEvent : CalendarEvent
    {
        private GameResource resourceType;
        private int resourceMultiplier;

        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        /// <param name="eventName">Nombre del evento.</param>
        /// <param name="eventDescription">Descripción del evento.</param>
        /// <param name="eventDuration">Duración del evento en días.</param>
        /// <param name="cardType">Tipo de carta afectada durante el evento.</param>
        /// <param name="resourceMultiplier">Multiplicador de recursos durante el evento.</param>
        public ResourceMultipleEvent(string eventName, string eventDescription, int eventDuration, GameResource resourceType, int resourceMultiplier) 
            : base(eventName, eventDescription, eventDuration)
        {
            this.resourceType = resourceType;
            this.resourceMultiplier = resourceMultiplier;
        }

        public override void ApplyEffects()
        {
            GameManager.Instance.Tabletop.StorageManager.SetResourceMultiplierAndType(resourceMultiplier, resourceType);
            Debug.Log($"Evento de multiplicar activado: tipo de carta {resourceType}, multiplicador de recursos: {resourceMultiplier}.");
        }

        public override void EndEvent()
        {
            GameManager.Instance.Tabletop.StorageManager.ClearResourceMultiplierAndType();
            Debug.Log($"Se acabo el multiplicar las cosas por {resourceMultiplier}");
        }

        public override void InitEvent()
        {
        }
    }
}
