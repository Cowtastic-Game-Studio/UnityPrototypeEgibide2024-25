using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StatisticsManager : MonoBehaviour
    {
        private List<Statistic> statsList = new();

        public static StatisticsManager Instance { get; private set; }
        public UnityEvent OnStatisticChanged;

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

        private void Initialized()
        {
            OnStatisticChanged = new UnityEvent();

            // Cards
            statsList.Add(new Statistic(StatisticType.CowsMilked, CardType.Cow, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.SeedsHarvested, CardType.Seed, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.CustomersServed, CardType.Customer, GameResource.None, 0, true));

            // Temporary cards
            statsList.Add(new Statistic(StatisticType.PlaceActivatorCards, CardType.PlaceActivator, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.PlaceMultiplierCards, CardType.PlaceMultiplier, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.HelperCards, CardType.Helper, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.TemporaryUsedCards, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.CardsTotalUsed, CardType.None, GameResource.None, 0, true));

            // Market
            statsList.Add(new Statistic(StatisticType.CardsDiscarded, CardType.None, GameResource.None, 0, false));
            statsList.Add(new Statistic(StatisticType.CardsPurchased, CardType.None, GameResource.None, 0, false));
            statsList.Add(new Statistic(StatisticType.ZonesPurchased, CardType.None, GameResource.None, 0, false));
            statsList.Add(new Statistic(StatisticType.ZonesWithCardsPurchased, CardType.None, GameResource.None, 0, false));

            // Resources adquired
            statsList.Add(new Statistic(StatisticType.MilkTotalAdquired, CardType.None, GameResource.Milk, 0, false));
            statsList.Add(new Statistic(StatisticType.CerealTotalAdquired, CardType.None, GameResource.Cereal, 0, false));
            statsList.Add(new Statistic(StatisticType.MuuneyTotalAdquired, CardType.None, GameResource.Muuney, 0, false));

            // Resources used
            statsList.Add(new Statistic(StatisticType.MilkTotalUsed, CardType.None, GameResource.Milk, 0, true));
            statsList.Add(new Statistic(StatisticType.CerealTotalUsed, CardType.None, GameResource.Cereal, 0, true));
            statsList.Add(new Statistic(StatisticType.MuuneyTotalUsed, CardType.None, GameResource.Muuney, 0, true));
            statsList.Add(new Statistic(StatisticType.APUsed, CardType.None, GameResource.ActionPoints, 0, true));

        }

        public void RaiseOnStatisticChanged()
        {
            OnStatisticChanged?.Invoke();
        }

        public void UpdateByType(ICard card)
        {
            CardBehaviour cardBehaviour = card as CardBehaviour;

            foreach (var item in statsList)
            {
                if (item.CardType == cardBehaviour.Type)
                {
                    GetStat(StatisticType.CardsTotalUsed).Uses += 1;

                    // TODO Mirar donde se llama a las temporales
                    if (cardBehaviour.Type == CardType.PlaceActivator || cardBehaviour.Type == CardType.PlaceMultiplier || cardBehaviour.Type == CardType.Helper)
                    {
                        GetStat(StatisticType.TemporaryUsedCards).Uses += 1;
                    }
                    UpdateStatistic(item, 1);
                }
            }

        }

        public void UpdateByResource(GameResource resource, int quantity, bool isUsed)
        {
            foreach (var item in statsList)
            {
                if (item.Resource == resource && item.IsUsed == isUsed)
                {
                    UpdateStatistic(item, quantity);
                }
            }
        }

        public Statistic GetStat(StatisticType type)
        {
            var stat = statsList.FirstOrDefault((s) => s.StatType == type);

            if (stat == null)
                throw new Exception(String.Format("Statistic with type {0} not found", type));

            return stat;
        }

        private void UpdateStatistic(Statistic stat, int quantity)
        {
            stat.Uses += quantity;

            //Debug.Log(stat.StatType.GetEnumString() + ": " + stat.Uses);
            //Debug.Log(GetStat(StatisticType.CardsTotalUsed).StatType.GetEnumString() + ": " + GetStat(StatisticType.CardsTotalUsed).Uses);
            //Debug.Log(GetStat(StatisticType.TemporaryUsedCards).StatType.GetEnumString() + ": " + GetStat(StatisticType.TemporaryUsedCards).Uses);

            RaiseOnStatisticChanged();
        }
    }
}
