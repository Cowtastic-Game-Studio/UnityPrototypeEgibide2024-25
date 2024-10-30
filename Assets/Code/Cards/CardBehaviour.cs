using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardBehaviour : MonoBehaviour
    {
        private void OnMouseDown()
        {
            // Llama al evento global en GameManager cuando se hace clic en esta carta
            GameManager.Instance?.CardClicked(gameObject);
        }
    }
}
