using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MissionHUDmanager : MonoBehaviour
    {
        #region Propiedades

        [SerializeField] private GameObject TutorialMission;

        [SerializeField] private GameObject WeeklyMission;


        #endregion



        #region Unity methods

        private void Start()
        {
            MissionSheetManager tutorialMisionManager;
            MissionSheetManager weeklyMissionManager;


            MissionsManager.Instance.NewWeeklyMission.AddListener(OnNewWeeklyMission);


            tutorialMisionManager = TutorialMission.GetComponent<MissionSheetManager>();
            tutorialMisionManager.Mission = MissionsManager.Instance.Tutorial;


            weeklyMissionManager = WeeklyMission.GetComponent<MissionSheetManager>();
            weeklyMissionManager.Mission = MissionsManager.Instance.WeeklyMission;
        }

        #endregion

        #region Eventhandlers
        private void OnNewWeeklyMission()
        {
            MissionSheetManager weeklyMissionManager;
            weeklyMissionManager = WeeklyMission.GetComponent<MissionSheetManager>();
            weeklyMissionManager.Mission = MissionsManager.Instance.WeeklyMission;
        }

        #endregion


    }
}
