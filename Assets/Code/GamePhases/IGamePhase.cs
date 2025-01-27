namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IGamePhase
    {
        GamePhaseTypes Phase { get; }

        void EnterPhase();
        void ExecutePhase();
        void EndPhase();
    }
}

