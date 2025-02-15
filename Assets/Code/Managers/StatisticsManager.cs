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

            statsList.Add(new Statistic(StatisticType.CardsPlaced, CardType.None, GameResource.None, 0, true));
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
            statsList.Add(new Statistic(StatisticType.CardsDiscarded, CardType.None, GameResource.None, 0, false)); //TODO
            statsList.Add(new Statistic(StatisticType.CardsPurchased, CardType.None, GameResource.None, 0, false));
            statsList.Add(new Statistic(StatisticType.ZonesUpgradePurchased, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.ZonesWithCardsPurchased, CardType.None, GameResource.None, 0, false));

            // Resources adquired
            statsList.Add(new Statistic(StatisticType.MilkTotalAcquired, CardType.None, GameResource.Milk, 0, false));
            statsList.Add(new Statistic(StatisticType.CerealTotalAcquired, CardType.None, GameResource.Cereal, 0, false));
            statsList.Add(new Statistic(StatisticType.MuuneyTotalAcquired, CardType.None, GameResource.Muuney, 0, false));

            // Resources used
            statsList.Add(new Statistic(StatisticType.MilkTotalUsed, CardType.None, GameResource.Milk, 0, true));
            statsList.Add(new Statistic(StatisticType.CerealTotalUsed, CardType.None, GameResource.Cereal, 0, true));
            statsList.Add(new Statistic(StatisticType.MuuneyTotalUsed, CardType.None, GameResource.Muuney, 0, true));
            statsList.Add(new Statistic(StatisticType.APUsed, CardType.None, GameResource.ActionPoints, 0, true));

            // Mission globals
            statsList.Add(new Statistic(StatisticType.StableCountUpgrade, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.GardenCountUpgrade, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.TavernCountUpgrade, CardType.None, GameResource.None, 0, true));

            statsList.Add(new Statistic(StatisticType.FridgeCountUpgrade, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.SiloCountUpgrade, CardType.None, GameResource.None, 0, true));

            statsList.Add(new Statistic(StatisticType.StableFull, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.FarmFull, CardType.None, GameResource.None, 0, true));
            statsList.Add(new Statistic(StatisticType.ShopFull, CardType.None, GameResource.None, 0, true));

            statsList.Add(new Statistic(StatisticType.EventsCompleted, CardType.None, GameResource.None, 0, true));
        }

        public void RaiseOnStatisticChanged()
        {
            OnStatisticChanged?.Invoke();
        }

        public void CheckPlayedCards()
        {
            int count = GameManager.Instance.Tabletop.CardManager.PlayedDeck.Cards.Count();
            Statistic statistics = statsList.Find(x => x.StatType == StatisticType.CardsPlaced);
            UpdateStatistic(statistics, count);
        }

        public void UpdateByType(ICard card)
        {
            CardBehaviour cardBehaviour = card as CardBehaviour;

            foreach (var item in statsList)
            {
                if (item.CardType == cardBehaviour.Type)
                {
                    GetStat(StatisticType.CardsTotalUsed).Uses += 1;

                    if (cardBehaviour.Type == CardType.PlaceActivator || cardBehaviour.Type == CardType.PlaceMultiplier || cardBehaviour.Type == CardType.Helper)
                    {
                        GetStat(StatisticType.TemporaryUsedCards).Uses += 1;
                    }
                    UpdateStatistic(item, 1);
                }
            }
        }

        public void UpdateByResource(GameResource resource, int quantity, bool isConsumed)
        {
            foreach (var item in statsList)
            {
                if (item.Resource == resource && item.IsUsed == isConsumed)
                {
                    UpdateStatistic(item, quantity);
                }
            }
        }

        public void UpdateByBuyedCard(CardType cardType)
        {
            switch (cardType)
            {
                //case CardType.Cow:
                //    Debug.Log("La carta es de tipo Cow.");
                //    break;
                //case CardType.Seed:
                //    Debug.Log("La carta es de tipo Seed.");
                //    break;
                //case CardType.Customer:
                //    Debug.Log("La carta es de tipo Customer.");
                //    break;
                case CardType.PlaceActivator:
                    UpdateByStatisticType(StatisticType.ZonesWithCardsPurchased, 1);
                    break;
                    //case CardType.PlaceMultiplier:
                    //    Debug.Log("La carta es de tipo PlaceMultiplier.");
                    //    break;
                    //case CardType.Helper:
                    //    Debug.Log("La carta es de tipo Helper.");
                    //    break;
                    //case CardType.None:
                    //    Debug.Log("No hay carta.");
                    //    break;
            }

        }

        public void UpdateByBuyedZone(CardType targedCardType, bool hasDiscount)
        {
            switch (targedCardType)
            {
                case CardType.Cow:
                    UpdateByStatisticType(StatisticType.StableCountUpgrade, 1);
                    break;
                case CardType.Seed:
                    UpdateByStatisticType(StatisticType.GardenCountUpgrade, 1);
                    break;
                case CardType.Customer:
                    UpdateByStatisticType(StatisticType.TavernCountUpgrade, 1);
                    break;
            }

            if (hasDiscount)
            {
                UpdateByStatisticType(StatisticType.ZonesWithCardsPurchased, 1);
            }
            else
            {
                UpdateByStatisticType(StatisticType.ZonesUpgradePurchased, 1);
            }
        }

        public void UpdateByBuyedZone(GameResource targedCardType)
        {
            switch (targedCardType)
            {
                //case GameResource.ActionPoints:
                //    UpdateByStatisticType(StatisticType.CardsPurchased, 1);
                //    break;
                case GameResource.Milk:
                    UpdateByStatisticType(StatisticType.FridgeCountUpgrade, 1);
                    break;
                //case GameResource.Muuney:
                //    UpdateByStatisticType(StatisticType.CardsPurchased, 1);
                //    break;
                case GameResource.Cereal:
                    UpdateByStatisticType(StatisticType.SiloCountUpgrade, 1);
                    break;
            }

            UpdateByStatisticType(StatisticType.ZonesUpgradePurchased, 1);
        }

        public void UpdateByStatisticType(StatisticType statType, int quantity)
        {
            List<Statistic> filteredStats = statsList.Where(stat => stat.StatType == statType).ToList();
            foreach (var item in filteredStats)
            {
                UpdateStatistic(item, quantity);
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

            RaiseOnStatisticChanged();
        }

        public void ShowStatistics()
        {
            foreach (var item in statsList)
            {
                Debug.Log(item.StatType.GetEnumString() + ": " + item.Uses);
            }
        }

        private int GetMaxSpace(List<PlaceSpaceBehaviour> completeList)
        {
            GetMaxSpace(GameManager.Instance.Tabletop.farms);
            List<PlaceSpaceBehaviour> notActiveSpacesList = completeList.FindAll(f => !f.GetIsActive());

            return notActiveSpacesList.Count();
        }
    }
}
