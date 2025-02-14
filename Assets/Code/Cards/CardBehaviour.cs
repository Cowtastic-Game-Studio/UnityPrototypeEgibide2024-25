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
        public CardType Type => template.cardType;
        public CardType TargetType => template.targetCardType;
        public string Description => template.description;
        public int ActionPointsCost => template.actionPointsCost;
        public int LifeCycleDays => template.lifeCycleDays;
        public int MarketCost => template.marketCost;
        public List<ResourceAmount> RequiredResources => template.requiredResources;
        public List<ResourceAmount> ProducedResources => template.producedResources;

        public bool IsActive => isActive;

        bool mouseClicksStarted = false;
        int mouseClicks = 0;
        float mouseTimerLimit = .25f;

        private int lifeCycleDaysRemaining;

        public int LifeCycleDaysRemaining
        {
            get { return lifeCycleDaysRemaining; }
            set
            {
                lifeCycleDaysRemaining = value;
                UpdateDisplay();
            }
        }


        #region Unity methods

        private void OnMouseDown()
        {
            if (GameManager.Instance.GamePhaseManager.CurrentPhaseType == GamePhaseTypes.PlaceCards)
            {
                OnClick();
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
            if (State.Equals(CardState.onHand))
            {
                GameManager.Instance.Tabletop.CardManager.SetHoveredCard(this);
            }
        }

        private void OnMouseExit()
        {
            if (State.Equals(CardState.onHand))
            {
                GameManager.Instance.Tabletop.CardManager.ClearHoveredCard(this);
            }
        }

        #endregion



        #region Public methods
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
            if (IsActive)
            {
                // Pasar la instancia ICard
                ICard card = gameObject.GetComponent<ICard>();
                GameManager.Instance?.CardClicked(card);
            }
        }

        public void SetupDisplay(CardDisplay display)
        {
            // Pasa el estado activo
            display.UpdateDisplayAndMat(template, isActive);
        }

        public CardTemplate GetTemplate()
        {
            return template;
        }
        #endregion


        #region Private methods
        /// <summary>
        /// M�todo para actualizar la visualizaci�n
        /// </summary>
        private void UpdateDisplay()
        {
            CardDisplay display = GetComponent<CardDisplay>();
            if (display != null)
            {
                display.UpdateDisplayAndMat(template, isActive);
                // Llama al m�todo para activar/desactivar el overlay
                // display.SetOverlayActive(!isActive);
            }
        }

        private void OnClick()
        {
            mouseClicks++;
            if (mouseClicksStarted)
            {
                return;
            }
            mouseClicksStarted = true;
            Invoke(nameof(CheckMouseDoubleClick), mouseTimerLimit);
        }


        private void CheckMouseDoubleClick()
        {
            if (mouseClicks > 1)
            {
                //Vuelve a la mano
                GameManager.Instance.Tabletop.CardManager.RemovePlacedCard(this.gameObject);
                isActive = true;
            }
            else
            {
                if (!GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
                {
                    // Verifica si la carta est� en la layer 'CardLayer'
                    if (gameObject.layer == LayerMask.NameToLayer("CardLayer"))
                    {
                        var placeSpace = transform.parent?.GetComponent<PlaceSpaceBehaviour>();
                        if (placeSpace != null)
                        {
                            placeSpace.updateEmpty();
                        }

                        InvokeCardClicked();
                    }
                }
            }
            mouseClicksStarted = false;
            mouseClicks = 0;
        }

        public void setCardTemplate(CardTemplate cardTemplate)
        {
            template = cardTemplate;
            //UpdateDisplay();

            LifeCycleDaysRemaining = LifeCycleDays;

            this.PositionInHand = null;

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

        #endregion
    }
}
