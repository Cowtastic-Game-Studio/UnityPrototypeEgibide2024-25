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
    
     public Button wheatPage,cowPage,CustomerPage,ButtonWheat,ButtonCowBase,ButtonCowHat,ButtonCowDInner,ButtonCowBlack,ButtonCustomer;
    public Button buyButton;
    public Text muuneyCount;
    private int Muuney;
    private int CardSelector = 5;

    public static UnityEvent GetCow;
    public static UnityEvent GetWheat;
    public static UnityEvent GetCustomer;        

    [SerializeField] private int cowAvaialable,cowHatAvaialable,cowBlackAvaialable,cowDinnerAvaialable,customerAvailable, wheatAvailable = 0;
    
    void Start()
    {
        if(_intance)
        {
            Destroy(gameObject);
        }
        else
        {
            _intance = this;
            DontDestroyOnLoad(gameObject);

        }

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
        InvokeRepeating(nameof(PrintValue), 0, 1);

    }
    void PrintValue()
    {
         Debug.Log("Value: " + CardSelector);
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
    public void BuyCowHatCow()
    {
        CardSelector = 2;
        Debug.Log("cow hat selected");
         Debug.Log(CardSelector);
        buyButton.interactable = true;
    }
    public void BuyCowBlackCow()
    {
        CardSelector = 4;
        Debug.Log("Cow black selected");
         Debug.Log(CardSelector);
        buyButton.interactable = true;
    }
    public void BuyCowDinnerCow()
    {
        CardSelector = 3;
        Debug.Log("Cow dinnerbone selected");
         Debug.Log(CardSelector);
        buyButton.interactable = true;
    }
    public void BuyCowCow()
    {
        CardSelector = 1;
        Debug.Log("default cow selected");
        Debug.Log(CardSelector);
        buyButton.interactable = true;

    }       

    public void getCardData(Enum cardtype)
    {
        
    } 

    public void BuyCard()
    {//revisa que carta fue seleccionada y la compra en caso de que haiga muuney disponible
     Debug.Log("card selected" + CardSelector);
        if(CardSelector == 1)
            {
                if(Muuney >1)
                    {
                    Muuney -= 1;
                    muuneyCount.text = Muuney.ToString();
                    buyButton.interactable = false   ;
                    Debug.Log(Muuney);
                    cowAvaialable++;
                }
                
            }
        
        else 
        {
            Debug.Log("ÑOWWWW"); 
            buyButton.interactable = false   ;     
            }
    }
      //--vacas
    #endregion

}
