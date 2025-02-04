using TMPro;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] private CardDisplay card;

        private CardTemplate cardTemplate;
        public void UpdateDisplayData(CardTemplate cardT)
        {
            cardTemplate = cardT;
            card.UpdateDisplay(cardTemplate, false);
            price.text = cardTemplate.marketCost.ToString();
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

                if (cardTemplate.cardType == CardType.None)
                {
                    //mejoras permanentes del tablero
                    if (cardTemplate.targetCardType == CardType.None && cardTemplate.targetResoruceType != GameResource.None)
                    {
                        GameManager.Instance.Tabletop.StorageManager.UpgradeStorage(cardTemplate.targetResoruceType);

                        StatisticsManager.Instance.UpdateByBuyedZone(cardTemplate.targetResoruceType);
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
    }
}
