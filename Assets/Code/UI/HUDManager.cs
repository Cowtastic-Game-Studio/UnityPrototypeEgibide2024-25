using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{

    /// <summary>
    /// Clase que gestiona interfaz grafica del jugador
    /// </summary>
    public class HUDManager : MonoBehaviour
    {
        //TODO: Hacer un checkeo en el start, para comprobar que todas las propiedades externas esten asignadas. si no lanzar excepcion
        //TODO: Cada fase como singleton?
        //TODO: Añadir visor de turno actual(dia)


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

        [SerializeField] private GameObject marketGUI;


        /// <summary>
        /// Referencia al GamePhaseManager
        /// </summary>
        private GamePhaseManager gamePhaseManager;

        #endregion

        #region Unity methods
        private void Start()
        {
            this.gamePhaseManager = GameManager.Instance.GamePhaseManager;
            UpdateGUI(this.gamePhaseManager.CurrentPhase);  
        }

        #endregion

        #region Events Handlers

        /// <summary>
        /// Evento click lanzado por el boton NextPhaseButton
        /// </summary>
        public void OnNextPhaseButtonClick()
        {
            this.gamePhaseManager.NextPhase();
            UpdateGUI(this.gamePhaseManager.CurrentPhase);
        }

        /// <summary>
        /// Evento click lanzado por el boton MulliganButton
        /// </summary>
        public void OnMulliganButtonClick()
        {
            GameManager.Instance.Tabletop.CardManager.Mulligan();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Actualiza la GUI
        /// </summary>
        /// <param name="currentPhase"></param>
        private void UpdateGUI(IGamePhase currentPhase)
        {
            //Dependiendo de la fase  modifica la GUI
            if (currentPhase is SetUpPhase)
            {
                HideActionPointsPanel();
                HideMulliganButton();
                HideMarket();
            }
            else if (currentPhase is StartDayPhase)
            {
                ShowMulliganButton();
                HideMarket();
            }
            else if (currentPhase is PlaceCardsPhase)
            {
                HideMulliganButton();
            }
            else if (currentPhase is ActionPointsPhase)
            {
                ShowActionPointsPanel();
            }
            else if (currentPhase is MarketPhase)
            {
                HideActionPointsPanel();
                ShowMarket();
            }

            //Actualiza el texto de la fase
            UpdatePhaseText(currentPhase);
        }

        /// <summary>
        /// Metodo que actualiza el texto de la fase actual 
        /// </summary>
        /// <param name="phase">Texto que indica la fase actual</param>
        private void UpdatePhaseText(IGamePhase currentPhase)
        {
            string phaseName;

            phaseName = currentPhase.GetType().Name;

            // Agregar un espacio antes de cada letra mayúscula, excepto la primera
            phaseName = Regex.Replace(phaseName, "(?<!^)([A-Z])", " $1");

            currentPhaseTextUI.text = phaseName;
        }

        /// <summary>
        /// Metodo que actualiza el texto de los puntos de accion
        /// </summary>
        /// <param name="points">Cantidad de los puntos de accion actuales</param>
        private void UpdateActionPoints(int points)
        {
            actionPointTextUI.text = points.ToString() + " PA";
        }

        /// <summary>
        /// Oculta el boton del mulligan
        /// </summary>
        private void HideMulliganButton()
        {
            mulliganButton.SetActive(false);
        }

        /// <summary>
        /// Muestra el boton del mulligan
        /// </summary>
        private void ShowMulliganButton()
        {
            mulliganButton.SetActive(true);
        }

        /// <summary>
        /// Oculta el panel de puntos de accion
        /// </summary>
        private void HideActionPointsPanel()
        {
            actionPointsPanel.SetActive(false);
        }

        /// <summary>
        /// Muestra el panel de puntos de accion
        /// </summary>
        private void ShowActionPointsPanel()
        {
            actionPointsPanel.SetActive(true);
        }

        private void ShowMarket()
        {
            marketGUI.SetActive(true);
        }

        private void HideMarket()
        {
            marketGUI.SetActive(false);
        }

        #endregion

    }

}