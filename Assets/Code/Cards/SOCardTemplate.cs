using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
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

        public void Print()
        {
            string sRequired = "Required Resources:\n";
            for (int i = 0; i < requiredResources.Count; i++)
            {
                sRequired += $"{i + 1}. {requiredResources[i].resourceType} : {requiredResources[i].resourceQuantity}\n";
            }

            string sProduced = "Produced Resources:\n";
            for (int i = 0; i < producedResources.Count; i++)
            {
                sProduced += $"{i + 1}. {producedResources[i].resourceType} : {producedResources[i].resourceQuantity}\n";
            }

            Debug.Log(
                $"Card Name: {name}\n" +
                $"Description: {description}\n" +
                $"Action Points Cost: {actionPointsCost}\n" +
                $"Life Cycle (Days): {lifeCycleDays}\n" +
                $"{sRequired}" +
                $"{sProduced}" +
                $"Market Cost: {marketCost}"
            );
        }
    }
}
