using TMPro;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] private CardDisplay card;

        public void UpdateDisplayData(CardTemplate cardTemplate)
        {
            card.UpdateDisplay(cardTemplate, false);
            price.text = cardTemplate.marketCost.ToString();
        }

        public void TriggerCard()
        {
            Debug.Log("TriggerCard");
        }

        public void TriggerPrice()
        {
            Debug.Log("TriggerPrice");
            Debug.Log("Price: " + price.text);

            BuyCard(int.Parse(price.text));
        }

        public void BuyCard(int price)
        {
            if (GameManager.Instance.Tabletop.StorageManager.CheckMuuney(price))
            {
                //Muuney = Muuney - x;
                int muuney = GameManager.Instance.Tabletop.StorageManager.WasteMuuney(price);

                //GameManager.Instance.Tabletop.CardManager.buyCard(cardType);
            }
        }

    }
}
