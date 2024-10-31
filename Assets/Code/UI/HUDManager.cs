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
        //TODO: Añadir descripcion de la fase en cada fase para obtenerla de la propia clase
        //TODO: Cada fase como singleton?

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
            //TODO: Añadir llamada a Mulligan
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Actualiza la GUI
        /// </summary>
        /// <param name="currentPhase"></param>
        private void UpdateGUI(IGamePhase currentPhase)
        {
            if (currentPhase is StartDayPhase)
            {
                HideMulliganButton();
                //UpdateCurrentPhase("Place cards phase");
            }
            else if (currentPhase is PlaceCardsPhase)
            {
                ShowActionPointsPanel();
                //UpdateCurrentPhase("Action points phase");
            }
            else if (currentPhase is ActionPointsPhase)
            {
                HideActionPointsPanel();
                //UpdateCurrentPhase("Market phase");
            }
            else if (currentPhase is MarketPhase)
            {
                ShowMulliganButton();
                //UpdateCurrentPhase("Start day phase");
            }

            string phaseName = currentPhase.GetType().Name;
            // Agregar un espacio antes de cada letra mayúscula, excepto la primera
            phaseName = Regex.Replace(phaseName, "(?<!^)([A-Z])", " $1");

            UpdateCurrentPhase(phaseName);
        }

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