using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlaceSpaceBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool isActive = true;

        private bool isEmpty = true;
        private bool deactiveNextDay = false;

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
            GameObject selectedCard = GameManager.Instance.Tabletop.CardManager.getSelectedCard();
            CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();

            bool shouldHighlight = false;

            // Verificamos las condiciones dependiendo del tipo de carta
            bool isCardTypeMatch = card.TargetType == type;
            bool isActiveConditionMet = (isActive && isEmpty);

            if (card.Type == CardType.PlaceActivator)
            {
                // PlaceActivator solo se activa si no está activo y está vacío
                shouldHighlight = !isActive && isEmpty && isCardTypeMatch;
            }
            else if (card.Type == CardType.PlaceMultiplier)
            {
                // PlaceMultiplier solo se activa si está activo y está vacío
                shouldHighlight = isActive && isEmpty && isCardTypeMatch;
            }
            else
            {
                isCardTypeMatch = card.Type == type;
                // Para otros tipos de carta, solo se activa si está activo, vacío y coincide el tipo
                shouldHighlight = isActiveConditionMet && isCardTypeMatch;
            }

            Highlight(shouldHighlight);
        }


        private void OnMouseExit()
        {
            RemoveHighlight();
        }

        private void OnMouseDown()
        {
            // TODO: Da un fallo si se clicka en un espacio vacio 
            // Verifica si la carta est� en la layer 'PlaceSpace'
            if (gameObject.layer == LayerMask.NameToLayer("PlaceSpace"))
            {
                // Recuperamos el componente CardBehaviour
                GameObject selectedCard = GameManager.Instance.Tabletop.CardManager.getSelectedCard();
                if (!selectedCard)
                    return;

                CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();

                bool canPlace = false, stayEmpty = false;

                // Verificamos las condiciones dependiendo del tipo de carta
                bool isCardTypeMatch = card.TargetType == type;
                bool isActiveConditionMet = (isActive && isEmpty);

                if (card.Type == CardType.PlaceActivator)
                {
                    // PlaceActivator solo se activa si no está activo y está vacío
                    canPlace = !isActive && isEmpty && isCardTypeMatch;
                    stayEmpty = canPlace;
                    if (canPlace)
                    {
                        deactiveNextDay = true;
                        SetIsActive(true);
                        card.Deactivate();
                        GameManager.Instance.Tabletop.CardManager.MoveLastCardsToHand(1);
                    }


                    //actualizar la estadistica
                    StatisticsManager.Instance.UpdateByType(card);
                }
                else if (card.Type == CardType.PlaceMultiplier)
                {
                    // PlaceMultiplier solo se activa si está activo y está vacío
                    canPlace = isActive && isEmpty && isCardTypeMatch;
                    stayEmpty = canPlace;
                    card.Deactivate();
                    GameManager.Instance.Tabletop.CardManager.MoveLastCardsToHand(1);

                    //actualizar la estadistica
                    StatisticsManager.Instance.UpdateByType(card);
                }
                else
                {
                    //actualizar la estadistica HELPER
                    if (card.Type == CardType.Helper)
                        StatisticsManager.Instance.UpdateByType(card);

                    isCardTypeMatch = card.Type == type;
                    // Para otros tipos de carta, solo se activa si está activo, vacío y coincide el tipo
                    canPlace = isActiveConditionMet && isCardTypeMatch;
                    stayEmpty = false;
                }

                if (canPlace)
                    OnPlaceSpaceClicked(stayEmpty);

            }
        }

        public void OnPlaceSpaceClicked(bool shouldStayEmpty)
        {
            isEmpty = shouldStayEmpty;

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

        public bool GetIsActive()
        {
            return isActive;
        }

        public bool GetIsEmpty()
        {
            return isEmpty;
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


        public void updateEmpty()
        {
            isEmpty = true;
        }

        internal void updateActive()
        {
            if (deactiveNextDay)
                isActive = false;
        }

    }
}
