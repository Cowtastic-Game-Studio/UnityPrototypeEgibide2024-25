using Cinemachine;
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
        //TODO: A�adir visor de turno actual(dia)


        #region Properties

        [SerializeField]private ButtonSoundManager buttonSoundManager; 

        /// <summary>
        /// Referencia al TextBox donde se muestra el texto de la fase actual
        /// </summary>
        [SerializeField] private TMP_Text currentPhaseTextUI;

        /// <summary>
        /// Referencia al TextBox donde se muestra el numero de puntos de accion
        /// </summary>
        [SerializeField] private TMP_Text actionPointTextUI;

        /// <summary>
        /// Referencia al TextBox donde se muestra el numero de puntos de accion
        /// </summary>
        [SerializeField] private TMP_Text wheatResourceTextUI;

        /// <summary>
        /// Referencia al TextBox donde se muestra el numero de puntos de accion
        /// </summary>
        [SerializeField] private TMP_Text milkResourceTextUI;

        /// <summary>
        /// Referencia al TextBox donde se muestra el numero de puntos de accion
        /// </summary>
        [SerializeField] private TMP_Text bankResourceTextUI;

        /// <summary>
        /// Referencia al Button de Mulligan
        /// </summary>
        [SerializeField] private GameObject mulliganButton;

        /// <summary>
        /// Referencia el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject actionPointsPanel;

        /// <summary>
        /// Referencia el panel de fase actual
        /// </summary>
        [SerializeField] private GameObject resourcesPanel;

        /// <summary>
        /// Referencia el panel de muuney
        /// </summary>
        [SerializeField] private GameObject muuneyPanel;

        /// <summary>
        /// Referencua el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject currentPhasePanel;
        /// <summary>
        /// Referencua el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject exitPanel;
        /// <summary>
        /// Referencua el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject catButton;
        /// <summary>
        /// Referencua el panel de puntos de accion
        /// </summary>
        [SerializeField] private GameObject savePanel;

        [SerializeField] private GameObject pageGUI;
        [SerializeField] private GameObject buttonsGUI;


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
            UpdateResources();
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
            buttonSoundManager.MulliganAudioPlayer();
            GameManager.Instance.Tabletop.CardManager.Mulligan();
            
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Oculta el boton del mulligan
        /// </summary>
        public void HideMulliganButton()
        {
            mulliganButton.SetActive(false);
        }

        /// <summary>
        /// Funcion que actualiza los recursos en la interfaz grafica.
        /// </summary>
        public void UpdateResources()
        {
            int paMax = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.ActionPoints);
            int wheatMax = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Cereal);
            int milkMax = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Milk);
            int muuneyMax = GameManager.Instance.Tabletop.StorageManager.GetMaxResourceAmounts(GameResource.Muuney);

            int pa = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.ActionPoints);
            int wheat = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Cereal);
            int milk = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Milk);
            int muuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);

            actionPointTextUI.text = pa.ToString() + "/" + paMax.ToString();
            wheatResourceTextUI.text = wheat.ToString() + "/" + wheatMax.ToString();
            milkResourceTextUI.text = milk.ToString() + "/" + milkMax.ToString();
            bankResourceTextUI.text = muuney.ToString() + "/" + muuneyMax.ToString();
        }

        public void UpdateHUDForCamera(CinemachineVirtualCamera activeCamera)
        {
            // Ocultar todo por defecto
            //currentPhaseTextUI.gameObject.SetActive(false);
            //bankResourceTextUI.gameObject.SetActive(false);
            catButton.gameObject.SetActive(false);
            savePanel.gameObject.SetActive(false);
            exitPanel.gameObject.SetActive(false);

            if (activeCamera.gameObject.name == "VirtualCameraIdle")
            {
                // Mostrar fase actual y dinero
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(true);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
            }
            else if (activeCamera.gameObject.name == "VirtualCameraDerecha")
            {
                // Mostrar fase actual y dinero
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);



            }
            else if (activeCamera.gameObject.name == "VirtualCameraAtras")
            {
                // Mostrar solo fase actual
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(false);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(true);
                savePanel.gameObject.SetActive(true);


            }
            else if (activeCamera.gameObject.name == "VirtualCameraIzquierda")
            {
                // Mostrar fase actual y dinero
                currentPhasePanel.gameObject.SetActive(false);
                muuneyPanel.gameObject.SetActive(false);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(true);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);

            }
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
                HideMulliganButton();
                HideMarket();
            }
            else if (currentPhase is StartDayPhase)
            {
                ShowActionPointsPanel();
                ShowMulliganButton();
                HideMarket();
            }
            else if (currentPhase is PlaceCardsPhase)
            {
                HideMulliganButton();
            }
            else if (currentPhase is ActionPointsPhase)
            {

            }
            else if (currentPhase is MarketPhase)
            {
                HideActionPointsPanel();
                ShowMarket();
            }

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

            // Agregar un espacio antes de cada letra may�scula, excepto la primera
            phaseName = Regex.Replace(phaseName, "(?<!^)([A-Z])", " $1");

            currentPhaseTextUI.text = phaseName;
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
            resourcesPanel.SetActive(false);

        }

        /// <summary>
        /// Muestra el panel de puntos de accion
        /// </summary>
        private void ShowActionPointsPanel()
        {
            actionPointsPanel.SetActive(true);
            resourcesPanel.SetActive(true);
        }

        private void ShowMarket()
        {
            pageGUI.SetActive(true);
            buttonsGUI.SetActive(true);
        }

        private void HideMarket()
        {
            pageGUI.SetActive(false);
            buttonsGUI.SetActive(false);
        }

        #endregion
    }
}