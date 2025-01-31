using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class NewMarketManager : MonoBehaviour
    {
        [SerializeField] private InitMarketCards marketCards;
        [SerializeField] private List<GameObject> buttonList;


        [SerializeField] private GameObject page;
        [SerializeField] private GameObject shopItem;

        private List<ShopItemData> shopItemsData;

        private List<GameObject> pageList = new();

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Market Manager Start");
            marketCards.Initialize();
            shopItemsData = marketCards.ShopItemsData;
            GetMuuney();

            UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow).ConvertAll(x => x.cardTemplate));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del mouse
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider != null)
                    {
                        string name = hit.collider.name;
                        Debug.Log("Hit: " + hit.collider.name);

                        switch (name)
                        {
                            case "CowButton":
                                shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow);

                                UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow).ConvertAll(x => x.cardTemplate));

                                break;
                            case "SeedButton":
                                break;
                            case "ClientButton":
                                break;
                            case "TemporalButton":
                                break;
                            case "ZonesButton":
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        public void UpdateShopItemDisplay(List<CardTemplate> cardList)
        {
            Debug.Log("Update");

            pageList = page.GetComponent<SlotsList>()?.slotsList;
            int counter = 0;
            foreach (CardTemplate card in cardList)
            {
                GameObject slot = pageList[counter];
                Debug.Log("Slot");

                GameObject createdItem = GameObject.Instantiate(shopItem, slot.transform);

                createdItem.GetComponent<ShopItem>()?.UpdateDisplayData(card);
                counter++;
            }
        }


        public void GetItemSlots()
        {
        }


        private void GetMuuney()
        {
            int totalMuuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);

            Debug.Log($"Total Muuney: {totalMuuney}");

        }
    }
}
