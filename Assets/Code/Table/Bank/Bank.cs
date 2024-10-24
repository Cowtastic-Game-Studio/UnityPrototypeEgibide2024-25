using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour, IStorage
{
    public GameResource type { get; }

    public int maxResources => throw new System.NotImplementedException();

    public int level => throw new System.NotImplementedException();

    public int resource => throw new System.NotImplementedException();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
