using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MarketManager : MonoBehaviour
    {
        //--listas de datos de las cartas
        public int[] CowCardData = new int[3];
        public int[] SeedCardData = new int[3];
        public int[] CustomerData = new int[3];


        public Button wheatPage, cowPage, CustomerPage, ButtonWheat, ButtonCowBase, ButtonCowHat, ButtonCowDInner, ButtonCowBlack, ButtonCustomer;
        public Button buyButton;
        public Text muuneyCount;
        int CardPrice = 0;
        public static UnityEvent GetCow;
        public static UnityEvent GetWheat;
        public static UnityEvent GetCustomer;

        private CardType cardType;

        [SerializeField] private int cowAvaialable, cowHatAvaialable, cowBlackAvaialable, cowDinnerAvaialable, customerAvailable, wheatAvailable = 0;

        void Start()
        {
            //Carga el dinero actual del storage manager
            updateMuuney();
            buyButton.interactable = false;
        }

        public void updateMuuney()
        {
            //Carga el dinero actual del storage manager
            int Muuney = GameManager.Instance.Tabletop.StorageManager.GetResourceAmounts(GameResource.Muuney); ;
            muuneyCount.text = Muuney.ToString();

            cardType = CardType.None;
        }


        #region metodosMostrarCatalogo
        //estos meteodos son los reponsable de ocultar los productos del mercado
        public void ShowWheat()
        {
            //--vacas
            ButtonCowBase.gameObject.SetActive(false);
            ButtonCowDInner.gameObject.SetActive(false);
            ButtonCowBlack.gameObject.SetActive(false);
            ButtonCowHat.gameObject.SetActive(false);
            //--cultivos                 
            ButtonWheat.gameObject.SetActive(true);
            //--clientes
            ButtonCustomer.gameObject.SetActive(false);

            cardType = CardType.None;
        }

        public void ShowCow()
        {
            //--vacas
            ButtonCowBase.gameObject.SetActive(true);
            ButtonCowDInner.gameObject.SetActive(true);
            ButtonCowBlack.gameObject.SetActive(true);
            ButtonCowHat.gameObject.SetActive(true);
            //--cultivos  
            ButtonWheat.gameObject.SetActive(false);
            //--clientes
            ButtonCustomer.gameObject.SetActive(false);

            cardType = CardType.None;
        }

        public void ShowCustomer()
        {
            //--vacas
            ButtonCowBase.gameObject.SetActive(false);
            ButtonCowDInner.gameObject.SetActive(false);
            ButtonCowBlack.gameObject.SetActive(false);
            ButtonCowHat.gameObject.SetActive(false);
            //--cultivos     
            ButtonWheat.gameObject.SetActive(false);
            //--clientes
            ButtonCustomer.gameObject.SetActive(true);

            cardType = CardType.None;
        }

        #endregion
        #region metodosComprarCartas
        //metodos que se ejecutan para obtener los datos de los objetos de las cartas y efectuar la compra dle item
        public void GetCowCardData(int[] cardCow)
        {
            for (int a = 0; a < cardCow.Length; a++)
            {
                CowCardData[a] = cardCow[a];
            }
            extractCowCardData();

            cardType = CardType.Cow;
        }

        public void GetSeedCardData(int[] cardSeed)
        {
            for (int a = 0; a < cardSeed.Length; a++)
            {
                SeedCardData[a] = cardSeed[a];
            }
            extractSeedCardData();

            cardType = CardType.Seed;
        }
        public void GetCusomterData(int[] cardCustomer)
        {
            for (int a = 0; a < cardCustomer.Length; a++)
            {
                CustomerData[a] = cardCustomer[a];
            }
            extractCustomerCardData();

            cardType = CardType.Customer;
        }
        //metodos que extraen el valor del precio de la carta objetivo
        public void extractCowCardData()
        {
            CardPrice = CowCardData[1];
        }
        public void extractCustomerCardData()
        {
            CardPrice = CustomerData[0];
        }
        public void extractSeedCardData()
        {
            CardPrice = SeedCardData[0];
        }

        public void BuyCard()
        {
            if (GameManager.Instance.Tabletop.StorageManager.CheckMuuney(CardPrice))
            {
                int muuney = GameManager.Instance.Tabletop.StorageManager.WasteMuuney(CardPrice);

                muuneyCount.text = muuney.ToString();
                buyButton.interactable = false;

                GameManager.Instance.Tabletop.CardManager.buyCard(cardType);
            }
        }

        #endregion

    }
}
