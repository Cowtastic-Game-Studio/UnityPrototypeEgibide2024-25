using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest.Managers
{
    /// <summary>
    /// Clase que gestiona el game loop principal
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Properties

        public static GameManager Instance { get; private set; }

        public GamePhaseManager GamePhaseManager;

        public GameCalendar GameCalendar { get; private set; }

        //TODO: Puedes añadir más gestores aquí (AudioManager, UIManager, etc.)

        #endregion

        #region Unity methods

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

        #endregion

        #region Private methods

        /// <summary>
        /// Inicializa los gestores auxiliares
        /// </summary>
        private void InitializeManagers()
        {
            // Instanciar cualquier otro sistema que quieras manejar desde aquí
            GamePhaseManager = new GamePhaseManager();
            GameCalendar = new GameCalendar();

            //TODO: Instanciar otros managers si es necesario

            AddCalendarEvents();
        }

        /// <summary>
        /// Añade eventos al calendario
        /// </summary>
        private void AddCalendarEvents()
        {
            GameCalendar.AddCalendarEvent(new HarvestDayEvent());
            GameCalendar.AddCalendarEvent(new CowDayEvent());
            GameCalendar.AddCalendarEvent(new PlagueEvent());
            GameCalendar.AddCalendarEvent(new BrokenFridgeEvent());
        }

        #endregion


    }
}