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
        private float cursorhover = 0.5f; // Hover time threshold
        private float storageHoverCounter = 0f;
        private float cardHoverCounter = 0f;

        private bool isHoveringCard = false;
        private GameObject currentHoveredCard = null;
        private bool forceResourcesPanelVisible = false;

        [SerializeField] private Texture2D cursorTooltip;
        [SerializeField] private Texture2D cursorNormal;


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
            //storagePanel.SetActive(state); // Si el estado cambia, aplicarlo
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
                    if (cardHoverCounter >= cursorhover)
                    {
                        Cursor.SetCursor(cursorTooltip, Vector2.zero, CursorMode.Auto);

                    }

                    if (cardHoverCounter >= hoverTime)
                    {
                        selectedCard = currentHoveredCard;
                        CardBehaviour selectedCardBehaviour = selectedCard.GetComponent<CardBehaviour>();

                        if (selectedCardBehaviour.IsPlaced && (selectedCardBehaviour.Type == CardType.PlaceActivator || selectedCardBehaviour.Type == CardType.PlaceMultiplier))
                        {
                            // Obtener el GameObject de la carta seleccionada
                            GameObject cardObject = selectedCardBehaviour.gameObject;

                            // Obtener los hermanos del GameObject de la carta
                            Transform[] siblings = cardObject.transform.parent.GetComponentsInChildren<Transform>();

                            // Iterar a través de los hermanos
                            foreach (Transform sibling in siblings)
                            {
                                // Asegurarse de que el hermano no sea el mismo GameObject
                                if (sibling.gameObject != cardObject)
                                {
                                    // Llamar a CheckAgainstStorage con el hermano
                                    ICard siblingCard = sibling.GetComponent<ICard>(); // Asegúrate de que el hermano tenga un componente ICard
                                    if (siblingCard != null)
                                    {
                                        selectedCardBehaviour = (siblingCard as CardBehaviour);
                                        break; // Salir después de encontrar el primer hermano que no sea el mismo
                                    }
                                }
                            }
                        }

                        // Determine the appropriate tooltip display based on active camera
                        CardDisplay cardToolDisplay = GetActiveTooltip();
                        if (cardToolDisplay != null)
                        {
                            cardToolDisplay.UpdateDisplay(selectedCardBehaviour.GetTemplate(), true, selectedCardBehaviour.LifeCycleDaysRemaining);
                            cardToolDisplay.gameObject.SetActive(true);
                        }
                        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
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
