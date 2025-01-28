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
        public event Action<ICard> OnCardClickedGlobal;
        public event Action<Transform> OnPlaceSpaceClickedGlobal;

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

            if (Input.GetKeyDown(KeyCode.P))
            {
                GamePhaseManager.NextPhase();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                // Selecciona una carta
                if (Tabletop.CardManager.handDeck.Cards.Count > 0)
                {
                    // Selecciona la primera carta de la mano
                    GameObject selectedCard = Tabletop.CardManager.handDeck.Peek();

                    // Obtener el componente ICard de la carta seleccionada
                    ICard cardComponent = selectedCard.GetComponent<ICard>();

                    if (cardComponent != null)
                    {
                        // Aquí puedes hacer algo con el componente ICard
                        Debug.Log("Se ha seleccionado la carta: " + cardComponent);
                        CardClicked(cardComponent); // O invoca el evento de clic de carta
                    }
                    else
                    {
                        Debug.LogWarning("La carta seleccionada no tiene el componente ICard.");
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Tabletop.CardManager.DrawFromDeck();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                Tabletop.CardManager.Mulligan();
            }


            if (Input.GetKeyDown(KeyCode.S))
            {
                Tabletop.CardManager.ShuffleDiscardDeck();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameCalendar.TestEvent();

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameCalendar.TestStopEvent();

            }

            #endregion
        }

        private void addCalendarEvents()
        {
            // Eventos
            GameCalendar.AddCalendarEvent(new HarvestDayEvent(), false);
            GameCalendar.AddCalendarEvent(new PlagueEvent(), false);
            GameCalendar.AddCalendarEvent(new BrokenFridgeEvent(), false);
            GameCalendar.AddCalendarEvent(new HarvestDayEvent(), false);
        }

        // Metodo para invocar el evento de clic de carta
        public void CardClicked(ICard card)
        {
            OnCardClickedGlobal?.Invoke(card);
        }

        // Metodo para invocar el evento de clic de carta
        public void PlaceSpaceClicked(Transform transform)
        {
            OnPlaceSpaceClickedGlobal?.Invoke(transform);
        }
    }
}
