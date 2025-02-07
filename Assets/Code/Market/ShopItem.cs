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

                if (cardTemplate.cardType == CardType.None)
                {
                    //mejoras permanentes del tablero
                    if (cardTemplate.targetCardType == CardType.None && cardTemplate.targetResoruceType != GameResource.None)
                    {
                        GameManager.Instance.Tabletop.StorageManager.UpgradeStorage(cardTemplate.targetResoruceType);

                    }
                    else if (cardTemplate.targetCardType != CardType.None && cardTemplate.targetResoruceType == GameResource.None)
                    {
                        switch (cardTemplate.targetCardType)
                        {
                            case CardType.Cow:
                                GameManager.Instance.Tabletop.StablesActivateZone();
                                break;
                            case CardType.Seed:
                                GameManager.Instance.Tabletop.FarmsActivateZone();
                                break;
                            case CardType.Customer:
                                GameManager.Instance.Tabletop.TavernActivateZone();
                                break;
                        }

                        StatisticsManager.Instance.UpdateByBuyedZone(cardTemplate.targetCardType);
                    }
                }
                else
                {
                    GameManager.Instance.Tabletop.CardManager.BuyCard(cardTemplate.name);

                    StatisticsManager.Instance.UpdateByBuyedCard(cardTemplate.cardType);
                }
            }
        }

        public CardTemplate getCardTemplate()
        {
            return cardTemplate;
        }
    }
}
