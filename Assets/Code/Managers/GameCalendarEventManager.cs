using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GameCalendarEventManager
    {
        private Dictionary<int, CalendarEvent> staticEvents;
        private List<CalendarEvent> dynamicEvents;
        private CalendarEvent activeEvent;
        private System.Random random;

        private RentEvent rent = new RentEvent();
        private CalendarEvent rentEvent;

        public GameCalendarEventManager()
        {
            staticEvents = new Dictionary<int, CalendarEvent>();
            dynamicEvents = new List<CalendarEvent>();
            activeEvent = null;
            random = new System.Random();
            InitializeStaticEvents();
            InitializeDynamicEvents();
        }

        private void InitializeStaticEvents()
        {
            staticEvents[8] = new ResourceMultipleEvent("Día de la cosecha", "¡Los cultivos dan el doble de recursos!", GameResource.Cereal, 2);
            staticEvents[13] = new ResourceMultipleEvent("Día de las vacas", "¡Las vacas dan el doble de recursos!", GameResource.Milk, 2);
            staticEvents[18] = new VetDayEvent();
            staticEvents[21] = new ResourceMultipleEvent("Festival de la granja", "Vendes el doble de caro, misma calidad y nadie se queja", GameResource.Muuney, 2);
            staticEvents[28] = new BlackFridayEvent();
        }

        private void InitializeDynamicEvents()
        {
            AddDynamicEvent(new PlagueEvent());
            AddDynamicEvent(new BrokenFridgeEvent());
            AddDynamicEvent(new CivilWarEvent());
            AddDynamicEvent(new LuckStrike());
            AddDynamicEvent(new Heist());
            AddDynamicEvent(new NewMemberEvent());
        }

        public void TriggerDailyEvent(int currentDay, int currentWeek)
        {
            EndActiveEvent();

            if (staticEvents.ContainsKey(currentDay))
            {
                //Debug.LogWarning("Static event day: " + currentDay);
                activeEvent = staticEvents[currentDay];
                activeEvent.TriggerEvent();
            }
            else if (currentWeek > 0 && random.Next(100) < 20) // 20% chance
            {
                //Debug.LogWarning("random event day: " + currentDay);
                int index = random.Next(dynamicEvents.Count);
                activeEvent = dynamicEvents[index];
                activeEvent.TriggerEvent();
            }

            // Activar el evento de renta cada séptimo día de la semana
            if (currentDay % 7 == 0)
            {
                Debug.LogWarning("Rent day " + currentDay);
                rent.TriggerEvent();
                rentEvent = rent;
            }
        }

        public void AddDynamicEvent(CalendarEvent calendarEvent)
        {
            dynamicEvents.Add(calendarEvent);
        }

        public void EndActiveEvent()
        {
            activeEvent?.EndEvent();
            activeEvent = null;

            rentEvent?.EndEvent();
            rentEvent = null;
        }
    }
}
