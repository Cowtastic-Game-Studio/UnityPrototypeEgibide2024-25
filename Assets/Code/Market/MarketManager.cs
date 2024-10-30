using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static MarketManager _intance;
     private static MarketManager GetItance =>_intance;

     //--listas de datos de las cartas
     public  int[] CowCardData = new int[3];
     public int[] SeedCardData = new int[3];
     public int[] CustomerData = new int[3];
  
    
     public Button wheatPage,cowPage,CustomerPage,ButtonWheat,ButtonCowBase,ButtonCowHat,ButtonCowDInner,ButtonCowBlack,ButtonCustomer;
    public Button buyButton;
    public Text muuneyCount;
    private int Muuney;
    private int CardSelector = 5;

    int CardPrice = 0;

    public static UnityEvent GetCow;
    public static UnityEvent GetWheat;
    public static UnityEvent GetCustomer;        

    [SerializeField] private int cowAvaialable,cowHatAvaialable,cowBlackAvaialable,cowDinnerAvaialable,customerAvailable, wheatAvailable = 0;
    
    void Start()
    {
        /*
        if(_intance)
        {
            Destroy(gameObject);
        }
        else
        {
            _intance = this;
            DontDestroyOnLoad(gameObject);

        }
*/
        Muuney = 20;
        muuneyCount.text = Muuney.ToString();
        buyButton.interactable = false;  


        if (GetCow == null)
        {
            GetCow = new UnityEvent();
        }    
       //--evento relacionado a comprar cliente,cuando inicia el mercado , revisa si el evento esta inactivo antes de crearlo        
        if (GetCustomer == null)
        {
            GetCustomer = new UnityEvent();
        }
        //-- evento relacionado a comprar trigo , cuando inicia el mercado , revisa si el evento esta inactivo antes de crearlo      
        if (GetWheat == null)
        {
            GetWheat = new UnityEvent();
        }
        

    }


    #region metodosMostrarCatalogo
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
       ButtonCowBase.gameObject.SetActive(true);

        ButtonCowDInner.gameObject.SetActive(true);
        ButtonCowBlack.gameObject.SetActive(true);

        ButtonCowHat.gameObject.SetActive(true);
        
        ButtonWheat.gameObject.SetActive(false);
        ButtonCustomer.gameObject.SetActive(false);
    }

    public void ShowCustomer()
    {
          ButtonCowBase.gameObject.SetActive(false);

        ButtonCowDInner.gameObject.SetActive(false);
        ButtonCowBlack.gameObject.SetActive(false);

        ButtonCowHat.gameObject.SetActive(false);
        
        ButtonWheat.gameObject.SetActive(false);
        ButtonCustomer.gameObject.SetActive(true);
    }
    #endregion
    #region metodosComprarCartas

    //--vacas
      public void GetCowCardData(int[] cardCow)
        {
            for(int a  =0; a < cardCow.Length; a ++ )
                {
                    CowCardData[a] = cardCow[a];

                    Debug.Log(CowCardData[a]);              
                
                }
                extractCowCardData(); 
            
        }

         public void GetSeedCardData(int[] cardSeed)
        {
            for(int a  =0; a < cardSeed.Length; a ++ )
                {
                    SeedCardData[a] = cardSeed[a];                 
                
                
                }
                extractSeedCardData(); 
            
        }
    public void GetCusomterData(int[] cardCustomer)
    {
            for(int a  =0; a < cardCustomer.Length; a ++ )
                {
                    CustomerData[a] = cardCustomer[a];                 
                
                
                }
                extractCustomerCardData(); 
    }

    public void extractCowCardData()
    {
       CardPrice =  CowCardData[1];
       
    }
     public void extractCustomerCardData()
    {
       CardPrice =  CustomerData[0];
       
    }
    public void extractSeedCardData()
    {
        CardPrice = SeedCardData[0];
    }

   public void BuyCard()
    {
        
        if(CowCardData !=null)
        {
            
        
            Muuney = Muuney -CardPrice;

            Debug.Log(CardPrice);       
             
        
            muuneyCount.text = Muuney.ToString();
            buyButton.interactable =false;
        
        }
        else
        {
            Debug.Log("ñow");
            buyButton.interactable =false;

        }
    }
      //--vacas
    #endregion

}
