using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Tooltip : MonoBehaviour
    {

        public GameObject storagePanel;
        public GameObject cardTooltip;
        public GameObject cardTooltipInterior;
        public GameObject cardTooltipExterior;
        private GameObject selectedCard;

        public CameraGestor cameraGestor;


        private float hoverTime = 1.5f; // Hover time threshold
        private float storageHoverCounter = 0f;
        private float cardHoverCounter = 0f;

        private bool isHoveringCard = false;
        private GameObject currentHoveredCard = null;
        private bool forceResourcesPanelVisible = false;


        void Start()
        {
            storagePanel.SetActive(false);
            cardTooltip.SetActive(false);
            cardTooltipExterior.SetActive(false);
            cardTooltipInterior.SetActive(false);
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HandleCardHover(hit);
            }

            if (forceResourcesPanelVisible)
            {
                if (Physics.Raycast(ray, out RaycastHit hitt))
                {
                    HandleStorageHover(hitt);
                }
                else
                {
                    // storagePanel.SetActive(false);
                    ResetCardHover();
                }
            }
        }


        public void ForceResourcesPanel(bool state)
        {
            forceResourcesPanelVisible = state;
            storagePanel.SetActive(state); // Si el estado cambia, aplicarlo
        }

        private void HandleStorageHover(RaycastHit hit)
        {
            if (forceResourcesPanelVisible)
            {

                if (hit.collider.CompareTag("Storage") || GameManager.Instance.GamePhaseManager.CurrentPhase is ActionPointsPhase)
                {
                    storagePanel.SetActive(true);
                }
                else
                {
                    storagePanel.SetActive(false);
                }
            }
        }


        private void HandleCardHover(RaycastHit hit)
        {
            if (GameManager.Instance.GamePhaseManager.CurrentPhaseType != GamePhaseTypes.Market)
            {
                if (hit.collider.CompareTag("Carta"))
                {
                    if (currentHoveredCard != hit.collider.gameObject)
                    {
                        isHoveringCard = true;
                        cardHoverCounter = 0f;
                        currentHoveredCard = hit.collider.gameObject;
                    }

                    cardHoverCounter += Time.deltaTime;

                    if (cardHoverCounter >= hoverTime)
                    {
                        selectedCard = currentHoveredCard;
                        CardBehaviour selectedCardBehaviour = selectedCard.GetComponent<CardBehaviour>();

                        // Determine the appropriate tooltip display based on active camera
                        CardDisplay cardToolDisplay = GetActiveTooltip();
                        if (cardToolDisplay != null)
                        {
                            cardToolDisplay.UpdateDisplay(selectedCardBehaviour.GetTemplate(), true, selectedCardBehaviour.LifeCycleDaysRemaining);
                            cardToolDisplay.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    ResetCardHover();
                }
            }
        }

        private CardDisplay GetActiveTooltip()
        {
            if (cameraGestor.IsMainCameraActive())
                return cardTooltip.GetComponent<CardDisplay>();
            if (cameraGestor.IsExteriorCameraActive())
                return cardTooltipExterior.GetComponent<CardDisplay>();
            if (cameraGestor.IsInteriorCameraActive())
                return cardTooltipInterior.GetComponent<CardDisplay>();

            return null; // No active tooltip
        }


        private void ResetCardHover()
        {
            isHoveringCard = false;
            cardHoverCounter = 0f;
            currentHoveredCard = null;
            selectedCard = null;
            cardTooltip.SetActive(false);
            cardTooltipExterior.SetActive(false);
            cardTooltipInterior.SetActive(false);
        }


    }
}
