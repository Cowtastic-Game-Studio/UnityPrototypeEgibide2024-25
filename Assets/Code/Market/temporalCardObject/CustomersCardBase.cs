using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersCardBase : MonoBehaviour
{
    //--Clase base para tener datos durante la creacion del mercado
    //listas de cada y sus parametros asociados a esa listaa
        public int[] Customer = new int[1];

        [SerializeField] int price = 3;
    void Awake()
    {
        //mete los datos en la lista para que sean accedidos por las otras clase que necesiten la lista
        Customer[0] = price;        
    }

}
