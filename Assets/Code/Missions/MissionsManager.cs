using System.Collections.Generic;
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
            Goal goal1, goal2;

            goal1 = GoalGenerator.CreateTutorialGoal1();
            goal2 = GoalGenerator.CreateTutorialGoal2();



            this.Tutorial = new Mission("Tutorial", "Tutorial", Mission.MissionTypes.Tutorial, new List<Goal>() { goal1, goal2 }, new List<Reward>());

        }

        /// <summary>
        /// Genera una mision semanal
        /// </summary>
        /// <returns>Devuelve la mision semanal generada</returns>
        private Mission GenerateWeeklyMission()
        {
            List<Goal> goals;

            goals = GoalGenerator.GetWeeklyRandomGoals();

            Mission mission = new Mission("Weekly", "Weekly", MissionTypes.Weekly, goals, new List<Reward>());

            return mission;
        }

        /// <summary>
        /// Crea las misiones globales/logros
        /// </summary>
        private void CreateGlobalMissions()
        {
            this.GlobalMissions = new List<Mission>();            

        }

        #endregion

    }
}