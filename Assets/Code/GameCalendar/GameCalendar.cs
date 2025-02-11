using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    // Enum que representa los d�as de la semana
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
        public int CurrentMonth { get; private set; }
        private int DayOfMonth; //{ get; private set; }
        public int CurrentYear { get; private set; }

        public UnityEvent<int> DayChanged = new UnityEvent<int>();

        private GameCalendarEventManager eventManager;

        private GameObject calendarMark;
        private Vector3[] positions;
        private bool isFirstWeek = true;

        // Propiedad para obtener el d�a de la semana (1 = Lunes, 7 = Domingo)
        public DayOfWeek DayOfWeek
        {
            get { return (DayOfWeek)((CurrentDay - 1) % 7 + 1); }
        }

        public GameCalendar()
        {
            CurrentDay = 0;
            CurrentWeek = 0;
            DayOfMonth = 0;
            eventManager = new GameCalendarEventManager();
            // Inicialmente no hay evento
            calendarMark = GameObject.Find("MarkDay");
            setMarkArray();
        }

        /// <summary>
        /// Genera un array con la posicion de cada dia del calendario
        /// </summary>
        private void setMarkArray()
        {
            positions = new Vector3[28];
            positions[0] = new Vector3(0.005f, 3.535f, -6.293f);
            positions[1] = new Vector3(0.005f, 3.535f, -4.267f);
            positions[2] = new Vector3(0.005f, 3.535f, -2.112f);
            positions[3] = new Vector3(0.005f, 3.535f, 0.049f);
            positions[4] = new Vector3(0.005f, 3.535f, 2.141f);
            positions[5] = new Vector3(0.005f, 3.535f, 4.139f);
            positions[6] = new Vector3(0.005f, 3.535f, 6.322f);
            positions[7] = new Vector3(0.005f, 1.16f, -6.293f);
            positions[8] = new Vector3(0.005f, 1.16f, -4.267f);
            positions[9] = new Vector3(0.005f, 1.16f, -2.112f);
            positions[10] = new Vector3(0.005f, 1.16f, 0.049f);
            positions[11] = new Vector3(0.005f, 1.16f, 2.141f);
            positions[12] = new Vector3(0.005f, 1.16f, 4.139f);
            positions[13] = new Vector3(0.005f, 1.16f, 6.322f);
            positions[14] = new Vector3(0.005f, -1.25f, -6.293f);
            positions[15] = new Vector3(0.005f, -1.25f, -4.267f);
            positions[16] = new Vector3(0.005f, -1.25f, -2.112f);
            positions[17] = new Vector3(0.005f, -1.25f, 0.049f);
            positions[18] = new Vector3(0.005f, -1.25f, 2.141f);
            positions[19] = new Vector3(0.005f, -1.25f, 4.139f);
            positions[20] = new Vector3(0.005f, -1.25f, 6.322f);
            positions[21] = new Vector3(0.005f, -3.61f, -6.293f);
            positions[22] = new Vector3(0.005f, -3.61f, -4.267f);
            positions[23] = new Vector3(0.005f, -3.61f, -2.112f);
            positions[24] = new Vector3(0.005f, -3.61f, 0.049f);
            positions[25] = new Vector3(0.005f, -3.61f, 2.141f);
            positions[26] = new Vector3(0.005f, -3.61f, 4.139f);
            positions[27] = new Vector3(0.005f, -3.61f, 6.322f);
        }

        // Avanzar al siguiente d�a
        public void NextDay()
        {
            eventManager.EndActiveEvent();

            DayOfMonth++;

            if (CurrentDay % 7 == 0)
            {
                CurrentWeek++;

                if (CurrentWeek > 1)
                {
                    MissionsManager.Instance.RenewWeeklyMission();
                }
            }
            if (DayOfMonth % 29 == 0) // 29 seria principio de mes
            {
                DayOfMonth = 1;
                CurrentMonth++;
            }
            if (CurrentMonth % 4 == 0) // El año tiene 4 meses (estaciones)
            {
                CurrentYear++;
            }

            CurrentDay++;

            //Debug.Log("CurrentDay: " + CurrentDay + " CurrentWeek: " + CurrentWeek);
            //Debug.Log("DayOfMonth: " + DayOfMonth);

            ChangeCallendar();

            //Comprobar si hay que activar un evento
            CheckForEvent();

            RaiseDayChanged();
        }

        // Comprobar si debe ocurrir un evento
        public void CheckForEvent()
        {
            eventManager.TriggerDailyEvent(DayOfMonth, CurrentWeek);
        }

        // M�todo para obtener el nombre del d�a de la semana 
        public string GetDayOfWeekName()
        {
            return DayOfWeek.ToString();
        }

        /// <summary>
        /// Funcion que hace avanzar el array cambiando la posicion de la marca del dia y lo reinicia si llega al untimo dia del mes
        /// </summary>
        public void ChangeCallendar()
        {
            calendarMark.transform.localPosition = positions[DayOfMonth - 1];
        }

        private void RaiseDayChanged()
        {
            this.DayChanged.Invoke(this.CurrentDay);
        }
    }
}
