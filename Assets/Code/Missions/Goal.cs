using System;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    public class Goal 
    {
        public delegate void InitializeCheckGoal();

        #region Properties

        #region Property: Name
        public string Name { get; set; }

        #endregion

        #region Property: Description
        public string Description { get; set; }

        #endregion

        //#region Property: RewardCondition
        //private Action CompleteCondition { get; set; }

        //#endregion

        #region Property: IsCompleted

        private bool _IsCompleted;

        public bool IsCompleted
        {
            get { return _IsCompleted; }
            set { _IsCompleted = value;

                if (_IsCompleted == true)
                    OnCompleted.Invoke(this);
            }
        }


        #endregion

        #endregion

        #region Events

        public UnityEvent<Goal> OnCompleted = new UnityEvent<Goal>();

        #endregion


        #region Constructors

        public Goal(string name, string description, UnityAction<Goal> initializeCondition)
        {
            this.Name = name;
            this.Description = description;
            this.IsCompleted = false;

            if(initializeCondition != null)
                initializeCondition.Invoke(this);
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }

    }
}
