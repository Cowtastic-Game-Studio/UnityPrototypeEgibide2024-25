using System.Collections.Generic;
using UnityEngine;

////borrar cuando esten definidos
//public enum GameResource
//{
//    None,
//    ActionPoints,
//    Muuney,
//    Milk,
//    Cereal
//}

//[System.Serializable]
//public struct ResourceAmount
//{
//    public GameResource resourceType;
//    public int resourceQuantity;
//}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/CardTemplate")]
public class CardTemplate : ScriptableObject
{
    [SerializeField]
    public new string name;
    //[SerializeField]
    //private new string cardType
    [SerializeField]
    public string description;

    [SerializeField]
    public Sprite artwork;
    [SerializeField]
    public Sprite baseCard;

    [SerializeField]
    public int actionPointsCost;
    [SerializeField]
    public int lifeCycleDays;

    [SerializeField]
    public List<ResourceAmount> requiredResources;
    [SerializeField]
    public List<ResourceAmount> producedResources;

    [SerializeField]
    public int marketCost;

    private void Print()
    {
        string sRequired = "";
        foreach (var resource in requiredResources)
        {
            sRequired += resource.resourceType + " : " + resource.resourceQuantity;
        }

        string sProduced = "";
        foreach (var resource in producedResources)
        {
            sProduced += resource.resourceType + " : " + resource.resourceQuantity + "/n";
        }


        Debug.Log(
            "Card Name: " + name + "\n" +
            "Description: " + description + "\n" +
            "Action Points Cost: " + actionPointsCost + "\n" +
            "Life Cycle (Days): " + lifeCycleDays + "\n" +
            "Required Resource: " + "\n" + sProduced +
            "Produced Resource: " + "\n" + sRequired +
            "Market Cost: " + marketCost
        );
    }
}

