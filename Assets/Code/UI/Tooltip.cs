using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Tooltip : MonoBehaviour
    {

        public GameObject storagePanel;
        public GameObject cardTooltip;
        private GameObject selectedCard;

        private float hoverTime = 1.5f; // Hover time threshold
        private float storageHoverCounter = 0f;
        private float cardHoverCounter = 0f;

        private bool isHoveringStorage = false;
        private bool isHoveringCard = false;
        private GameObject currentHoveredCard = null;

        void Start()
        {
            storagePanel?.SetActive(false);
            cardTooltip?.SetActive(false);
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HandleStorageHover(hit);
                HandleCardHover(hit);
            }
            else
            {
                ResetStorageHover();
                ResetCardHover();
            }
        }

        private void HandleStorageHover(RaycastHit hit)
        {
            if (hit.collider.CompareTag("Storage"))
            {
                if (!isHoveringStorage)
                {
                    isHoveringStorage = true;
                    storageHoverCounter = 0f;
                }

                storageHoverCounter += Time.deltaTime;

                if (storageHoverCounter >= hoverTime)
                {
                    storagePanel.SetActive(true);
                }
            }
            else
            {
                ResetStorageHover();
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

                        CardBehaviour selctedCardBehaviour = selectedCard.GetComponent<CardBehaviour>();

                        CardBehaviour cardTool = cardTooltip.GetComponent<CardBehaviour>();
                        CardDisplay cardToolDisplay = cardTool.GetComponent<CardDisplay>();

                        cardToolDisplay.UpdateDisplay(selctedCardBehaviour.GetTemplate(), false);

                        // Mueve el cardTooltip a la posición del ratón
                        //Vector3 mousePosition = Input.mousePosition;                    
                        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                        //cardTooltip.transform.position = worldPosition;

                        cardTooltip.SetActive(true);
                    }
                }
                else
                {
                    ResetCardHover();
                }
            }
        }

        private void ResetStorageHover()
        {
            isHoveringStorage = false;
            storageHoverCounter = 0f;
            storagePanel.SetActive(false);
        }

        private void ResetCardHover()
        {
            isHoveringCard = false;
            cardHoverCounter = 0f;
            currentHoveredCard = null;
            selectedCard = null;
            cardTooltip.SetActive(false);
        }


    }
}
