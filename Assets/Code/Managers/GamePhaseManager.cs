using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GamePhaseManager: MonoBehaviour
    {
        private IGamePhase currentPhase;
        [SerializeField] private TMP_Text currentPhaseTextUI;
        [SerializeField] private TMP_Text actionPointTextUI;
        [SerializeField] private GameObject mulliganButton;
        [SerializeField] private GameObject actionPointsPanel;

        public void Start()
        {
            SetPhase(new StartDayPhase());
        }

        public void SetPhase(IGamePhase newPhase)
        {
            if (currentPhase != null)
            {
                currentPhase.EndPhase();
            }
            currentPhase = newPhase;
            currentPhase.EnterPhase();
            HideAPPanel();
            UpdateCurrentPhase("Start day phase");
        }

        public void NextPhase()
        {
            Debug.Log("enter next phase");
            if (currentPhase is StartDayPhase)
            {
                Debug.Log("estas en la fase de PlaceCardsPhase");
                SetPhase(new PlaceCardsPhase());
                HideMulliganButton();
                UpdateCurrentPhase("Place cards phase");
            }
            else if (currentPhase is PlaceCardsPhase)
            {
                Debug.Log("estas en la fase de ActionPointsPhase");
                SetPhase(new ActionPointsPhase());
                ShowAPPanel();
                UpdateCurrentPhase("Action points phase");
            }
            else if (currentPhase is ActionPointsPhase)
            {
                Debug.Log("estas en la fase de MarketPhase");
                SetPhase(new MarketPhase());
                HideAPPanel();
                UpdateCurrentPhase("Market phase");
            }
            else if (currentPhase is MarketPhase)
            {
                Debug.Log("estas en la fase de StartDayPhase");
                SetPhase(new StartDayPhase()); // Comienza un nuevo d�a
                ShowMulliganButton();
                UpdateCurrentPhase("Start day phase");
            }
        }

        public void Update()
        {
            if (currentPhase != null)
            {
                currentPhase.ExecutePhase();
            }
        }

        #region Metodos publicos de la clase

        /// <summary>
        /// Metodo que actualiza el texto de la fase actual 
        /// </summary>
        /// <param name="phase">Texto que indica la fase actual</param>
        public void UpdateCurrentPhase(string phase)
        {
            currentPhaseTextUI.text = phase;
        }

        /// <summary>
        /// Metodo que actualiza el texto de los puntos de acciones 
        /// </summary>
        /// <param name="points">Cantidad de los puntos de accion actuales</param>
        public void UpdateActionPoints(int points)
        {
            actionPointTextUI.text = points.ToString() + " AP";
        }

        public void NextPhaseButton()
        {
            NextPhase();
        }

        public void HideMulliganButton()
        {
            mulliganButton.SetActive(false);
        }

        public void ShowMulliganButton()
        {
            mulliganButton.SetActive(true);
        }

        public void HideAPPanel()
        {
            actionPointsPanel.SetActive(false);
        }

        public void ShowAPPanel()
        {
            actionPointsPanel.SetActive(true);
        }

        #endregion
    }
}