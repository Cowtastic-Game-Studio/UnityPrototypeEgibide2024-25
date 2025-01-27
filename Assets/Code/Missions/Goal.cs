using System;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    public class Goal
    {
        #region Properties

        #region Property: Description
        public string Description { get; set; }

        #endregion

        #region Property: RewardCondition
        private Action RewardCondition { get; set; }

        #endregion

        #region Property: IsDone

        public bool IsDone { get; set; }

        #endregion

        #endregion

    }
}
