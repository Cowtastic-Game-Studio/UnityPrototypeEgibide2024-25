using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StatisticsManager : MonoBehaviour
    {
        public enum StatisticAction
        {
            Add,
            Remove,
            None
        }

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

        public void UpdateStatistic(Statistic stat, StatisticAction action)
        {
            switch (action)
            {
                case StatisticAction.Add:
                    stat.Uses++;
                    break;
                case StatisticAction.Remove:
                    stat.Uses--;
                    break;
                case StatisticAction.None:
                    break;
            }

            foreach (var item in statsList)
            {
                Debug.Log(item.Title + ": " + item.Uses);
            }
        }

        private void Initialized()
        {
            statsList.Add(new Statistic("Milked cows", CardType.Cow, GameResource.None, 0));
            statsList.Add(new Statistic("Used seeds", CardType.Seed, GameResource.None, 0));
            statsList.Add(new Statistic("Served customers", CardType.Customer, GameResource.None, 0));
            statsList.Add(new Statistic("Temporary used cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Purchased cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Purchased zones", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Purchased zones with cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Discarded cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Used cards", CardType.None, GameResource.None, 0));
            statsList.Add(new Statistic("Total adquired milk", CardType.None, GameResource.Milk, 0));
            statsList.Add(new Statistic("Total adquired cereal", CardType.None, GameResource.Cereal, 0));
            statsList.Add(new Statistic("Total adquired muuney", CardType.None, GameResource.Muuney, 0));
            statsList.Add(new Statistic("Used action points", CardType.None, GameResource.ActionPoints, 0));

        }

        public void CardClicked(ICard card)
        {
            if (!card.IsActive && GameManager.Instance.GamePhaseManager.CurrentPhaseType == GamePhaseTypes.Action)
            {
                CardBehaviour cardBehaviour = card as CardBehaviour;

                Debug.Log(cardBehaviour.Type);

                foreach (var item in statsList)
                {
                    if (item.Type == cardBehaviour.Type)
                    {
                        UpdateStatistic(item, StatisticAction.Add);
                    }

                }
            }

        }

        public UnityEvent OnStatisticChanged = new UnityEvent();


        public void RaiseOnStatisticChanged()
        {
            OnStatisticChanged?.Invoke();
        }
    }
}
