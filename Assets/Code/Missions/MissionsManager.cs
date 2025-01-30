using System;
using System.Collections.Generic;
using System.Linq;
using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using UnityEngine;
using UnityEngine.Events;
using static CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions.Mission;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    /// <summary>
    /// Gestor de misiones
    /// </summary>
    public class MissionsManager : MonoBehaviour
    {
        #region Singleton

        public static MissionsManager Instance { get; private set; }

        #endregion

        #region Properties

        #region Property: TutorialMission

        public Mission Tutorial { get; set; }

        #endregion

        #region Property: WeeklyMission

        public Mission WeeklyMission { get; set; }

        #endregion

        #region Property: GlobalMission

        public List<Mission> GlobalMissions { get; set; }

        #endregion

        #endregion

        #region Events

        public UnityEvent NewWeeklyMission = new UnityEvent();

        #endregion

        #region Unity methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            InitializeMissions();
        }

        #endregion

        #region Public methods

        public void RenewWeeklyMission()
        {
            //Genera la mision semanal
            this.WeeklyMission = GenerateWeeklyMission();

            NewWeeklyMission.Invoke();
        }

        #endregion

        #region Private methods

        private void InitializeMissions()
        {
            CreateTutorialMission();

            RenewWeeklyMission();

            CreateGlobalMissions();
        }

        /// <summary>
        /// Crea la mission de tutorial
        /// </summary>
        private void CreateTutorialMission()
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
            var goal1 = new Goal("T-M1", "Coloca 1 carta en la mesa", goal1Initialization); 

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
            var goal2 = new Goal("T-M2", "Consigue 1 de trigo", goal2Initialization);


            //var goal3 = new Goal("T-M3", "T-M3", null);
            //var goal4 = new Goal("T-M4", "T-M4", null);
            //var goal5 = new Goal("T-M5", "T-M5", null);

            this.Tutorial = new Mission("Tutorial", "Tutorial", Mission.MissionTypes.Tutorial, new List<Goal>() { goal1, goal2 }, new List<Reward>());

        }

        /// <summary>
        /// Genera una mision semanal
        /// </summary>
        /// <returns></returns>
        private Mission GenerateWeeklyMission()
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
            var goal1 = new Goal("W-M1", "Ordeña vacas 4 veces", goal1Initialization);

            //var goal2 = new Goal("W-M2", "W2", null);
            //var goal3 = new Goal("W-M3", "W3", null);
            //var goal4 = new Goal("W-M4", "W4", null);

            Mission mission = new Mission("Weekly", "Weekly", MissionTypes.Weekly, new List<Goal>() { goal1 }, new List<Reward>());

            return mission;
        }

        /// <summary>
        /// Crea las misiones globales/logros
        /// </summary>
        /// <returns></returns>
        private List<Mission> CreateGlobalMissions()
        {
            return null;
        }

        #endregion

    }
}