using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{

    /// <summary>
    /// Genera los objetivos
    /// </summary>
    internal static class GoalGenerator
    {

        #region Tutorial

        /// <summary>
        /// Crea el objetivo 1 de la mision de tutorial.
        /// "Coloca 1 carta en la mesa"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal1()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CardsTotalUsed);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };

            return new Goal("T-M1", "Coloca 1 carta en la mesa", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 2 de la mision de tutorial.
        /// "Consigue 1 de trigo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal2()
        {
            UnityAction<Goal> goal2Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                //Crea 
                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CerealTotalAcquired);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("T-M2", "Consigue 1 de trigo", goal2Initialization);

        }

        /// <summary>
        /// Crea el objetivo 3 de la mision de tutorial.
        /// "Consigue 1 de leche"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal3()
        {
            UnityAction<Goal> goal2Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                //Crea 
                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.MilkTotalAcquired);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("T-M3", "Consigue 1 de leche", goal2Initialization);

        }

        /// <summary>
        /// Crea el objetivo 4 de la mision de tutorial.
        /// "Consigue un cliente"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal4()
        {
            UnityAction<Goal> goal2Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                //Crea 
                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CustomersTotalUsed);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("T-M4", "Consigue un cliente", goal2Initialization);

        }

        /// <summary>
        /// Crea el objetivo 5 de la mision de tutorial.
        /// "Compra una carta"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal5()
        {
            UnityAction<Goal> goal2Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                //Crea 
                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CardsPurchased);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("T-M5", "Compra una carta", goal2Initialization);

        }

        /// <summary>
        /// Crea el objetivo 5 de la mision de tutorial.
        /// "Compra una carta"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateTutorialGoal6()
        {
            UnityAction<Goal> goal2Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                //Crea 
                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.ZonesUpgradePurchased);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("T-M6", "Compra una mejora de zona", goal2Initialization);

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
            randomIndexs = GenerateNotRepeatedRandomNumbers(0, WeeklyGoalFunctions.Count, 1);

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
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CowsMilked);

                    if (stat.Uses >= 4)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M1", "Ordeña vacas 4 veces", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 2 de la mision semanal.
        /// "Cosecha trigo 5 veces"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal2()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.SeedsHarvested);

                    if (stat.Uses >= 5)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M2", "Cosecha trigo 5 veces", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 3 de la mision semanal.
        /// "Vende 3 leches"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal3()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.MilkTotalUsed);

                    if (stat.Uses >= 3)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M3", "Vende 3 leches", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 4 de la mision semanal.
        /// "Vende 2 compra dos cartas"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal4()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CardsPurchased);

                    if (stat.Uses >= 2)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M4", "Compra 2 cartas", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 5 de la mision semanal.
        /// "adquiere 20 de Muuuney"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal5()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.MuuneyTotalAcquired);

                    if (stat.Uses >= 20)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M5", "Adquiere 20 de Muuney", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 6 de la mision semanal.
        /// "Usa una mejora temporal"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal6()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    // TODO No estoy seguro si esta es la estadistica para las mejoras temporales o para cartas temporales como tal
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.TemporaryUsedCards);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M6", "Usa una mejora temporal", goal1Initialization);
        }

        /// <summary>
        /// Crea el objetivo 7 de la mision semanal.
        /// "Atiende a 5 clientes"
        /// </summary>
        /// <returns></returns>
        private static Goal CreateWeeklyGoal7()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CustomersServed);

                    if (stat.Uses >= 5)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M7", "Atiende a 5 clientes", goal1Initialization);
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
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.StableCountUpgrade);

                    if (stat.Uses >= 6)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M1", "Establo mejorado al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 2
        /// "Consigue todas las mejoras del huerto"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal2()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.GardenCountUpgrade);

                    if (stat.Uses >= 8)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M2", "Huerto mejorado al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 2
        /// "Consigue todas las mejoras del establo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal3()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.TavernCountUpgrade);

                    if (stat.Uses >= 4)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M3", "Taverna mejorada al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 4
        /// "Llena el establo con vacas cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal4()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.StableFilledWithCowsMaxUpgrade);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M4", "Llena el establo con vacas cuando este al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 5
        /// "Llena el huerto con cultivos cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal5()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.GardenFilledWithCropsMaxUpgrade);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M5", "GardenFilledWithCropsMaxUpgrade", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 6
        /// "Llena la taverna con clientes cuando este al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal6()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.TavernFilledWithCustomersMaxUpgrade);

                    if (stat.Uses >= 1)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M6", "Llena la taverna con clientes cuando este al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 7
        /// "Ten los almacenes silo y frigorifico al maximo"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal7()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic statFridge = StatisticsManager.Instance.GetStat(StatisticType.FridgeCountUpgrade);
                    Statistic statSilo = StatisticsManager.Instance.GetStat(StatisticType.SiloCountUpgrade);

                    if (statFridge.Uses >= 8 & statSilo.Uses >= 8)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M7", "Ten los almacenes silo y frigorifico al maximo", goal1Initialization);

        }

        /// <summary>
        /// Crea el objetivo global 8
        /// "Supera 3 eventos"
        /// </summary>
        /// <returns></returns>
        public static Goal CreateGlobalGoal8()
        {
            UnityAction<Goal> goal1Initialization = (Goal goal) =>
            {
                UnityAction OnStatisticChanged = null;

                OnStatisticChanged = () =>
                {
                    //Comprobar condicion del objetivo
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.EventsCompleted);

                    if (stat.Uses >= 3)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M8", "Supera 3 eventos", goal1Initialization);

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
            Random rand = new Random();

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

        #endregion
    }
}
