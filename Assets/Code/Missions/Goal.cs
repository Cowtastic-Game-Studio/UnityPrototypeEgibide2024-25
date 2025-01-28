using System;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    public class Goal
    {
        #region Properties

        #region Property: Name
        public string Name { get; set; }

        #endregion

        #region Property: Description
        public string Description { get; set; }

        #endregion

        #region Property: RewardCondition
        private Action CompleteCondition { get; set; }

        #endregion

        #region Property: IsComplete

        public bool IsComplete { get; set; }

        #endregion

        #endregion

        #region Constructors

        public Goal(string name, string description, Action completeCondition)
        {
            this.Name = name;
            this.Description = description;
            this.CompleteCondition = completeCondition;
            this.IsComplete = false;
        }

        #endregion


    }
}
