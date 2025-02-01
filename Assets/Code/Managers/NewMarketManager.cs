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

        [SerializeField]
        private SlotList shopSlotList;

        private List<ShopItemData> shopItemsData;

        private List<GameObject> pageItemsList = new();

        private List<CardTemplate> cardList = new();

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

            pageItemsList = page.GetComponent<SlotList>()?.slotsList;
            UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow && x.isActive).ConvertAll(x => x.cardTemplate));

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del mouse
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask = LayerMask.GetMask("Market");

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider != null)
                    {
                        string name = hit.collider.name;
                        GameObject shopItemGO = hit.collider.gameObject.transform.parent.gameObject;

                        Debug.Log("Name: " + name);

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
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow && x.isActive).ConvertAll(x => x.cardTemplate));

                    break;
                case "SeedButton":
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Seed && x.isActive).ConvertAll(x => x.cardTemplate));

                    break;
                case "ClientButton":
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Customer && x.isActive).ConvertAll(x => x.cardTemplate));

                    break;
                case "TemporalButton":
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.PlaceMultiplier && x.isActive).ConvertAll(x => x.cardTemplate));

                    break;
                case "ZonesButton":
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Helper && x.isActive).ConvertAll(x => x.cardTemplate));

                    break;
                case "CardDisplayTemplate":
                    // TODO: Mostrar la información de la carta bien
                    Debug.Log("CardDisplay Template");
                    cardPreview.SetActive(true);
                    shopItemGO.GetComponent<ShopItem>()?.TriggerCard();

                    break;
                case "BuyButton":
                    // TODO: Logica de comprar la carta
                    shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();
                    break;

                default:
                    break;
            }
        }

        public void UpdateShopItemDisplay(List<CardTemplate> cardList)
        {
            ClearPageItemsList();

            int counter = 0;
            foreach (CardTemplate card in cardList)
            {
                if (counter < pageItemsList.Count)
                {
                    GameObject slot = pageItemsList[counter];

                    GameObject createdItem = GameObject.Instantiate(shopItem, slot.transform);

                    createdItem.GetComponent<ShopItem>()?.UpdateDisplayData(card);
                    counter++;
                }
            }

        }

        private void ClearPageItemsList()
        {
            foreach (var item in pageItemsList)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    if (item.transform.GetChild(i).gameObject != null)
                        Destroy(item.transform.GetChild(i).gameObject);
                }
            }
        }


        private void GetMuuney()
        {
            int totalMuuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney);

            Debug.Log($"Total Muuney: {totalMuuney}");

        }
    }
}
