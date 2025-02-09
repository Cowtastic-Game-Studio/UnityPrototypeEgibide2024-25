using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardItem : MonoBehaviour
    {
        [SerializeField] public CardDisplayUI cardDisplayUI;
        [SerializeField] private TMP_Text countTxt;

        private CardTemplate cardTemplate;
        private int totalCount;
        private int selectedCount = 0;

        public void Setup(GameObject card, int count)
        {
            cardTemplate = card.GetComponent<CardBehaviour>()?.GetTemplate();
            totalCount = count;
            selectedCount = 0;
            InitCountText();
            cardDisplayUI.UpdateDisplay(cardTemplate, true);
        }

        public void IncrementCount()
        {
            if (selectedCount < totalCount)
            {
                selectedCount++;
                UpdateCountText();
            }
        }

        public void DecrementCount()
        {
            if (selectedCount > 0)
            {
                selectedCount--;
                UpdateCountText();
            }
        }

        private void UpdateCountText()
        {
            countTxt.text = $"{selectedCount}/{totalCount}";
        }

        private void InitCountText()
        {
            countTxt.text = $"{totalCount}";
        }
    }
}
