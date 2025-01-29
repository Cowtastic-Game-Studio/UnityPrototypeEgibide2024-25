using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlaceSpaceBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool isActive = true;

        private bool isEmpty = true;

        [SerializeField]
        private CardType type;

        private Renderer myRenderer;
        private Color originalColor;
        private Color highlightColorValid = Color.green;  // El color de resaltado al pasar el raton cuando es positivo
        private Color highlightColorIncorrect = Color.red; // El color de resaltado al pasar el raton cuando es negativo

        private void Start()
        {
            myRenderer = GetComponent<Renderer>();
            if (myRenderer != null)
            {
                originalColor = myRenderer.material.color;
            }
        }

        private void OnMouseEnter()
        {
            // Solo resalta si se está arrastrando una carta
            if (!GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
                return;

            // Recuperamos el componente CardBehaviour
            GameObject selectedCard = GameManager.Instance.Tabletop.CardManager.selectedCard;
            CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();

            bool shouldHighlight = false;

            // Comprobamos el tipo de carta y las condiciones de activación
            if (card.Type == CardType.PlaceActivator)
            {
                bool isCardTypeMatch = card.TargetType == type;
                bool isActiveConditionMet = (!isActive && isEmpty);

                // Si la carta es de tipo PlaceActivator o PlaceMultiplier, comprobamos las condiciones
                shouldHighlight = (isActiveConditionMet && isCardTypeMatch);
            }
            else if (card.Type == CardType.PlaceActivator || card.Type == CardType.PlaceMultiplier)
            {
                bool isCardTypeMatch = card.TargetType == type;
                bool isActiveConditionMet = (isActive && isEmpty);

                // Si la carta es de tipo PlaceActivator o PlaceMultiplier, comprobamos las condiciones
                shouldHighlight = (isActiveConditionMet && isCardTypeMatch);
            }
            else
            {
                // Para otros tipos de carta, comprobamos solo si está activa y es vacía
                shouldHighlight = isActive && isEmpty && card.Type == type;
            }

            Highlight(shouldHighlight);
        }


        private void OnMouseExit()
        {
            RemoveHighlight();
        }

        private void OnMouseDown()
        {
            // Verifica si la carta est� en la layer 'PlaceSpace'
            if (gameObject.layer == LayerMask.NameToLayer("PlaceSpace"))
            {
                // Recuperamos el componente CardBehaviour
                GameObject selectedCard = GameManager.Instance.Tabletop.CardManager.selectedCard;
                CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();

                if (isActive && isEmpty)
                {
                    if (card.Type == type)
                    {
                        OnPlaceSpaceClicked();
                    }
                }
            }
        }

        public void OnPlaceSpaceClicked()
        {
            //if (isActive && isEmpty)
            //{
            Transform placeTrans = gameObject.transform;
            GameManager.Instance?.PlaceSpaceClicked(placeTrans);
            //}
        }

        public CardType GetType(PlaceSpaceBehaviour item)
        {
            return item.type;
        }

        public bool GetIsActive(PlaceSpaceBehaviour item)
        {
            return item.isActive;
        }

        public void SetIsActive(bool active)
        {
            isActive = active;
        }


        private void Highlight(bool valid)
        {
            if (myRenderer != null)
            {
                if (valid == true)
                {
                    myRenderer.material.color = highlightColorValid;
                }
                else
                {
                    myRenderer.material.color = highlightColorIncorrect;
                }
            }
        }

        private void RemoveHighlight()
        {
            if (myRenderer != null)
            {
                myRenderer.material.color = originalColor;
            }
        }

    }
}
