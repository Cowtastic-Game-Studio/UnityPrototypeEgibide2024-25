using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardBehaviour : MonoBehaviour, ICard
    {
        [SerializeField]
        private CardTemplate template;

        private bool isActive;

        public CardState State { get; set; } = CardState.onDeck;
        public string Name => template.name;
        public string Description => template.description;
        public int ActionPointsCost => template.actionPointsCost;
        public int LifeCycleDays => template.lifeCycleDays;
        public int MarketCost => template.marketCost;
        public List<ResourceAmount> RequiredResources => template.requiredResources;
        public List<ResourceAmount> ProducedResources => template.producedResources;

        public bool IsActive => isActive;

        private void Start()
        {
            if (template == null)
            {
                Debug.LogError("Card template is not assigned.");
            }
            else
            {
                // Configurar la visualizaci�n de la carta usando CardDisplay
                CardDisplay display = GetComponent<CardDisplay>();
                if (display != null)
                {
                    SetupDisplay(display);
                }
            }
            // Desactiva la carta inicialmente
            Deactivate();
        }

        public void Activate()
        {
            isActive = true;
            UpdateDisplay();
        }

        public void Deactivate()
        {
            isActive = false;
            UpdateDisplay();
        }

        public void Print()
        {
            template.Print();
        }

        public void OnCardClicked()
        {
            //if (IsActive)
            //{
            // Pasar la instancia ICard
            ICard card = gameObject.GetComponent<ICard>(); ;
            GameManager.Instance?.CardClicked(card);
            //}
        }

        //public void OnPointerClick(PointerEventData eventData)
        private void OnMouseDown()
        {
            if (!GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
            {
                // Verifica si la carta est� en la layer 'CardLayer'
                if (gameObject.layer == LayerMask.NameToLayer("CardLayer"))
                {
                    OnCardClicked();
                }
            }
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameManager.Instance.Tabletop.CardManager.StopDragging();
            }
        }

        // M�todo para configurar la visualizaci�n en CardDisplay
        public void SetupDisplay(CardDisplay display)
        {
            // Pasa el estado activo
            display.UpdateDisplay(template, isActive);
        }

        // M�todo para actualizar la visualizaci�n
        private void UpdateDisplay()
        {
            CardDisplay display = GetComponent<CardDisplay>();
            if (display != null)
            {
                // Llama al m�todo para activar/desactivar el overlay
                display.SetOverlayActive(!isActive);
            }
        }
    }
}
