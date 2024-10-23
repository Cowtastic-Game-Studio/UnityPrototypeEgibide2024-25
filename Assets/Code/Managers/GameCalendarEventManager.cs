using System.Collections.Generic;
using UnityEngine; 

public class GameCalendarEventManager
{
    private List<CalendarEvent> availableEvents;
    private CalendarEvent activeEvent;

    public GameCalendarEventManager()
    {
        availableEvents = new List<CalendarEvent>();
        activeEvent = null;
    }

    public void AddEvent(CalendarEvent calendarEvent)
    {
        availableEvents.Add(calendarEvent);
    }

    public void RemoveEvent(CalendarEvent calendarEvent)
    {
        availableEvents.Remove(calendarEvent);
    }

    public void TriggerRandomEvent()
    {
        if (availableEvents.Count > 0)
        {
            // Genera un �ndice aleatorio
            int index = UnityEngine.Random.Range(0, availableEvents.Count); 
            activeEvent = availableEvents[index];
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
