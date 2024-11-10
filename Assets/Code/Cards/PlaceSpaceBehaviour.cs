using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlaceSpaceBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool isActive = true;

        private bool isEmpty = false;


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
    }
}
