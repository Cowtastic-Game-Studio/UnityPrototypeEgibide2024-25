using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MarketManager : MonoBehaviour
    {
        //objetos de los botones para comprar
        public Button cow, wheat, customer;
        //texto que muestra el muuney actual
        public Text muuneyCount;
        //dinero base con el cual se empieza esta prueba del mercado
        private int Muuney = 100;
        //coste de los productos disponibles en el  momento
        private int cowPrice = 300;
        private int customerPrice = 200;
        private int wheatPrice = 100;

        //eventos creados 3 para los botones y 1 para repartir el muunety
        public static UnityEvent winMuuney;
        public static UnityEvent GetCow;
        public static UnityEvent GetWheat;
        public static UnityEvent GetCustomer;


        void Start()
        {
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
            muuneyCount.text = Muuney.ToString();
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
    }
}