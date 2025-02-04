using TMPro;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] private CardDisplay card;

        private CardTemplate cardTemplate;

        public void UpdateDisplayData(CardTemplate cardT, float discountPercentage)
        {
            cardTemplate = cardT;
            card.UpdateDisplay(cardTemplate, false);
            float finalPrice = Utils.RoundMuuney(cardTemplate.marketCost * discountPercentage);
            price.text = finalPrice.ToString();
        }

        public void TriggerCard()
        {
            // TODO: Show Card Preview
            Debug.Log("TriggerCard");
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

    }
}
