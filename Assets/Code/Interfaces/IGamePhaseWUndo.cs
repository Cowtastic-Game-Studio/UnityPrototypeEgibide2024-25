using System.Windows.Input;

public interface IGamePhaseWUndo : IGamePhase
{
    ActionManager<ICommand> ActionManager { get; }  // Agrega la propiedad ActionManager
}
