using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{

    /// <summary>
    /// Clase que gestiona interfaz grafica del jugador
    /// </summary>
    public class GUIManager : MonoBehaviour
    {
        //TODO: Hacer un checkeo en el start, para comprobar que todas las propiedades externas esten asignadas. si no lanzar excepcion

        #region Properties

        /// <summary>
        /// Referencia al TextBox donde se muestra el texto de la fase actual
        /// </summary>
        [SerializeField] private TMP_Text currentPhaseTextUI;

        /// <summary>
        /// Referencia al TextBox donde se muestra el numero de puntos de accion
        /// </summary>
        [SerializeField] private TMP_Text actionPointTextUI;

        /// <summary>
        /// Referencia al Button de Mulligan
        /// </summary>
        [SerializeField] private GameObject mulliganButton;

        /// <summary>
        /// Referencua el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject actionPointsPanel;

        /// <summary>
        /// Referencia al GamePhaseManager
        /// </summary>

        [SerializeField] private GamePhaseManager gamePhaseManager;

        #endregion

        private void Start()
        {
        }


        #region Events Handlers

        /// <summary>
        /// Evento click lanzado por el boton NextPhaseButton
        /// </summary>
        public void OnNextPhaseButtonClick()
        {
            this.gamePhaseManager.NextPhase();

            if (this.gamePhaseManager.CurrentPhase is StartDayPhase)
            {
                HideMulliganButton();
                UpdateCurrentPhase("Place cards phase");
            }
            else if (this.gamePhaseManager.CurrentPhase is PlaceCardsPhase)
            {
                ShowActionPointsPanel();
                UpdateCurrentPhase("Action points phase");
            }
            else if (this.gamePhaseManager.CurrentPhase is ActionPointsPhase)
            {
                HideActionPointsPanel();
                UpdateCurrentPhase("Market phase");
            }
            else if (this.gamePhaseManager.CurrentPhase is MarketPhase)
            {
                ShowMulliganButton();
                UpdateCurrentPhase("Start day phase");
            }

        }

        /// <summary>
        /// Evento click lanzado por el boton MulliganButton
        /// </summary>
        public void OnMulliganButtonClick()
        {
            //TODO: Añadir llamada a Mulligan
        }

        #endregion

        #region Private methods

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
            actionPointTextUI.text = points.ToString() + " PA";
        }

        public void HideMulliganButton()
        {
            mulliganButton.SetActive(false);
        }

        public void ShowMulliganButton()
        {
            mulliganButton.SetActive(true);
        }

        public void HideActionPointsPanel()
        {
            actionPointsPanel.SetActive(false);
        }

        public void ShowActionPointsPanel()
        {
            actionPointsPanel.SetActive(true);
        }

        #endregion

    }

}