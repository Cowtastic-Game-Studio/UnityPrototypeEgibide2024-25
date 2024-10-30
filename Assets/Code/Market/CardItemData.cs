using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardItemData : MonoBehaviour
{
    //objetos de los bototnes y clases que se usan en esta clase
    //--boton de comprar el cualquier se activa al pasar los datos 
    public Button buyCard;
    //-- clase de test  de cada tipo de objeto
    public MarketManager market ;
    public CowCardClass ccclass;
    public   SeedsCardbase scb;

    public CustomersCardBase ccb;
      
    public void BaseCowData()
        {
        //variables de la carta las cuales de usan para obtener los datos base
            int type =  ccclass.baseCow[0];
            int price =  ccclass.baseCow[1];
            int food =  ccclass.baseCow[2];
        //lista creada para passar los datos
            int[] cow = new int[3];

            cow[0] = type;
            cow[1] = price;
            cow[2] = food;
        //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetCowCardData(cow);

            buyCard.interactable = true;           
        }
    public void HatCowData()
        {  
            //variables de la carta las cuales de usan para obtener los datos base     
            int type =   ccclass.hatCow[0];
            int price =  ccclass.hatCow[1];
            int food =  ccclass.hatCow[2];
            //lista creada para passar los datos
            int[] cow = new int[3];

            cow[0] = type;
            cow[1] = price;
            cow[2] = food;
            //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetCowCardData(cow);

            buyCard.interactable = true;

        }
    public void BLackCowData()
        {
            //variables de la carta las cuales de usan para obtener los datos base
            int type =   ccclass.blackCow[0];
            int price =  ccclass.blackCow[1];
            int food =  ccclass.blackCow[2];
            //lista creada para passar los datos
            int[] cow = new int[3];

            cow[0] = type;
            cow[1] = price;
            cow[2] = food;
            //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetCowCardData(cow);

            buyCard.interactable = true;

        }
    public void DinnerCowData()
        {
            //variables de la carta las cuales de usan para obtener los datos base
            int type =   ccclass.dinnerCow[0];
            int price =  ccclass.dinnerCow[1];
            int food =  ccclass.dinnerCow[2];
            //lista creada para passar los datos
            int[] cow = new int[3];

            cow[0] = type;
            cow[1] = price;
            cow[2] = food;
            //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetCowCardData(cow);

            buyCard.interactable = true;
        }

    public void GetSeedWheatData()
        {
            //variables de la carta las cuales de usan para obtener los datos base
            int price = scb.wheatSeed[0];
            int PAcost = scb.wheatSeed[1];
            int TurnDuracion = scb.wheatSeed[2];
            //lista creada para passar los datos
            int[] baseSeed  = new int[3];
            baseSeed[0] = price;
            baseSeed[1] = PAcost;
            baseSeed[2] = TurnDuracion;
            //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetSeedCardData(baseSeed);

            buyCard.interactable = true;
        }

    public void CustomerData()
        {
            //variables de la carta las cuales de usan para obtener los datos base
            int price = ccb.Customer[0];
            //lista creada para passar los datos
            int[] customer = new int[1];
            customer[0] = price;
            //llamada al manager del mercado para pasarle los datos de la carta seleccioanda
            market.GetCusomterData(customer);

            buyCard.interactable = true;
        }


    
}
