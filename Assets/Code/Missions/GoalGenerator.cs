using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{

    /// <summary>
    /// Genera los objetivos
    /// </summary>
    public class GoalGenerator
    {

        #region Tutorial

        /// <summary>
        /// Crea el objetivo 1 de la mision de tutorial.
        /// "Coloca 1 carta en la mesa"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal1()
        {
            return GenerateStatGoal("T-M1", "Place 1 card on the table", StatisticType.CardsTotalUsed, 1);
        }

        /// <summary>
        /// Crea el objetivo 2 de la mision de tutorial.
        /// "Consigue 1 de trigo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal2()
        {
            return GenerateStatGoal("T-M2", "Get 1 wheat", StatisticType.CerealTotalAcquired, 1);

        }

        /// <summary>
        /// Crea el objetivo 3 de la mision de tutorial.
        /// "Consigue 1 de leche"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal3()
        {
            return GenerateStatGoal("T-M3", "Get 1 milk", StatisticType.MilkTotalAcquired, 1);

        }

        /// <summary>
        /// Crea el objetivo 4 de la mision de tutorial.
        /// "Consigue un cliente"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal4()
        {
            return GenerateStatGoal("T-M4", "Get a client", StatisticType.CustomersServed, 1);

        }

        /// <summary>
        /// Crea el objetivo 5 de la mision de tutorial.
        /// "Compra una carta"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal5()
        {
            return GenerateStatGoal("T-M5", "Buy a card", StatisticType.CardsPurchased, 1);

        }

        /// <summary>
        /// Crea el objetivo 5 de la mision de tutorial.
        /// "Compra una carta"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal6()
        {
            return GenerateStatGoal("T-M6", "Buy a zone upgrade", StatisticType.ZonesUpgradePurchased, 1);
        }

        #endregion

        #region Semanales

        /// <summary>
        /// Lista que apunta a todas las funciones que crean objetivos de la semana
        /// Añadir aqui cada vez que se genere
        /// </summary>
        public static List<Func<Goal>> WeeklyGoalFunctions = new List<Func<Goal>>() {
            CreateWeeklyGoal1,
            CreateWeeklyGoal2,
            CreateWeeklyGoal3,
            CreateWeeklyGoal4,
            CreateWeeklyGoal5,
            CreateWeeklyGoal6,
            CreateWeeklyGoal7
        };

        /// <summary>
        /// Obtiene los objetivos semanales de manera aleatoria
        /// </summary>
        /// <returns></returns>
        public static List<Goal> GetWeeklyRandomGoals()
        {
            List<int> randomIndexs;
            List<Goal> goals = new List<Goal>();
            Goal goal;

            //Genera los indices de los objetivos
            randomIndexs = GenerateNotRepeatedRandomNumbers(0, WeeklyGoalFunctions.Count, 4);

            foreach (int index in randomIndexs)
            {
                //genera el objetivo y lo añade a la lista
                goal = WeeklyGoalFunctions[index].Invoke();
                goals.Add(goal);
            }

            return goals;
        }

        /// <summary>
        /// Crea el objetivo 1 de la mision semanal.
        /// "Ordeña vacas 4 veces"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal1()
        {
            return GenerateStatGoal("W-M1", "Milk cows 4 times", StatisticType.CowsMilked, 4);
        }

        /// <summary>
        /// Crea el objetivo 2 de la mision semanal.
        /// "Cosecha trigo 5 veces"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal2()
        {
            return GenerateStatGoal("W-M2", "Harvest wheat 5 times", StatisticType.SeedsHarvested, 5);
        }

        /// <summary>
        /// Crea el objetivo 3 de la mision semanal.
        /// "Vende 3 leches"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal3()
        {
            return GenerateStatGoal("W-M3", "Sell 3 milks", StatisticType.MilkTotalUsed, 3);
        }

        /// <summary>
        /// Crea el objetivo 4 de la mision semanal.
        /// "Vende 2 compra dos cartas"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal4()
        {
            return GenerateStatGoal("W-M4", "Buy 2 cards", StatisticType.CardsPurchased, 2);
        }

        /// <summary>
        /// Crea el objetivo 5 de la mision semanal.
        /// "adquiere 20 de Muuuney"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal5()
        {
            return GenerateStatGoal("W-M5", "Get 20 Muuney", StatisticType.MuuneyTotalAcquired, 20);
        }

        /// <summary>
        /// Crea el objetivo 6 de la mision semanal.
        /// "Usa una mejora temporal"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal6()
        {
            return GenerateStatGoal("W-M6", "Use an upgrade card", StatisticType.TemporaryUsedCards, 1);
        }

        /// <summary>
        /// Crea el objetivo 7 de la mision semanal.
        /// "Atiende a 5 clientes"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal7()
        {
            return GenerateStatGoal("W-M7", "Serve 5 clients", StatisticType.CustomersServed, 5);
        }

        #endregion

        #region Globales

        /// <summary>
        /// Crea el objetivo global 1
        /// "Consigue todas las mejoras del establo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal1()
        {
            return GenerateStatGoal("G-M1", "Fully upgraded stable", StatisticType.StableCountUpgrade, GetMaxSpace(GameManager.Instance.Tabletop.farms));
        }

        /// <summary>
        /// Crea el objetivo global 2
        /// "Consigue todas las mejoras del huerto"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal2()
        {
            return GenerateStatGoal("G-M2", "Fully upgraded garden", StatisticType.GardenCountUpgrade, GetMaxSpace(GameManager.Instance.Tabletop.stables));
        }

        /// <summary>
        /// Crea el objetivo global 2
        /// "Consigue todas las mejoras del establo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal3()
        {
            return GenerateStatGoal("G-M3", "Fully upgraded tavern", StatisticType.TavernCountUpgrade, GetMaxSpace(GameManager.Instance.Tabletop.taverns));
        }

        /// <summary>
        /// Crea el objetivo global 4
        /// "Llena el establo con vacas cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal4()
        {
            return GenerateStatGoal("G-M4", "Fill the barn with cows when it is fully upgraded.", StatisticType.StableFull, 1);
        }

        /// <summary>
        /// Crea el objetivo global 5
        /// "Llena el huerto con cultivos cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal5()
        {
            return GenerateStatGoal("G-M5", "GardenFilledWithCropsMaxUpgrade", StatisticType.FarmFull, 1);
        }

        /// <summary>
        /// Crea el objetivo global 6
        /// "Llena la taverna con clientes cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal6()
        {
            return GenerateStatGoal("G-M6", "Fill the tavern with customers when it is fully upgraded", StatisticType.ShopFull, 1);
        }

        /// <summary>
        /// Crea el objetivo global 7
        /// "Ten los almacenes silo y frigorifico al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal7()
        {
            Func<bool> condition = () =>
            {
                //Comprobar condicion del objetivo
                Statistic statFridge = StatisticsManager.Instance.GetStat(StatisticType.FridgeCountUpgrade);
                Statistic statSilo = StatisticsManager.Instance.GetStat(StatisticType.SiloCountUpgrade);

                int maxLevelFridge = GameManager.Instance.Tabletop.StorageManager.GetStorageMaxLevel(GameResource.Milk);
                int maxLevelSilo = GameManager.Instance.Tabletop.StorageManager.GetStorageMaxLevel(GameResource.Cereal);

                return (statFridge.Uses >= maxLevelFridge && statSilo.Uses >= maxLevelSilo);
            };

            return GenerateStatGoal("G-M7", "Have the silo and refrigerator warehouses fully upgraded", condition);
        }

        /// <summary>
        /// Crea el objetivo global 8
        /// "Supera 3 eventos"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal8()
        {
            return GenerateStatGoal("G-M8", "Beat 3 events", StatisticType.EventsCompleted, 3);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Genera X numero randoms no repetidos
        /// </summary>
        /// <param name="min">Numero posible mas pequeño</param>
        /// <param name="max">Numero posible mas grande</param>
        /// <param name="numberCount">Cantidad de numeros a generar</param>
        /// <returns>Devuelve la lista de numeros aleatorios</returns>
        private static List<int> GenerateNotRepeatedRandomNumbers(int min, int max, int numberCount)
        {
            List<int> randomNumbers = new List<int>();
            int randomNumber;
            System.Random rand = new System.Random();

            if (numberCount > max)
                throw new Exception("El numero de numeros necesarios es mayor que el numero de numeros disponibles");

            // Generar x números aleatorios no repetidos
            while (randomNumbers.Count < numberCount)
            {
                randomNumber = rand.Next(min, max);
                if (!randomNumbers.Contains(randomNumber))  // Verificar que no se repita
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            return randomNumbers;
        }

        private static Goal GenerateStatGoal(string goalName, string goalDescription, StatisticType statisticType, int usesToComplete)
        {
            Func<bool> condition = () =>
            {
                //Comprobar condicion del objetivo
                Statistic stat = StatisticsManager.Instance.GetStat(statisticType);

                return (stat.Uses >= usesToComplete);
            };

            return GenerateStatGoal(goalName, goalDescription, condition);
        }

        private static Goal GenerateStatGoal(string goalName, string goalDescription, Func<bool> condition)
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    bool result;

                    result = condition.Invoke();

                    if (result)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal(goalName, goalDescription, goal1Initialization);
        }

        private static int GetMaxSpace(List<PlaceSpaceBehaviour> completeList)
        {
            List<PlaceSpaceBehaviour> notActiveSpacesList = completeList.FindAll(f => !f.GetIsActive());
            return notActiveSpacesList.Count;
        }


        #endregion
    }
}
