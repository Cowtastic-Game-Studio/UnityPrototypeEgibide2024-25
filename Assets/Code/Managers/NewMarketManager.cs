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
        [SerializeField] private GameObject cardPreview;

        private List<ShopItemData> shopItemsData;

        private List<GameObject> pageList = new();

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            cardPreview.SetActive(false);
            marketCards.Initialize();
            shopItemsData = marketCards.ShopItemsData;
            GetMuuney();

            pageList = page.GetComponent<SlotsList>()?.slotsList;
            UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow).ConvertAll(x => x.cardTemplate));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del mouse
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask = LayerMask.GetMask("Default"); // Ajusta esto según las capas que quieras considerar

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider != null)
                    {
                        string name = hit.collider.name;
                        GameObject shopItemGO = hit.collider.gameObject.transform.parent.gameObject;

                        Debug.Log("Name: " + name);
                        //Debug.Log("Hit: " + hit.collider.name);

                        ChangePanel(name, shopItemGO);
                    }
                }
            }
        }


        private void ChangePanel(string name, GameObject shopItemGO)
        {
            switch (name)
            {
                case "CowButton":
                    Debug.Log("Cows");

                    Debug.Log("PageList: " + pageList.Count);

                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow).ConvertAll(x => x.cardTemplate));

                    break;
                case "SeedButton":
                    Debug.Log("Seeds");

                    Debug.Log("PageList: " + pageList.Count);

                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Seed).ConvertAll(x => x.cardTemplate));

                    break;
                case "ClientButton":
                    Debug.Log("Client");


                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Customer).ConvertAll(x => x.cardTemplate));

                    break;
                case "TemporalButton":
                    Debug.Log("Temporal");


                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.PlaceMultiplier).ConvertAll(x => x.cardTemplate));

                    break;
                case "ZonesButton":
                    Debug.Log("Zones");

                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Helper).ConvertAll(x => x.cardTemplate));

                    break;
                case "CardDisplay Template":
                    Debug.Log("CardDisplay Template");
                    cardPreview.SetActive(true);

                    break;
                case "BuyButton":
                    Debug.Log("BuyButton");

                    shopItemGO.GetComponent<ShopItem>()?.TriggerCard();
                    shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();


                    break;

                default:
                    break;
            }
        }

        public void UpdateShopItemDisplay(List<CardTemplate> cardList)
        {
            int counter = 0;
            // todo: borrar los items anteriores
            foreach (CardTemplate card in cardList)
            {
                if (counter < pageList.Count)
                {
                    GameObject slot = pageList[counter];

                    GameObject createdItem = GameObject.Instantiate(shopItem, slot.transform);

                    createdItem.GetComponent<ShopItem>()?.UpdateDisplayData(card);
                    counter++;
                }
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
