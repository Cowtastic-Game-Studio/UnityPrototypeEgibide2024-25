using System.Collections.Generic;

using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class NewMarketManager : MonoBehaviour
    {
        [Header("Market Buttons")]
        [SerializeField] private List<GameObject> buttonList;

        [Header("Scripts")]
        [SerializeField] private InitMarketCards InitMarketCards;
        [SerializeField] private SlotList slotList;

        [Header("Game Objects")]
        [SerializeField] private GameObject page;
        [SerializeField] private GameObject shopItem;
        [SerializeField] private GameObject cardPreview;

        /// <summary>
        /// List of all the shop items/cards
        /// </summary>
        private List<ShopItemData> shopItemsData;

        /// <summary>
        /// The slots in the page
        /// </summary>
        private List<GameObject> pageItemsList = new();

        public float discountPercentage = 1f;


        /// <summary>
        /// Actual list of cards of a specific type
        /// </summary>
        private List<CardTemplate> actualCardList = new();

        private int counter = 0;
        private int nActualItems;

        // Start is called before the first frame update
        void Start()
        {
            cardPreview.SetActive(false);

            InitMarketCards.Initialize();
            shopItemsData = InitMarketCards.ShopItemsData;

            pageItemsList = page.GetComponent<SlotList>()?.slotsList;
            UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow && x.isActive).ConvertAll(x => x.cardTemplate));
        }

        // Update is called once per frame
        public void UpdateMarket()
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

        public void RestartMarket()
        {
            cardPreview.SetActive(false);
            Debug.Log(shopItemsData);

            UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow && x.isActive).ConvertAll(x => x.cardTemplate));
        }

        /// <summary>
        /// Checks the day and unlocks the cards
        /// </summary>
        /// <param name="day"></param>
        public void CheckDay(int day)
        {
            shopItemsData.ForEach(x => x.CheckUnlock(day));
        }

        #region Private Methods

        /// <summary>
        /// Change the panel to show the correct cards
        /// </summary>
        /// <param name="name">Collider name</param>
        /// <param name="shopItemGO">Shop item game object to trigger its methods</param>
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
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => (x.cardTemplate.cardType == CardType.PlaceMultiplier || x.cardTemplate.cardType == CardType.PlaceActivator || x.cardTemplate.cardType == CardType.Helper) && x.isActive).ConvertAll(x => x.cardTemplate));
                    break;

                case "ZonesButton":
                    // TODO: Hacer SO de zonas
                    UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.None && x.isActive).ConvertAll(x => x.cardTemplate));
                    break;

                case "CardDisplayTemplate":
                    // TODO: Mostrar la información de la carta bien
                    Debug.Log("CardDisplay Template");
                    cardPreview.SetActive(true);
                    //shopItemGO.GetComponent<ShopItem>()?.TriggerCard();
                    ShopItem clickItem = shopItemGO.GetComponent<ShopItem>();
                    if (clickItem)
                        cardPreview.GetComponent<CardDisplay>()?.UpdateDisplayAndMat(clickItem.getCardTemplate(), false);
                    break;

                case "BuyButton":
                    ShopItem shopItem = shopItemGO.GetComponent<ShopItem>();
                    shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();
                    cardPreview.SetActive(false);
                    break;

                case "NextButton":
                    slotList.NextPage();
                    ClearPageItemsList();
                    CreateShopItems(false);
                    cardPreview.SetActive(false);
                    break;

                case "PreviousButton":
                    slotList.PreviousPage();
                    counter -= pageItemsList.Count + nActualItems;
                    ClearPageItemsList();
                    CreateShopItems(false);
                    cardPreview.SetActive(false);
                    break;

                default:
                    break;
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

        /// <summary>
        /// Updates the shop item display
        /// </summary>
        /// <param name="cardList">The list to show</param>
        private void UpdateShopItemDisplay(List<CardTemplate> cardList)
        {
            ClearPageItemsList();
            actualCardList = cardList;
            counter = 0;

            CreateShopItems(false);
            cardPreview.SetActive(false);

            slotList.totalPage = Mathf.CeilToInt((float)cardList.Count / 8);
        }

        private void CreateShopItems(bool isNextPage)
        {
            for (int i = 0; i < pageItemsList.Count; i++)
            {
                if (counter < actualCardList.Count)
                {
                    GameObject slot = pageItemsList[i];
                    GameObject createdItem = GameObject.Instantiate(shopItem, slot.transform);
                    createdItem.name = actualCardList[counter].name;

                    createdItem.GetComponent<ShopItem>()?.UpdateDisplayData(actualCardList[counter], discountPercentage);
                    counter++;
                    if (isNextPage)
                    {
                        nActualItems++;
                    }
                }
            }
        }
        #endregion  

    }
}
