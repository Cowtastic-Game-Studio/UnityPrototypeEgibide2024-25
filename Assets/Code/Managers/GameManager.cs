using System;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GamePhaseManager GamePhaseManager { get; private set; }
        public GameCalendar GameCalendar { get; private set; }

        public Tabletop Tabletop;

        // Evento global para manejar clics en cartas
        public event Action<GameObject> OnCardClickedGlobal;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }

        private void InitializeManagers()
        {
            GamePhaseManager = new GamePhaseManager();
            GameCalendar = new GameCalendar();
            addCalendarEvents();
        }

        private void Update()
        {
            GamePhaseManager?.Update();

            #region CheatCodes
            if (Input.GetKeyDown(KeyCode.N))
            {
                GameCalendar.NextDay();
                Debug.Log("NextDay: " + GameCalendar.CurrentDay);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ICard card = null;
                Tabletop.OnCardUseActionPoints(card);
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

        // Método para invocar el evento de clic de carta
        public void CardClicked(GameObject cardGameObject)
        {
            OnCardClickedGlobal?.Invoke(cardGameObject);
        }
    }
}
