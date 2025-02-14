using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
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

        public bool isActivatedCheatCodes = false;
        public int rentAdd = 3;

        //private void Awake()
        //{
        //    if (Instance != null && Instance != this)
        //    {
        //        Destroy(gameObject);
        //        return;
        //    }

        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //    InitializeManagers();
        //}

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // Evitar duplicados en la misma escena
                return;
            }

            InitializeManagers();
        }

        private void InitializeManagers()
        {
            GamePhaseManager = new GamePhaseManager();
            GameCalendar = new GameCalendar();
            //addCalendarEvents();
            Tabletop.FindPlaces();
        }

        private void Update()
        {
            GamePhaseManager?.Update();

            #region CheatCodes

            if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.C)))
            {
                isActivatedCheatCodes = !isActivatedCheatCodes;
            }

            if (isActivatedCheatCodes)
            {

                if (Input.GetKeyDown(KeyCode.N))
                {
                    GameCalendar.NextDay();
                    Debug.Log("NextDay: " + GameCalendar.CurrentDay);
                }

                if (Input.GetKeyDown(KeyCode.P))
                {
                    GamePhaseManager.NextPhase();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Tabletop.CardManager.DrawFromDeck();
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    Tabletop.CardManager.Mulligan();
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    Tabletop.CardManager.ShuffleDiscardDeck();
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    Tabletop.FarmsActivateZone(0);
                    Tabletop.StablesActivateZone(0);
                    Tabletop.TavernActivateZone(0);
                }

                if (Input.GetKeyDown(KeyCode.V))
                {
                    Reward reward = RewardGenerator.CreateTutorialReward();
                    reward.Receive();
                }
            }

            #endregion
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
