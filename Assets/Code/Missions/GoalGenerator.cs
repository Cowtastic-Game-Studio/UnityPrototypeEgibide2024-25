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
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CerealTotalAdquired);

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
            return new Goal("W-M1", "Cosecha trigo 5 veces", goal1Initialization);
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
            return new Goal("W-M1", "Vende 3 leches", goal1Initialization);
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
            return new Goal("W-M1", "Vende 3 leches", goal1Initialization);
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
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.MuuneyTotalAdquired);

                    if (stat.Uses >= 20)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("W-M1", "Vende 3 leches", goal1Initialization);
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
            return new Goal("W-M1", "Vende 3 leches", goal1Initialization);
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
            return new Goal("W-M1", "Vende 3 leches", goal1Initialization);
        }

        #endregion

        #region Globales

        /// <summary>
        /// Crea el objetivo 2 de la mision de tutorial.
        /// "Consigue 1 de trigo"
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
                    Statistic stat = StatisticsManager.Instance.GetStat(StatisticType.CowsMilked);

                    if (stat.Uses >= 4)
                        goal.IsCompleted = true;

                    //si esta clompleto
                    if (goal.IsCompleted)
                        StatisticsManager.Instance.OnStatisticChanged.RemoveListener(OnStatisticChanged);
                };

                //StatisticsManager.Instance.OnStatisticChanged.AddListener(OnStatisticChanged);
            };
            return new Goal("G-M1", "Ordeña vacas 4 veces", goal1Initialization);

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
