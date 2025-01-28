using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using static CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions.Mission;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MissionsManager : MonoBehaviour
    {

        #region Properties

        #region Property: Missions

        public List<Mission> Missions { get; set; }

        #endregion

        #region Property: TutorialMission

        public Mission Tutorial { get; set; }

        #endregion

        #endregion

        public static MissionsManager Instance { get; private set; }

        #region Unity methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            this.Missions = new List<Mission>();

            InitializeMissions();
        }

        #endregion

        #region Public methods

        public void InitializeMissions()
        {
            this.Tutorial = CreateTutorialMission();
            this.Missions.Add(this.Tutorial);

            RenewWeeklyMission();
            CreateGlobalMissions();

        }

        public void RenewWeeklyMission()
        {
            Mission weeklyMission;

            //Obtiene las misiones semanales
            weeklyMission = this.Missions.FirstOrDefault((mission) => mission.Type == Mission.MissionTypes.Weekly);

            //Borra la mision semanal de la lista
            if(weeklyMission != null)
                this.Missions.Remove(weeklyMission);

            //Genera la mision semanal
            weeklyMission = GenerateWeeklyMission();

            //Añadir la mision semanal
            this.Missions.Add(weeklyMission);

            NewWeeklyMission.Invoke();
        }

        

        #endregion

        #region Private methods

        private Mission CreateTutorialMission()
        {
            var goal1 = new Goal("T-M1", "Robar 1 carta", null);
            var goal2 = new Goal("T-M2", "Robar 1 carta", null);
            var mission = new Mission("Tutorial", "", Mission.MissionTypes.Tutorial, new List<Goal>() { goal1, goal2 }, new List<Reward>());

            return mission;
        }

        private Mission GenerateWeeklyMission()
        {
            var goal1 = new Goal("W-M1", "Weekly", null);

            Mission mission = new Mission("Weekly", "", MissionTypes.Weekly, new List<Goal>() { goal1 }, new List<Reward>());

            return mission;
        }

        private List<Mission> CreateGlobalMissions()
        {
            return null;
        }

        #endregion

        public UnityEvent NewWeeklyMission;

    }
}