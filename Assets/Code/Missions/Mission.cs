using System.Collections.Generic;

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
        }

        public Mission(string name, string description, MissionTypes type, Goal goal, Reward reward) :
            this(name, description, type, new List<Goal>() { goal }, new List<Reward>() { reward })
        {
        }


        #endregion


    }
}
