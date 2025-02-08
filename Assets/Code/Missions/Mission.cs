using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions
{
    public class Mission
    {
        public enum MissionTypes
        {
            Tutorial,
            Weekly,
            Global
        }


        #region Properties

        #region Property: Name

        public string Name { get; private set; }

        #endregion

        #region Property: Description

        public string Description { get; private set; }

        #endregion

        #region Property: Type

        public MissionTypes Type { get; private set; }

        #endregion

        #region Property: Goals

        public IReadOnlyList<Goal> Goals { get; private set; }

        #endregion

        #region Property: Rewards
        public IReadOnlyList<Reward> Rewards { get; set; }

        #endregion

        #endregion

        #region Constructors

        public Mission(string name, string description, MissionTypes type, IEnumerable<Goal> goals, IEnumerable<Reward> rewards)
        {
            this.Name = name;
            this.Description = description;
            this.Type = type;
            this.Goals = new List<Goal>(goals);
            this.Rewards = new List<Reward>(rewards);

            if (!MissionsManager.Instance.IsTutorialEnabled && type == MissionTypes.Tutorial)
            {
                return;
            }

            //Se suscribe el evento OnCompleted de los goals
            foreach (Goal goal in Goals)
            {
                goal.OnCompleted.AddListener(OnCompleteGoal);
            }
        }

        public Mission(string name, string description, MissionTypes type, Goal goal, Reward reward) :
            this(name, description, type, new List<Goal>() { goal }, new List<Reward>() { reward })
        {
        }

        #endregion

        #region Events

        public UnityEvent<Mission> Updated = new UnityEvent<Mission>();

        #endregion

        #region Public methods

        public void DeactivateMision()
        {
            foreach (Goal goal in Goals)
            {
                goal.OnCompleted.RemoveListener(OnCompleteGoal);
            }
        }



        #endregion

        #region Private methods

        private void OnCompleteGoal(Goal goal)
        {
            Debug.LogWarning("Goal completed: " + goal.Description);
            goal.OnCompleted.RemoveListener(OnCompleteGoal);
            this.Updated.Invoke(this);

            List<Goal> filteredGoals = Goals.Where(_goal => _goal.IsCompleted == false).ToList();

            //Si se han completado todos los goals, se reciben las recompensas
            if (filteredGoals.Count == 0)
            {
                //Rewards.ToList().ForEach(reward => reward.Receive());
                foreach (Reward reward in Rewards.ToList())
                {
                    reward.Receive();
                }
            }
        }

        #endregion

    }
}
