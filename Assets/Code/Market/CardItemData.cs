using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItemData : MonoBehaviour
{
    
     string data = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string BaseWheat()
    {

    return data;
    }

    public string BaseCowData()
    {
        data = CardType.Cow.ToString();

        return data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
