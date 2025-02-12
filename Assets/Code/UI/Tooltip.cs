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

        private bool isHoveringCard = false;
        private GameObject currentHoveredCard = null;

        void Start()
        {
            storagePanel.SetActive(false);
            cardTooltip.SetActive(false);
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
                // storagePanel.SetActive(false);
                ResetCardHover();
            }
        }

        private void HandleStorageHover(RaycastHit hit)
        {
            if (hit.collider.CompareTag("Storage"))
            {
                storagePanel.SetActive(true);

            }
            else if (GameManager.Instance.GamePhaseManager.CurrentPhase is ActionPointsPhase)
            {
                storagePanel.SetActive(true);
            }
            else
            {
                storagePanel.SetActive(false);
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

                        //CardBehaviour cardTool = cardTooltip.GetComponent<CardBehaviour>();
                        CardDisplay cardToolDisplay = cardTooltip.GetComponent<CardDisplay>();

                        cardToolDisplay.UpdateDisplay(selctedCardBehaviour.GetTemplate(), true, selctedCardBehaviour.LifeCycleDaysRemaining);

                        cardTooltip.SetActive(true);
                    }
                }
                else
                {
                    ResetCardHover();
                }
            }
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
