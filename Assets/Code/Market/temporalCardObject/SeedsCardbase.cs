using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsCardbase : MonoBehaviour
{
    //--Clase base para tener datos durante la creacion del mercado
    //listas y parametros asociados a esa lista
    public int[] wheatSeed = new int[3];
    [SerializeField] private int price = 1;
    [SerializeField] private int PAcost = 1;
    [SerializeField] private int TurnDuracion = 1;



void Awake()
    {
        //mete los datos en la lista para que sean accedidos por las otras clase que necesiten la lista
        wheatSeed[0] = price;
        wheatSeed[1] = PAcost;
        wheatSeed[2] = TurnDuracion;
    }




}
