using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardItem : MonoBehaviour
    {
        [SerializeField] public CardDisplayUI cardDisplayUI;

        private CardTemplate cardTemplate;

        public void Setup(GameObject card)
        {
            cardTemplate = card.GetComponent<CardBehaviour>()?.GetTemplate();
            cardDisplayUI.UpdateDisplay(cardTemplate, true);
        }
    }
}
