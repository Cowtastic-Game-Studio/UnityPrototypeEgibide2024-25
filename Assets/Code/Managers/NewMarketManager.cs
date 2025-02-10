using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private BuyButtonBehaviour normalPriceButton;
        [SerializeField] private BuyButtonBehaviour discountPriceButton;

        /// <summary>
        /// List of all the shop items/cards
        /// </summary>
        private List<ShopItemData> shopItemsData;

        /// <summary>
        /// The slots in the page
        /// </summary>
        private List<GameObject> pageItemsList = new();

        public float discountPercentage = 1f;
        public int discountPrice = 5;


        /// <summary>
        /// Actual list of cards of a specific type
        /// </summary>
        private List<CardTemplate> actualCardList = new();

        private int counter = 0;
        private int nActualItems;

        // Start is called before the first frame update
        void Start()
        {
            ShowHideCardPreviewZone(false);

            InitMarketCards.Initialize();
            shopItemsData = InitMarketCards.ShopItemsData;

            pageItemsList = page.GetComponent<SlotList>()?.slotsList;
            //UpdateShopItemDisplay(shopItemsData.FindAll(x => x.cardTemplate.cardType == CardType.Cow && x.isActive).ConvertAll(x => x.cardTemplate));
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

                        //Debug.Log("Name: " + name);

                        ChangePanel(name, shopItemGO);
                    }
                }
            }
        }

        public void RestartMarket()
        {
            ShowHideCardPreviewZone(false);
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
                    OnShopItemClicked(shopItemGO);

                    break;
                case "BuyButton":
                    ShopItem shopItem = shopItemGO.GetComponent<ShopItem>();
                    shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();
                    cardPreview.SetActive(false);
                    break;

                case "NormalBuyButton":
                    OnBuyButtonClicked(shopItemGO);
                    break;

                case "DiscountBuyButton":
                    OnDiscountBuyButtonClicked(shopItemGO);
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

            slotList.totalPage = Mathf.CeilToInt((float) cardList.Count / 8);

            //Limpia la carta previsualizada
            ShowHideCardPreviewZone(false);
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

        /// <summary>
        /// Obtiene el item clicado y muestra la informacion
        /// </summary>
        private void OnShopItemClicked(GameObject shopItemGO)
        {
            //ShopItem shopItem = shopItemGO.GetComponent<ShopItem>();
            //shopItem?.TriggerCard();
            ShopItem clickItem = shopItemGO.GetComponent<ShopItem>();
            if (clickItem)
                cardPreview.GetComponent<CardDisplay>()?.UpdateDisplayAndMat(clickItem.getCardTemplate(), false);

            ShowHideCardPreviewZone(true);

            List<GameObject> allCardsList = GameManager.Instance.Tabletop.CardManager.getAllCardsList();
            List<GameObject> temporalCardsList = new();

            switch (clickItem.getCardTemplate().name)
            {
                case "Garden+":
                    temporalCardsList = allCardsList.Where(x => x.GetComponent<CardDisplay>().name == "MuussiveGarden").ToList();
                    break;
                case "Stable+":
                    temporalCardsList = allCardsList.Where(x => x.GetComponent<CardDisplay>().name == "MuussiveStable").ToList();
                    break;
                case "Tavern+":
                    temporalCardsList = allCardsList.Where(x => x.GetComponent<CardDisplay>().name == "MuussiveTavern").ToList();
                    break;
                default:
                    SetCardPrices(clickItem.card.cost, false, 0);
                    break;
            }

            if (temporalCardsList.Count > 0)
            {
                SetCardPrices(clickItem.card.cost, true, discountPrice);
            }

        }

        private void OnBuyButtonClicked(GameObject shopItemGO)
        {
            ShopItem shopItem = shopItemGO.GetComponent<ShopItem>();
            shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();

            //TODO: Ejecutar la compra de la carta
            /**
             * 1. Otener el precio(hacer unas de las 3 cosas)
             * * Se puede obtener del shop item, y hacer los mismos calculos que en SetCardPrices
             * * Se pueden obtener del texto del boton pulsado(lo mas sencillo)
             * * Pasar la funcion del SetCardPrices, pasarlo al shopItem(lo mas limpio) y llamarle de otra forma(GetCardPrice y GetDiscountPrices)
             * 2. Comprobar que tiene el dinero
             *  * Si no tiene mostrar error
             * 3. Restar el dinero
             * 4. Añadir la nueva carta al mazo
             */
        }

        private void OnDiscountBuyButtonClicked(GameObject shopItemGO)
        {
            ShopItem shopItem = shopItemGO.GetComponent<ShopItem>();
            shopItemGO.GetComponent<ShopItem>()?.TriggerPrice();

            //TODO: Ejecutar la compra de la carta con descuento
            /**
             * 1. Usar la funcion FindDiscountForCard o guardado la carta de descuento vinculada al shop item o aqui en una variable(RECORDAR LIMPIARLA CADA VEZ QUE SE seleccione otra carta)
             * 2. Comprobar que tiene el dinero
             *  * Si no tiene mostrar error
             * 3. Restar el dinero
             * 4. Añadir la nueva carta al mazo
             */
        }



        /// <summary>
        /// Activa o desactiva 
        /// </summary>
        /// <param name="active"></param>
        private void ShowHideCardPreviewZone(bool active)
        {
            cardPreview.SetActive(active);
            normalPriceButton.SetActive(active);
            discountPriceButton.SetActive(active);
        }

        /// <summary>
        /// Asigna el precio de las cartas
        /// </summary>
        /// <param name="cardPrice"></param>
        /// <param name="hasDiscount"></param>
        /// <param name="discount"></param>
        private void SetCardPrices(int cardPrice, bool hasDiscount, int discount)
        {
            float price, discountPrice;

            //Comprueba si es black friday
            Debug.Log("IsVacFriday: " + GameManager.Instance.GameCalendar.IsVacFriday());
            if (GameManager.Instance.GameCalendar.IsVacFriday())
            {
                //Rebaja a la mitad el precio
                price = Utils.RoundMuuney(cardPrice / 2f);

                //Le pone un color especial
                normalPriceButton.SetVacFridayColor();
            }
            else //Deja el precio normal
            {
                price = cardPrice;
                normalPriceButton.SetNormalColor();
            }

            //Asigna el precio normal
            normalPriceButton.SetPrice((int) price);

            //Si tiene descuento
            if (hasDiscount)
            {
                Debug.Log(cardPrice);
                //Calcula el descuento
                discountPrice = cardPrice - discount;

                Debug.Log(discountPrice);
                //Aplica el precio con descuento
                discountPriceButton.SetPrice((int) discountPrice);
                discountPriceButton.SetActive(true);
            }
            else//Si no existe descuento
            {
                //Oculta el boton con descuento
                discountPriceButton.SetActive(false);
            }

        }


        #endregion  

    }
}
