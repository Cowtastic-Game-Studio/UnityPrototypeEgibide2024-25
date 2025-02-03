using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

        private GameCalendarEventManager eventManager;

        // D�a aleatorio para el evento de la semana actual
        private int eventDayOfWeek;

        private GameObject calendarMark;
        private Vector3[] positions;
        private int positionCount;

        // Propiedad para obtener el d�a de la semana (1 = Lunes, 7 = Domingo)
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
            calendarMark = GameObject.Find("MarkDay");
            eventDayOfWeek = -1;
            setMarkArray();
        }

        /// <summary>
        /// Genera un array con la posicion de cada dia del calendario
        /// </summary>
        private void setMarkArray()
        {
            positionCount = 1;
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

            CurrentDay++;

            ChangeCallendar();

            //Comprobar si hay que activar un evento
            CheckForEvent();
        }

        // Comprobar si debe ocurrir un evento
        public void CheckForEvent()
        {
            eventManager.TriggerDailyEvent(CurrentDay);

            // Al final de cada semana (m�ltiplo de 7) + Evitar la primera semana
            //if (CurrentDay % 7 == 1 && CurrentDay > 7)
            //{
            //    CurrentWeek++;
            //    // Asignar un d�a aleatorio dentro de la nueva semana para que ocurra un evento
            //    eventDayOfWeek = new System.Random().Next(1, 8);
            //}

            //// Comprobar si el d�a actual es el d�a del evento
            //if (CurrentDay % 7 == eventDayOfWeek % 7)
            //{
            //    // Dispara un evento aleatorio
            //    eventManager.TriggerRandomEvent();
            //    // Resetear el evento de la semana actual
            //    eventDayOfWeek = -1;
            //}
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
            calendarMark.transform.localPosition = positions[positionCount];
            positionCount++;
            if(CurrentDay%28 == 0)
            {
                positionCount = 0;
            }
        }
    }
}
