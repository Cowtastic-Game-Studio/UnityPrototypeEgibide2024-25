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

        private Renderer renderer;
        private Color originalColor;
        public Color highlightColorValid = Color.green;  // El color de resaltado al pasar el raton cuando es positivo
        public Color highlightColorIncorrect = Color.red; // El color de resaltado al pasar el raton cuando es negativo

        private void Start()
        {
            renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                originalColor = renderer.material.color;
            }
        }

        private void OnMouseEnter()
        {
            // Solo resalta si se est� arrastrando una carta
            if (GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
            {
                // Recuperamos el componente CardBehaviour
                GameObject selectedCard = GameManager.Instance.Tabletop.CardManager.selectedCard;
                CardBehaviour card = selectedCard.GetComponent<CardBehaviour>();

                if (isActive && isEmpty) {
                    if (card.Type == type) {
                        Debug.LogError(card.Type);
                        Debug.LogError(type);
                        Highlight(true);
                    }
                    else {
                        Debug.LogError(card.Type);
                        Debug.LogError(type);
                        Highlight(false);
                    }
                }
            }
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

                if (isActive && isEmpty) {
                    if (card.Type == type) {
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

        private void Highlight(bool valid)
        {
            if (renderer != null)
            {
                if (valid == true) {
                    renderer.material.color = highlightColorValid;
                }
                else {
                    renderer.material.color = highlightColorIncorrect;
                }
            }
        }

        private void RemoveHighlight()
        {
            if (renderer != null)
            {
                renderer.material.color = originalColor;
            }
        }
    }
}
