public class GameCalendar
{
    private int currentDay;
    private int currentWeek;
    private GameCalendarEventManager eventManager;

    public GameCalendar()
    {
        currentDay = 0;
        currentWeek = 0;
        eventManager = new GameCalendarEventManager();
    }

    // Avanzar al siguiente día
    public void NextDay()
    {
        currentDay++;
        // Al final de cada semana
        if (currentDay % 7 == 0) 
        {
            currentWeek++;
            CheckForEvent();
        }
    }

    // Comprobar si debe ocurrir un evento
    public void CheckForEvent()
    {
        // Dispara un evento aleatorio
        eventManager.TriggerRandomEvent();
    }

    // Agregar eventos al calendario
    public void AddCalendarEvent(CalendarEvent calendarEvent)
    {
        eventManager.AddEvent(calendarEvent);
    }
}
