using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardBehaviour : MonoBehaviour, ICard
    {
        #region Properties

        #region Property: IsPlaced

        public bool IsPlaced { get; set; }

        #endregion

        #region Property: PositionInHand

        /// <summary>
        /// Posicion de la carta en la mano
        /// </summary>
        public float? PositionInHand { get; set; }

        #endregion


        #endregion


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


        #region Unity methods

        private void Start()
        {
            this.PositionInHand = null;

            if (template == null)
            {
                Debug.LogError("Card template is not assigned.");
            }
            else
            {
                // Configurar la visualización de la carta usando CardDisplay
                CardDisplay display = GetComponent<CardDisplay>();
                if (display != null)
                {
                    SetupDisplay(display);
                }
            }
            // Desactiva la carta inicialmente
            Deactivate();
        }

        private void OnMouseDown()
        {
            if (GameManager.Instance.GamePhaseManager.CurrentPhaseType == GamePhaseTypes.PlaceCards)
            {
                //Si esta colocada
                if (this.IsPlaced)
                {
                    //Vuelve a la mano
                    GameManager.Instance.Tabletop.CardManager.RemovePlacedCard(this.gameObject);
                }
                else
                {
                    if (!GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
                    {
                        // Verifica si la carta está en la layer 'CardLayer'
                        if (gameObject.layer == LayerMask.NameToLayer("CardLayer"))
                        {
                            InvokeCardClicked();
                        }
                    }
                }
            }
            else if (GameManager.Instance.GamePhaseManager.CurrentPhaseType == GamePhaseTypes.Action)
            {
                if (IsActive)
                {
                    InvokeCardClicked();
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

        #endregion

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

        public void InvokeCardClicked()
        {
            // Pasar la instancia ICard
            ICard card = gameObject.GetComponent<ICard>();
            GameManager.Instance?.CardClicked(card);
        }

        //public void OnPointerClick(PointerEventData eventData)


        // Método para configurar la visualización en CardDisplay
        public void SetupDisplay(CardDisplay display)
        {
            // Pasa el estado activo
            display.UpdateDisplay(template, isActive);
        }

        // Método para actualizar la visualización
        private void UpdateDisplay()
        {
            CardDisplay display = GetComponent<CardDisplay>();
            if (display != null)
            {
                // Llama al método para activar/desactivar el overlay
                display.SetOverlayActive(!isActive);
            }
        }
    }
}
