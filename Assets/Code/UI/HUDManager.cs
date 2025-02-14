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

        [SerializeField] private ButtonSoundManager buttonSoundManager;

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
        [SerializeField] private GameObject discardBtn;

        /// <summary>
        /// Referencia al GamePhaseManager
        /// </summary>
        private GamePhaseManager gamePhaseManager;
        private CardManager CardManager;
        [SerializeField] private Tooltip tooltip;
        [SerializeField] private CameraGestor cameraGestor;

        #endregion

        #region Unity methods
        private void Start()
        {
            this.gamePhaseManager = GameManager.Instance.GamePhaseManager;
            this.CardManager = GameManager.Instance.Tabletop.CardManager;
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
            buttonSoundManager?.PhaseAudioPlayer();
            GameManager.Instance.GamePhaseManager.NextPhase();
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
            bankResourceTextUI.text = FormatNumber(muuney) + "/" + FormatNumber(muuneyMax);
        }
        public void UpdateHUDForCamera(CinemachineVirtualCamera activeCamera)
        {
            // Ocultar todo por defecto
            catButton.gameObject.SetActive(false);
            savePanel.gameObject.SetActive(false);
            exitPanel.gameObject.SetActive(false);
            //resourcesPanel.gameObject.SetActive(false); // Asegurar que se oculta por defecto

            if (activeCamera.gameObject.name == "VirtualCameraIdle")
            {
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(true);
                CardManager.showHand();
                tooltip.ForceResourcesPanel(true);

                if (GameManager.Instance.GamePhaseManager.CurrentPhase is ActionPointsPhase)
                {
                    resourcesPanel.gameObject.SetActive(true);
                    tooltip.ForceResourcesPanel(false);
                }

                if (GameManager.Instance.GamePhaseManager.CurrentPhase is MarketPhase)
                {
                    discardBtn.SetActive(true);
                }
                if (GameManager.Instance.GamePhaseManager.CurrentPhase is StartDayPhase)
                {
                    ShowMulliganButton();
                }
            }

            else if (activeCamera.gameObject.name == "VirtualCameraDerecha")
            {
                resourcesPanel.gameObject.SetActive(false);
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
                CardManager.hideHand();
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();
                if (GameManager.Instance.GamePhaseManager.CurrentPhase is MarketPhase)
                {
                    discardBtn.SetActive(true);
                }
            }
            else if (activeCamera.gameObject.name == "VirtualCameraAtras")
            {
                resourcesPanel.gameObject.SetActive(false);
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(false);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(true);
                savePanel.gameObject.SetActive(true);
                discardBtn.gameObject.SetActive(false);
                CardManager.hideHand();
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();


            }
            else if (activeCamera.gameObject.name == "VirtualCameraIzquierda")
            {
                resourcesPanel.gameObject.SetActive(false);
                currentPhasePanel.gameObject.SetActive(false);
                muuneyPanel.gameObject.SetActive(false);
                actionPointsPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(true);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
                discardBtn.gameObject.SetActive(false);
                CardManager.hideHand();
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();


            }
            else if (activeCamera.gameObject.name == "VirtualCameraPared")
            {
                currentPhasePanel.gameObject.SetActive(true);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(true);
                resourcesPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
                discardBtn.gameObject.SetActive(false);
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();


            }
            else if (activeCamera.gameObject.name == "VirtualCameraInterior")
            {
                currentPhasePanel.gameObject.SetActive(false);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(true);
                resourcesPanel.gameObject.SetActive(false);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
                discardBtn.gameObject.SetActive(false);
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();


            }
            else if (activeCamera.gameObject.name == "VirtualCameraExterior")
            {
                currentPhasePanel.gameObject.SetActive(false);
                muuneyPanel.gameObject.SetActive(true);
                actionPointsPanel.gameObject.SetActive(true);
                exitPanel.gameObject.SetActive(false);
                catButton.gameObject.SetActive(false);
                savePanel.gameObject.SetActive(false);
                discardBtn.gameObject.SetActive(false);
                tooltip.ForceResourcesPanel(false);
                HideMulliganButton();


            }
            //UpdateGUI(GameManager.Instance.GamePhaseManager.CurrentPhase);
        }

        public void HideHUD()
        {
            //tooltip.ForceResourcesPanel(false);
            actionPointsPanel.gameObject.SetActive(false);
            currentPhasePanel.gameObject.SetActive(false);
            muuneyPanel.gameObject.SetActive(false);
            catButton.gameObject.SetActive(false);
            savePanel.gameObject.SetActive(false);
            exitPanel.gameObject.SetActive(false);
            mulliganButton.gameObject.SetActive(false);
            discardBtn.gameObject.SetActive(false);


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
                tooltip.ForceResourcesPanel(true);
                resourcesPanel.gameObject.SetActive(false);


            }
            else if (currentPhase is StartDayPhase)
            {
                ShowActionPointsPanel();
                ShowMulliganButton();
                HideMarket();
                tooltip.ForceResourcesPanel(true);
                resourcesPanel.gameObject.SetActive(false);

            }
            else if (currentPhase is PlaceCardsPhase)
            {
                HideMulliganButton();
                HideMarket();
                tooltip.ForceResourcesPanel(true);
                resourcesPanel.gameObject.SetActive(false);

            }
            else if (currentPhase is ActionPointsPhase)
            {
                if (cameraGestor.GetCurrentCamera().gameObject.name == "VirtualCameraIdle")
                {
                    resourcesPanel.gameObject.SetActive(true);

                }
                else
                {
                    resourcesPanel.gameObject.SetActive(false);
                }

                // tooltip.ForceResourcesPanel(false);

                HideMulliganButton();
                HideMarket();
            }
            else if (currentPhase is MarketPhase)
            {

                HideActionPointsPanel();
                ShowMarket();
                HideMulliganButton();
                tooltip.ForceResourcesPanel(true);
                resourcesPanel.gameObject.SetActive(false);



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
            //resourcesPanel.SetActive(false);

        }

        /// <summary>
        /// Muestra el panel de puntos de accion
        /// </summary>
        private void ShowActionPointsPanel()
        {
            actionPointsPanel.SetActive(true);
            //resourcesPanel.SetActive(false);
        }

        private void ShowMarket()
        {
            discardBtn.SetActive(true);
            pageGUI.SetActive(true);
            buttonsGUI.SetActive(true);
        }

        private void HideMarket()
        {
            discardBtn.SetActive(false);
            pageGUI.SetActive(false);
            buttonsGUI.SetActive(false);
        }


        public static string FormatNumber(int number)
        {
            if (number >= 10000000)
            {
                float millions = number / 1000000.0f;
                return Mathf.Floor(millions).ToString("0") + "M"; // Sin decimales
            }
            if (number >= 1000000)
            {
                float millions = number / 1000000.0f;
                return millions.ToString("0.0") + "M"; // Con un decimal
            }
            else if (number >= 10000)
            {
                float thousands = number / 1000.0f;
                return Mathf.Floor(thousands).ToString("0") + "K"; // Sin decimales
            }
            else if (number >= 1000)
            {
                float thousands = number / 1000.0f;
                return thousands.ToString("0.0") + "K"; // Con un decimal
            }
            return number.ToString();
        }

        #endregion
    }
}