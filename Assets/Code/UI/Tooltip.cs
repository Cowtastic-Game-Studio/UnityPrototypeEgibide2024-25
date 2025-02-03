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



        private float hoverTime = 3f; // Tiempo necesario en hover (3 segundos)
        private float storageHoverCounter = 0f; // Contador para Storage
        private float cardHoverCounter = 0f; // Contador para Cards

        private bool isHoveringStorage = false;
        private bool isHoveringCard = false;
        private GameObject currentHoveredCard = null;


        void Start()
        {
            if (storagePanel != null)
            {
                storagePanel.SetActive(false); // Oculta el panel al inicio
                cardTooltip.SetActive(false);

            }
        }

        void Update()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Si el objeto tiene la etiqueta "Storage"
                if (hit.collider.CompareTag("Storage"))
                {
                    if (hit.collider.CompareTag("Storage"))
                    {
                        if (!isHoveringStorage)
                        {
                            isHoveringStorage = true;
                            storageHoverCounter = 0f;
                        }

                        storageHoverCounter += Time.deltaTime;

                        if (storageHoverCounter >= hoverTime) // Si pasa el tiempo requerido
                        {
                            storagePanel.SetActive(true);
                        }
                        return;
                    }
                    else
                    {
                        isHoveringStorage = false;
                        storageHoverCounter = 0f;
                        storagePanel.SetActive(false); // Ocultamos si no estamos sobre él
                    }

                    // Hover sobre Cards
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

                        if (cardHoverCounter >= hoverTime) // Si pasa el tiempo necesario
                        {
                            selectedCard = currentHoveredCard;
                        }
                        return;
                    }
                    else
                    {
                        isHoveringCard = false;
                        cardHoverCounter = 0f;
                        currentHoveredCard = null;
                        selectedCard = null;
                    }
                }
                else
                {
                    // Si no hay colisión con nada relevante, reiniciamos todo
                    isHoveringStorage = false;
                    storageHoverCounter = 0f;
                    storagePanel.SetActive(false);

                    isHoveringCard = false;
                    cardHoverCounter = 0f;
                    currentHoveredCard = null;
                    selectedCard = null;
                }


                // Si no hay nada relevante, ocultamos el panel y limpiamos selectedCard
                storagePanel.SetActive(false);
                selectedCard = null;
                isHoveringStorage = false;
                //hoverCounter = 0f;


            }
        }
        private void PlacedCardTooltip()
        {
            CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();
            //CardType cardType = card.GetComponent<CardType>();

            cardTooltip.SetActive(true);
            // UpdateDisplay



        }


    }
}
