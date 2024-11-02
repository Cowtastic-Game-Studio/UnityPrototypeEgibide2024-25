using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GamePhaseManager GamePhaseManager { get; private set; }
        public GameCalendar GameCalendar { get; private set; }

        public Tabletop Tabletop;

        // Evento global para manejar clics en cartas
        public event Action<ICard> OnCardClickedGlobal;

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
                // Selecciona una carta; aqu� puedes definir una l�gica para elegir una carta espec�fica

                CardClicked(Tabletop.CardManager.HandDeck[0]); // Invoca el evento de clic de carta


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

        // M�todo para invocar el evento de clic de carta
        public void CardClicked(ICard card)
        {
            OnCardClickedGlobal?.Invoke(card);
        }
    }
}
