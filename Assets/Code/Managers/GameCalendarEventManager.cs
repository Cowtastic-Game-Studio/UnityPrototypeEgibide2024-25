using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GameCalendarEventManager
    {
        private List<CalendarEvent> staticEvents;
        private List<CalendarEvent> dynamicEvents;
        private CalendarEvent activeEvent;

        public GameCalendarEventManager()
        {
            staticEvents = new List<CalendarEvent>();
            dynamicEvents = new List<CalendarEvent>();
            activeEvent = null;
        }

        public void TestActiveEvent()
        {
            activeEvent = staticEvents[1]; //Plague
            activeEvent.TriggerEvent();
        }

        public void AddEvent(CalendarEvent calendarEvent, bool isDynamic)
        {
            if (isDynamic)
            {
                staticEvents.Add(calendarEvent);
            }
            else
            {
                staticEvents.Add(calendarEvent);
            }
            calendarEvent.InitEvent();
        }

        public void RemoveEvent(CalendarEvent calendarEvent)
        {
            staticEvents.Remove(calendarEvent);
        }

        public void TriggerRandomEvent()
        {
            if (dynamicEvents.Count > 0)
            {
                // Genera un �ndice aleatorio
                int index = UnityEngine.Random.Range(0, dynamicEvents.Count);
                activeEvent = dynamicEvents[index];
                // Llama al m�todo TriggerEvent
                activeEvent.TriggerEvent();
            }
        }

        public void EndActiveEvent()
        {
            if (activeEvent != null)
            {
                // Llama al m�todo EndEvent
                activeEvent.EndEvent();
                activeEvent = null;
            }
        }
    }
}
