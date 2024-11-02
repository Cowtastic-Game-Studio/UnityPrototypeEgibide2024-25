namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IGamePhaseWUndo : IGamePhase
    {
        ActionManager<ICommand> ActionManager { get; }
    }
}
