using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{

    public class MarketManager : MonoBehaviour
    {
        //variables de la instancia para que cuando se inicie el escena
        private static MarketManager _intance;
        private static MarketManager GetItance => _intance;

        //objetos de los botones para comprar
        public Button cow, wheat, customer;
        public Button wheatPage, cowPage, CustomerPage, ButtonWheat, ButtonCowBase, ButtonCowHat, ButtonCowDInner, ButtonCowBlack, ButtonCustomer;
        public Button buyButton;

        //texto que muestra el muuney actual
        public Text muuneyCount;
        //dinero base con el cual se empieza esta prueba del mercado
        private int Muuney = 100;
        //coste de los productos disponibles en el  momento
        private int cowPrice = 300;
        private int customerPrice = 200;
        private int wheatPrice = 100;
        int CardPrice = 0;

        //eventos creados 3 para los botones y 1 para repartir el muunety
        public static UnityEvent winMuuney;
        public static UnityEvent GetCow;
        public static UnityEvent GetWheat;
        public static UnityEvent GetCustomer;


        //--listas de datos de las cartas
        public int[] CowCardData = new int[3];
        public int[] SeedCardData = new int[3];
        public int[] CustomerData = new int[3];


        [SerializeField] private int cowAvaialable, cowHatAvaialable, cowBlackAvaialable, cowDinnerAvaialable, customerAvailable, wheatAvailable = 0;


        void Start()
        {
            //revisa si hay instancia del mercado activa en caso de que haiga una la cierra y la vuelve abrir 
            if (_intance)
            {
                Destroy(gameObject);
            }
            else
            {
                _intance = this;
                DontDestroyOnLoad(gameObject);
            }



            //-- evento que da plata inicial en base a un random para hacer las pruebas, revisa si el evento esta inactivo antes de crearlo
            if (winMuuney == null)
            {
                winMuuney = new UnityEvent();
            }
            winMuuney.AddListener(WinMuuney);

            //--evento relacionados a comprar vaca, cuando inicia el mercado , revisa si el evento esta inactivo antes de crearlo
            if (GetCow == null)
            {
                GetCow = new UnityEvent();
            }
            GetCow.AddListener(LooseMuuneyCow);
            //--evento relacionado a comprar cliente,cuando inicia el mercado , revisa si el evento esta inactivo antes de crearlo        
            if (GetCustomer == null)
            {
                GetCustomer = new UnityEvent();
            }
            GetCustomer.AddListener(LooseMuuneyCustomer);
            //-- evento relacionado a comprar trigo , cuando inicia el mercado , revisa si el evento esta inactivo antes de crearlo      
            if (GetWheat == null)
            {
                GetWheat = new UnityEvent();
            }
            GetWheat.AddListener(LooseMuuneWheat);
            // llama al metodo para repartir el muuney y luego cancela su invoke para que no este activo todo el rato
            winMuuney.Invoke();
            CancelInvoke();

            //pone el muuney actual en el text que se paso como objeto
            //setea el dinero base para la prueba
            Muuney = 20;
            muuneyCount.text = Muuney.ToString();
            buyButton.interactable = false;

            // metodo que se encarga de desactivar o activar la capacidad de interactuar con los botones
            CheckMuney();

        }

        #region Eventos botones
        //Metodo del boton que se encarga de comprar manojos de trigo
        public void BuyWheat()
        {
            GetWheat.Invoke();
            CancelInvoke();
            CheckMuney();
        }

        //Metodo del boton que se encarga de comprar las vacas
        public void BuyCow()
        {
            GetCow.Invoke();
            CancelInvoke();
            CheckMuney();
        }

        //Metodo del boton que se encarga de comprar los clientes
        public void BuyCustomer()
        {
            GetCustomer.Invoke();
            CancelInvoke();
            CheckMuney();
        }

        //metodo que usa el evento de comprar trigo, el cual compara muuney con el precio y si este es igual o mayor permmite comprarlo
        private void LooseMuuneWheat()
        {
            if (Muuney >= wheatPrice)
            {
                Muuney = Muuney - wheatPrice;
                Debug.Log(Muuney);
                muuneyCount.text = Muuney.ToString();
                CheckMuney();
                CancelInvoke();
            }
        }

        //metodo que usa el evento de clientes trigo, el cual compara muuney con el precio y si este es igual o mayor permmite comprarlo
        private void LooseMuuneyCustomer()
        {
            if (Muuney >= customerPrice)
            {
                Muuney = Muuney - customerPrice;
                Debug.Log(Muuney);
                muuneyCount.text = Muuney.ToString();
                CheckMuney();
                CancelInvoke();
            }
        }

        //metodo que usa el evento de comprar vacas, el cual compara muuney con el precio y si este es igual o mayor permmite comprarlo

        private void LooseMuuneyCow()
        {
            if (Muuney >= cowPrice)
            {
                Muuney = Muuney - cowPrice;
                Debug.Log(Muuney);

                muuneyCount.text = Muuney.ToString();
                CheckMuney();
                CancelInvoke();
            }
        }

        #endregion
        //metodo usado por el evento de dar muuney al iniciar la ventana se puede ampliar para dar dinero de ventas y otros
        void WinMuuney()
        {
            int wonMuuney = UnityEngine.Random.Range(0, 2);
            if (wonMuuney == 1)
            {
                Muuney = 600;

            }

        }

        //metodo que compara el muuney actual con el precio y si es igual o mayor el boton es interactuable sino  no
        void CheckMuney()
        {
            if (Muuney <= 0)
            {
                wheat.interactable = false;
                customer.interactable = false;
                cow.interactable = false;
            }
            else
            {
                ///condicion si muuney inferior x cantidad desactiva los botones
                if (Muuney < 100)
                {
                    wheat.interactable = false;
                }
                if (Muuney < 200)
                {
                    customer.interactable = false;
                }
                if (Muuney < 300)
                {
                    cow.interactable = false;
                }
                ///condicion si muuney superio a x cantidad activa los botones
                if (Muuney >= 100)
                {
                    wheat.interactable = true;
                }
                if (Muuney >= 200)
                {
                    customer.interactable = true;
                }
                if (Muuney >= 300)
                {
                    cow.interactable = true;
                }
            }
        }


        // \/ HELL

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
        }

        public void GetSeedCardData(int[] cardSeed)
        {
            for (int a = 0; a < cardSeed.Length; a++)
            {
                SeedCardData[a] = cardSeed[a];
            }
            extractSeedCardData();
        }
        public void GetCusomterData(int[] cardCustomer)
        {
            for (int a = 0; a < cardCustomer.Length; a++)
            {
                CustomerData[a] = cardCustomer[a];
            }
            extractCustomerCardData();
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
            Muuney = Muuney - CardPrice;
            muuneyCount.text = Muuney.ToString();
            buyButton.interactable = false;
        }

        #endregion


    }
}