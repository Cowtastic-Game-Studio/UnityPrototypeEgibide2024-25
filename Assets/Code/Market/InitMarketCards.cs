using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class InitMarketCards : MonoBehaviour
    {
        [SerializeField] private List<CardTemplate> CowsList;
        [SerializeField] private List<CardTemplate> SeedsList;
        [SerializeField] private List<CardTemplate> CustomersList;
        [SerializeField] private List<CardTemplate> ActivatorsList;
        [SerializeField] private List<CardTemplate> ZonesList;

        private List<ShopItemData> shopItemsData = new();

        internal List<ShopItemData> ShopItemsData { get => shopItemsData; set => shopItemsData = value; }

        public void Initialize()
        {
            CreateCards(CowsList);
            CreateCards(SeedsList);
            CreateCards(CustomersList);
            CreateCards(ActivatorsList);
            CreateCards(ZonesList);
        }

        private void CreateCards(List<CardTemplate> cardList)
        {
            foreach (CardTemplate card in cardList)
            {
                ShopItemData itemData = new(CanUnlock(card.dayToUnlock), card);
                ShopItemsData.Add(itemData);
            }
        }

        // TODO: Change 0 to current day in the future
        private bool CanUnlock(int dayToUnlock)
        {
            return 0 >= dayToUnlock;
        }
    }
}
