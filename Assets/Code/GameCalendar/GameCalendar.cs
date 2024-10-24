namespace CowtasticGameStudio.MuuliciousHarvest
{
    // Enum que representa los días de la semana
    public enum DayOfWeek
    {
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6,
        Domingo = 7
    }

    public class GameCalendar
    {
        public int CurrentDay { get; private set; }
        public int CurrentWeek { get; private set; }

        private GameCalendarEventManager eventManager;

        // Día aleatorio para el evento de la semana actual
        private int eventDayOfWeek;

        // Propiedad para obtener el día de la semana (1 = Lunes, 7 = Domingo)
        public DayOfWeek DayOfWeek
        {
            get { return (DayOfWeek)((CurrentDay - 1) % 7 + 1); }
        }

        public GameCalendar()
        {
            CurrentDay = 1;
            CurrentWeek = 0;
            eventManager = new GameCalendarEventManager();
            // Inicialmente no hay evento
            eventDayOfWeek = -1;
        }

        // Avanzar al siguiente día
        public void NextDay()
        {
            CurrentDay++;

            //Comprobar si hay que activar un evento
            CheckForEvent();
        }

        // Comprobar si debe ocurrir un evento
        public void CheckForEvent()
        {
            // Al final de cada semana (múltiplo de 7) + Evitar la primera semana
            if (CurrentDay % 7 == 1 && CurrentDay > 7)
            {
                CurrentWeek++;
                // Asignar un día aleatorio dentro de la nueva semana para que ocurra un evento
                eventDayOfWeek = new System.Random().Next(1, 8);
            }

            // Comprobar si el día actual es el día del evento
            if (CurrentDay % 7 == eventDayOfWeek % 7)
            {
                // Dispara un evento aleatorio
                eventManager.TriggerRandomEvent();
                // Resetear el evento de la semana actual
                eventDayOfWeek = -1;
            }
        }

        // Agregar eventos al calendario
        public void AddCalendarEvent(CalendarEvent calendarEvent)
        {
            eventManager.AddEvent(calendarEvent);
        }

        // Método para obtener el nombre del día de la semana 
        public string GetDayOfWeekName()
        {
            return DayOfWeek.ToString();
        }
    }
}
