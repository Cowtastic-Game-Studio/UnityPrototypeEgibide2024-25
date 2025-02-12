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

        #region Property: IsTutorialEnabled

        public bool IsTutorialEnabled { get; set; }

        #endregion

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
            this.IsTutorialEnabled = true;

            CreateGlobalMissions();
        }

        /// <summary>
        /// Crea la mission de tutorial
        /// </summary>
        private void CreateTutorialMission()
        {
            Goal goal1, goal2, goal3, goal4, goal5, goal6;
            Reward reward;

            goal1 = GoalGenerator.CreateTutorialGoal1();
            goal2 = GoalGenerator.CreateTutorialGoal2();
            goal3 = GoalGenerator.CreateTutorialGoal3();
            goal4 = GoalGenerator.CreateTutorialGoal4();
            goal5 = GoalGenerator.CreateTutorialGoal5();
            goal6 = GoalGenerator.CreateTutorialGoal6();

            reward = RewardGenerator.CreateTutorialReward();

            this.Tutorial = new Mission("Tutorial", "Tutorial", MissionTypes.Tutorial, new List<Goal>() { goal1, goal2, goal3, goal4, goal5, goal6 }, new List<Reward>() { reward });

        }

        /// <summary>
        /// Genera una mision semanal
        /// </summary>
        /// <returns>Devuelve la mision semanal generada</returns>
        private Mission GenerateWeeklyMission()
        {
            List<Goal> goals;
            Reward reward;

            GoalGenerator.SetWeeklyGoals();
            goals = GoalGenerator.GetWeeklyRandomGoals();
            reward = RewardGenerator.CreateWeekReward();

            Mission mission = new Mission("Weekly", "Weekly", MissionTypes.Weekly, goals, new List<Reward>() { reward });

            return mission;
        }

        /// <summary>
        /// Crea las misiones globales/logros
        /// </summary>
        private void CreateGlobalMissions()
        {
            Mission mission1, mission2, mission3, mission4, mission5, mission6, mission7, mission8;
            Goal goal1, goal2, goal3, goal4, goal5, goal6, goal7, goal8;

            this.GlobalMissions = new List<Mission>();

            goal1 = GoalGenerator.CreateGlobalGoal1();
            mission1 = CreateGlobalMission("G1", "Global 1", goal1, 50);
            this.GlobalMissions.Add(mission1);

            goal2 = GoalGenerator.CreateGlobalGoal2();
            mission2 = CreateGlobalMission("G2", "Global 2", goal2, 50);
            this.GlobalMissions.Add(mission2);

            goal3 = GoalGenerator.CreateGlobalGoal3();
            mission3 = CreateGlobalMission("G3", "Global 3", goal3, 50);
            this.GlobalMissions.Add(mission3);

            goal4 = GoalGenerator.CreateGlobalGoal4();
            mission4 = CreateGlobalMission("G4", "Global 4", goal4, 100);
            this.GlobalMissions.Add(mission4);

            goal5 = GoalGenerator.CreateGlobalGoal5();
            mission5 = CreateGlobalMission("G5", "Global 5", goal5, 100);
            this.GlobalMissions.Add(mission5);

            goal6 = GoalGenerator.CreateGlobalGoal6();
            mission6 = CreateGlobalMission("G6", "Global 6", goal6, 100);
            this.GlobalMissions.Add(mission6);

            goal7 = GoalGenerator.CreateGlobalGoal7();
            mission7 = CreateGlobalMission("G7", "Global 7", goal7, 75);
            this.GlobalMissions.Add(mission7);

            goal8 = GoalGenerator.CreateGlobalGoal8();
            mission8 = CreateGlobalMission("G8", "Global 8", goal8, 20);
            this.GlobalMissions.Add(mission8);
        }

        /// <summary>
        /// Crea una mision global
        /// </summary>
        /// <param name="missionName"></param>
        /// <param name="missionDescription"></param>
        /// <param name="goal"></param>
        /// <param name="rewardMuuney"></param>
        /// <returns></returns>
        private Mission CreateGlobalMission(string missionName, string missionDescription, Goal goal, int rewardMuuney)
        {
            Reward reward;

            reward = RewardGenerator.CreateGoalReward(rewardMuuney);
            return new Mission(missionName, missionDescription, MissionTypes.Global, goal, reward);
        }

        #endregion

    }
}