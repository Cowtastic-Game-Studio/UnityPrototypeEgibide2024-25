using UnityEngine;


namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GamePhaseManager GamePhaseManager { get; private set; }
        public GameCalendar GameCalendar { get; private set; }

        public Tabletop Tabletop { get; private set; }

        // Puedes añadir más gestores aquí (AudioManager, UIManager, etc.)

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            // Para mantenerlo entre escenas, creo que no sera necesario !!!
            DontDestroyOnLoad(gameObject);

            InitializeManagers();
        }

        private void InitializeManagers()
        {
            // Instanciar cualquier otro sistema que quieras manejar desde aquí
            GamePhaseManager = new GamePhaseManager();
            GameCalendar = new GameCalendar();
            // Instanciar otros managers si es necesario

            addCalendarEvents();
        }

        private void Update()
        {
            // Puedes delegar el Update a los distintos sistemas si es necesario
            if (GamePhaseManager != null)
            {
                GamePhaseManager.Update();
            }

            #region CheatCodes
            if (Input.GetKeyDown(KeyCode.N))
            {
                GameCalendar.NextDay();
                Debug.Log("NextDay: " + GameCalendar.CurrentDay);
            }
            #endregion
        }

        private void addCalendarEvents()
        {
            GameCalendar.AddCalendarEvent(new HarvestDayEvent());
            GameCalendar.AddCalendarEvent(new CowDayEvent());
            GameCalendar.AddCalendarEvent(new PlagueEvent());
            GameCalendar.AddCalendarEvent(new BrokenFridgeEvent());
        }
    }
}