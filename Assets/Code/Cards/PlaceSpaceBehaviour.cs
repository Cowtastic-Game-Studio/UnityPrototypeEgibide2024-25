using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlaceSpaceBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool isActive = true;

        private bool isEmpty = false;

        private Renderer renderer;
        private Color originalColor;
        public Color highlightColor = Color.green;  // El color de resaltado al pasar el ratón

        private void Start()
        {
            renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                //originalColor = renderer.material.color;
            }
        }

        private void OnMouseEnter()
        {
            // Solo resalta si se está arrastrando una carta
            if (GameManager.Instance.Tabletop.CardManager.IsDraggingCard)
            {
                //Highlight();
            }
        }

        private void OnMouseExit()
        {
            //RemoveHighlight();
        }

        private void OnMouseDown()
        {
            // Verifica si la carta está en la layer 'PlaceSpace'
            if (gameObject.layer == LayerMask.NameToLayer("PlaceSpace"))
            {
                OnPlaceSpaceClicked();
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

        private void Highlight()
        {
            if (renderer != null)
            {
                renderer.material.color = highlightColor;
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
