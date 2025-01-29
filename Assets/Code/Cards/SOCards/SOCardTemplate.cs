using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/CardTemplate")]
    public class CardTemplate : ScriptableObject
    {
        public Sprite artwork;
        public Sprite baseCard;

        public CardType cardType;
        public new string name;
        public string description;

        [Header("Requests")]
        public int actionPointsCost;

        [Header("Life")]
        public int lifeCycleDays;

        [Header("Requests")]
        public List<ResourceAmount> requiredResources;
        public List<ResourceAmount> producedResources;

        [Header("Market")]
        public int marketCost;

        [Header("Upgrade")]
        public CardType targetCardType = CardType.None;

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
