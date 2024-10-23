using System;
public class PlaceCardsPhase : IGamePhaseWUndo
{
    public ActionManager<ICommand> ActionManager { get; private set; }

    public PlaceCardsPhase()
    {
        // Inicializar el ActionManager
        ActionManager = new ActionManager<ICommand>();
    }

    public void EnterPhase()
    {
        // Código para entrar en la fase
        Console.WriteLine("Entering Place Cards Phase");
    }

    public void ExecutePhase()
    {
        // Código que define la lógica de la fase
        Console.WriteLine("Executing Place Cards Phase");

    }

    public void EndPhase()
    {
        // Código para finalizar la fase
        Console.WriteLine("Ending Place Cards Phase");
    }
}

