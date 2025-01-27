using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface ICard
    {
        CardState State { get; set; }

        string Name { get; }
        string Description { get; }
        int ActionPointsCost { get; }
        int LifeCycleDays { get; }
        int MarketCost { get; }
        List<ResourceAmount> RequiredResources { get; }
        List<ResourceAmount> ProducedResources { get; }

        bool IsActive { get; }
        void Activate();
        void Deactivate();

        void Print();
        void InvokeCardClicked();
    }
}
