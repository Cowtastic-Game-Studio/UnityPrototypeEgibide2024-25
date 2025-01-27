using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using System.Collections.Generic;
using System.Linq;
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

        #endregion

        #region Unity methods

        private void Awake()
        {
            this.Missions = new List<Mission>();
        }

        #endregion

        #region Public methods

        public void InitializeMissions()
        {

        }

        public void RenewWeeklyMissions()
        {
            List<Mission> weeklyMissions;
            Mission weeklyMission;

            //Obtiene las misiones semanales
            weeklyMissions = this.Missions.Where((mission) => mission.Type == Mission.MissionTypes.Weekly).ToList();

            //Borra las misiones semanales de la lista
            weeklyMissions.ForEach(mission => this.Missions.Remove(mission));

            weeklyMission = GenerateWeeklyMission();

            this.Missions.Add(weeklyMission);

            NewWeeklyMission.Invoke();
        }

        #endregion

        #region Private methods

        private Mission GenerateWeeklyMission()
        {
            Mission mission = new Mission("Weekly", "", MissionTypes.Weekly, new Goal(), new Reward());

            return mission;
        }

        #endregion

        public UnityEvent NewWeeklyMission;

    }
}