namespace CowtasticGameStudio.MuuliciousHarvest
{
    //[CreateAssetMenu(fileName = "NewCalendarEvent", menuName = "Calendar/Event")]
    public abstract class CalendarEvent
    {
        // Nombre del evento
        public string eventName { get; private set; }

        // Descripci�n del evento
        public string eventDescription { get; private set; }

        // Duraci�n del evento en d�as
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

        public abstract void InitEvent();


        // M�todo para activar el evento
        public virtual void TriggerEvent()
        {
            InitEvent();
            isActive = true;
            MessageManager.Instance.ShowMessage($"{eventName} started. {eventDescription}", 1);
            UnityEngine.Debug.LogWarning($"{eventName} started. {eventDescription}");
            ApplyEffects();
        }

        // M�todo abstracto para aplicar los efectos espec�ficos del evento
        public abstract void ApplyEffects();

        // M�todo para finalizar el evento
        public virtual void EndEvent()
        {
            isActive = false;
            MessageManager.Instance.ShowMessage($"Event {eventName} ended.", 1);
            //Debug.LogWarning($"Event {eventName} ended.");
        }
    }
}