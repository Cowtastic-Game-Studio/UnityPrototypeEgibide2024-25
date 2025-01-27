using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using System.Collections.Generic;
using UnityEngine;

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

        #endregion

        #region Private methods


        #endregion

    }
}
