using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Button wheatPage,cowPage,CustomerPage,ButtonWheat,ButtonCow,ButtonCustomer;
    [SerializeField] private int cowAvaialable,customerAvailable, wheatAvailable;
    



    
    void Start()
    {
    cowAvaialable = 4;
    customerAvailable = 2;
    wheatAvailable = 12;    
    }


    public void ShowWheat()
    {
        ButtonCow.gameObject.SetActive(false);
        ButtonWheat.gameObject.SetActive(true);
        ButtonCustomer.gameObject.SetActive(false);

    }

    public void ShowCow()
    {
        ButtonCow.gameObject.SetActive(true);
        ButtonWheat.gameObject.SetActive(false);
        ButtonCustomer.gameObject.SetActive(false);
    }

    public void ShowCustomer()
    {
          ButtonCow.gameObject.SetActive(false);
        ButtonWheat.gameObject.SetActive(false);
        ButtonCustomer.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
