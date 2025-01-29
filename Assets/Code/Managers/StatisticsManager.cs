using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StatisticsManager : MonoBehaviour
    {


        public static StatisticsManager Instance { get; private set; }
        //public event Action OnCardClickedGlobal;

        private List<Statistic> statsList = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Initialized();
        }

        public void UpdateStatistic(Statistic stat, int quantity)
        {
            stat.Uses += quantity;

            foreach (var item in statsList)
            {
                Debug.Log(item.Title + ": " + item.Uses);
            }
        }

        private void Initialized()
        {
            statsList.Add(new Statistic(StatisticType.CowsMilked, "Milked cows", CardType.Cow, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.SeedsUsed, "Used seeds", CardType.Seed, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.CustomersServed, "Served customers", CardType.Customer, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.TemporaryUsedCards, "Temporary used cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.CardsPurchased, "Purchased cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.ZonesPurchased, "Purchased zones", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.ZonesWithCardsPurchased, "Purchased zones with cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.CardsDiscarded, "Discarded cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.CardsUsed, "Used cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic(StatisticType.MilkTotalAdquired, "Total adquired milk", CardType.None, GameResource.Milk, 0));
            statsList.Add(new Statistic(StatisticType.CerealTotalAdquired, "Total adquired cereal", CardType.None, GameResource.Cereal, 0));
            statsList.Add(new Statistic(StatisticType.MuuneyTotalAdquired, "Total adquired muuney", CardType.None, GameResource.Muuney, 0));
            statsList.Add(new Statistic(StatisticType.APUsed, "Used action points", CardType.None, GameResource.ActionPoints, 0));

        }

        public void UpdateByType(ICard card)
        {

            CardBehaviour cardBehaviour = card as CardBehaviour;

            Debug.Log(cardBehaviour.Type);

            foreach (var item in statsList)
            {
                if (item.Type == cardBehaviour.Type)
                {
                    UpdateStatistic(item, 1);
                }
            }

        }

        public void UpdateByResource(GameResource resource, bool isAdding, int quantity)
        {
            foreach (var item in statsList)
            {
                if (item.Resource == resource)
                {

                    UpdateStatistic(item, quantity);

                }
            }
        }




        public UnityEvent OnStatisticChanged = new UnityEvent();


        public void RaiseOnStatisticChanged()
        {
            OnStatisticChanged?.Invoke();
        }

        public Statistic GetStat(StatisticType type)
        {
            var stat = statsList.FirstOrDefault((s) => s.StatType == type);

            if (stat == null)
                throw new Exception(String.Format("Statistic with type {0} not found", StatisticType.CowsMilked));

            return stat;
        }
    }
}
