using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MissionHUDmanager : MonoBehaviour
    {
        #region Propiedades

        [SerializeField] private GameObject TutorialMission;

        [SerializeField] private GameObject WeeklyMission;

        [SerializeField] private GameObject GlobalMission;

        #endregion

        #region Unity methods

        private void Start()
        {
            MissionSheetManager tutorialMisionManager;
            MissionSheetManager weeklyMissionManager;
            GlobalMissionSheetManager globalMissionManager;

            MissionsManager.Instance.NewWeeklyMission.AddListener(OnNewWeeklyMission);

            tutorialMisionManager = TutorialMission.GetComponent<MissionSheetManager>();
            tutorialMisionManager.Mission = MissionsManager.Instance.Tutorial;

            weeklyMissionManager = WeeklyMission.GetComponent<MissionSheetManager>();
            weeklyMissionManager.Mission = MissionsManager.Instance.WeeklyMission;

            globalMissionManager = GlobalMission.GetComponent<GlobalMissionSheetManager>();
            globalMissionManager.Missions = MissionsManager.Instance.GlobalMissions;

        }


        #endregion

        #region Eventhandlers
        private void OnNewWeeklyMission()
        {
            MissionSheetManager weeklyMissionManager;
            weeklyMissionManager = WeeklyMission.GetComponent<MissionSheetManager>();
            weeklyMissionManager.Mission = MissionsManager.Instance.WeeklyMission;

            if (GameManager.Instance.GameCalendar.CurrentWeek == 1)
            {
                ChangeTutorialForWeekly();
            }

        }


        #endregion

        #region Private methods

        private void ChangeTutorialForWeekly()
        {
            this.TutorialMission.SetActive(false);
            this.WeeklyMission.SetActive(true);

            MissionsManager.Instance.IsTutorialEnabled = false;
        }

        #endregion


    }
}
