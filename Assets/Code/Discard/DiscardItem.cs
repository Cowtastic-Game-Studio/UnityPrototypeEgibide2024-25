using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class DiscardItem : MonoBehaviour
    {
        [SerializeField] public CardDisplayUI cardDisplayUI;
        [SerializeField] private TMP_Text countTxt;
        [SerializeField] private TMP_Text discardTxt;

        private GameObject cardGO;
        public CardTemplate CardTemplate { get; private set; }
        private int totalCount;
        private int discardPrice;
        public int SelectedCount { get; private set; } = 0;

        public delegate void CountChangedDelegate(GameObject cardGO, int selectedCount, CardTemplate cardTemplate);
        public event CountChangedDelegate OnCountChanged;

        public void Setup(GameObject card, int count)
        {
            cardGO = card;
            CardTemplate = card.GetComponent<CardBehaviour>()?.GetTemplate();
            totalCount = count;
            SelectedCount = 0;
            discardPrice = CardTemplate.discardCost;
            InitCountText();
            cardDisplayUI.UpdateDisplay(CardTemplate, true);



        }
        public void IncrementCount()
        {
            // Accedemos a discardManager desde GameManager.Instance.Tabletop
            var discardManager = GameManager.Instance.Tabletop.DiscardManager;
            if (!discardManager.CheckMinDeckReached())
            {
                if (SelectedCount < totalCount && discardManager.currentDiscardCount < discardManager.maxDiscardLimit)
                {
                    if (GameManager.Instance.Tabletop.StorageManager.CheckMuuney(discardManager.TotalCost + CardTemplate.discardCost))
                    {
                        SelectedCount++;
                        UpdateCountText();
                        NotifyManager();
                        discardManager.currentDiscardCount++; // Incrementar el contador de eliminaciones
                    }
                    else if (discardManager.currentDiscardCount >= discardManager.maxDiscardLimit)
                    {
                        MessageManager.Instance.ShowMessage("There is not enough money to discard.");

                        //Debug.LogWarning("No hay sucifiente dinero para descartar esta carta.");
                    }
                }
                else if (discardManager.currentDiscardCount >= discardManager.maxDiscardLimit)
                {
                    MessageManager.Instance.ShowMessage("You have reached the card elimination limit for this turn.");
                    //Debug.LogWarning("Has alcanzado el límite de eliminaciones para este turno.");
                }
            }
            else if (discardManager.currentDiscardCount >= discardManager.maxDiscardLimit)
            {
                MessageManager.Instance.ShowMessage("The maze size limit has been reached.");
                //Debug.LogWarning("Has alcanzado el límite de tamano del mazo");
            }
        }

        public void DecrementCount()
        {
            if (SelectedCount > 0)
            {
                SelectedCount--;
                UpdateCountText();
                NotifyManager();
                GameManager.Instance.Tabletop.DiscardManager.currentDiscardCount--; // Decrementar el contador de eliminaciones
            }
        }

        private void UpdateCountText()
        {
            countTxt.text = $"{SelectedCount}/{totalCount}";
            discardTxt.text = discardPrice.ToString();
        }

        private void InitCountText()
        {
            countTxt.text = $"{totalCount}";
            discardTxt.text = discardPrice.ToString();
        }

        // Notificar al manager con el selectedCount y el discardCost
        private void NotifyManager()
        {
            OnCountChanged?.Invoke(cardGO, SelectedCount, CardTemplate); // Pasar ambos valores al manager
        }
    }
}
