using System;
using System.Collections;
using System.Collections.Generic;
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
            if (hit.collider.CompareTag("CardCow") ||
                hit.collider.CompareTag("CardSeed") ||
                hit.collider.CompareTag("CardClient"))
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

                    CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();
                    CardTemplate cardTemplate = selectedCard.GetComponent<CardTemplate>();

                    cardTooltip.SetActive(true);
                }
            }
            else
            {
                ResetCardHover();
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
