using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class InitMarketCards : MonoBehaviour
    {
        [SerializeField] private List<CardTemplate> CowsList;
        [SerializeField] private List<CardTemplate> CustomersList;
        [SerializeField] private List<CardTemplate> SeedsList;
        [SerializeField] private List<CardTemplate> ActivatorsList;
        [SerializeField] private List<CardTemplate> ZonesList;

        private List<ShopItemData> shopItemsData = new();

        internal List<ShopItemData> ShopItemsData { get => shopItemsData; set => shopItemsData = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Initialize()
        {
            CreateCards(CowsList);
            CreateCards(CustomersList);
            CreateCards(SeedsList);
            CreateCards(ActivatorsList);
            CreateCards(ZonesList);
        }

        private void CreateCards(List<CardTemplate> cardList)
        {
            int dayToUnlock = 0;
            foreach (CardTemplate card in cardList)
            {
                ShopItemData itemData = new(false, card, dayToUnlock);
                ShopItemsData.Add(itemData);

                dayToUnlock += 2;
            }
        }
    }
}
