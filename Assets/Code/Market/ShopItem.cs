using TMPro;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] public CardDisplay card;

        private CardTemplate cardTemplate;

        public void UpdateDisplayData(CardTemplate cardT, float discountPercentage)
        {
            cardTemplate = cardT;
            card.UpdateDisplayAndMat(cardTemplate, false);
            float finalPrice = Utils.RoundMuuney(cardTemplate.marketCost * discountPercentage);
            price.text = finalPrice.ToString();
        }

        public void TriggerCard()
        {
            // TODO: Show Card Preview
            Debug.Log("TriggerCard");
            card.UpdateDisplayAndMat(cardTemplate, false);
        }

        public void TriggerPrice()
        {
            BuyCard(int.Parse(price.text));
        }

        public void BuyCard(int price)
        {
            if (GameManager.Instance.Tabletop.StorageManager.CheckMuuney(price))
            {
                int muuney = GameManager.Instance.Tabletop.StorageManager.WasteMuuney(price);

                GameManager.Instance.Tabletop.CardManager.BuyCard(cardTemplate.name);
            }
        }

        public CardTemplate getCardTemplate()
        {
            return cardTemplate;
        }
    }
}
