using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardItem : MonoBehaviour
    {
        [SerializeField] public CardDisplayUI cardDisplayUI;
        [SerializeField] private TMP_Text countTxt;

        private GameObject cardGO;
        private CardTemplate cardTemplate;
        private int totalCount;
        private int selectedCount = 0;

        public delegate void CountChangedDelegate(GameObject cardGO, int selectedCount, CardTemplate cardTemplate);
        public event CountChangedDelegate OnCountChanged;

        public void Setup(GameObject card, int count)
        {
            cardGO = card;
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
                NotifyManager();
            }
        }

        public void DecrementCount()
        {
            if (selectedCount > 0)
            {
                selectedCount--;
                UpdateCountText();
                NotifyManager();
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

        // Notificar al manager con el selectedCount y el discardCost
        private void NotifyManager()
        {
            OnCountChanged?.Invoke(cardGO, selectedCount, cardTemplate); // Pasar ambos valores al manager
        }
    }
}
