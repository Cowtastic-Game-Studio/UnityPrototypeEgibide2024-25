using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private CardDisplay card;

        public CardTemplate cardTemplate;
        public int price;
        public int discountPrice;

        public void UpdateDisplayData(CardTemplate cardT, float discountPercentage)
        {
            cardTemplate = cardT;
            card.UpdateDisplayAndMat(cardTemplate, true);
            int finalPrice = Utils.RoundMuuney(cardTemplate.marketCost * discountPercentage);
            price = finalPrice;
        }

        public void UpdateDisplayDataSpecial(float discountPercentage)
        {
            gameObject.GetComponent<CardDisplay>()?.UpdateDisplayAndMat(cardTemplate, true);
            int finalPrice = Utils.RoundMuuney(cardTemplate.marketCost * discountPercentage);
            price = finalPrice;
        }

        public void SetDiscountPrice(int discount)
        {
            discountPrice = price - discount;
        }

        public void TriggerCard()
        {
            Debug.Log("TriggerCard");
            card.UpdateDisplayAndMat(cardTemplate, false);
        }

        public void TriggerPrice(bool hasDiscount, GameObject cardToDelete)
        {
            if (hasDiscount)
            {
                BuyCard(discountPrice, hasDiscount, cardToDelete);
            }
            else
            {
                BuyCard(price, hasDiscount, null);
            }
        }

        private void BuyCard(int price, bool hasDiscount, GameObject cardToDelete)
        {
            if (cardTemplate.cardType == CardType.None)
            {
                //mejoras permanentes del tablero
                if (cardTemplate.targetCardType == CardType.None && cardTemplate.targetResoruceType != GameResource.None)
                {
                    GameManager.Instance.Tabletop.StorageManager.UpgradeStorage(cardTemplate.targetResoruceType);
                }
                else if (cardTemplate.targetCardType != CardType.None && cardTemplate.targetResoruceType == GameResource.None && GameManager.Instance.Tabletop.StorageManager.CheckMuuney(price))
                {

                    switch (cardTemplate.targetCardType)
                    {
                        case CardType.Cow:
                            GameManager.Instance.Tabletop.StablesActivateZone(price);
                            break;
                        case CardType.Seed:
                            GameManager.Instance.Tabletop.FarmsActivateZone(price);
                            break;
                        case CardType.Customer:
                            GameManager.Instance.Tabletop.TavernActivateZone(price);
                            break;
                    }

                    if (hasDiscount)
                    {
                        GameManager.Instance.Tabletop.CardManager.TryRemoveCardsGOFromDecks(cardToDelete);
                    }

                    StatisticsManager.Instance.UpdateByBuyedZone(cardTemplate.targetCardType, hasDiscount);
                }
            }
            else
            {
                if (GameManager.Instance.Tabletop.StorageManager.CheckMuuney(price))
                {
                    GameManager.Instance.Tabletop.CardManager.BuyCard(cardTemplate.name, price);
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
