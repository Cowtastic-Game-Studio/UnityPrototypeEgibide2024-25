using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
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
            // C�digo para entrar en la fase
            Console.WriteLine("Entering Place Cards Phase");
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            Console.WriteLine("Executing Place Cards Phase");
            // Manejar la entrada del mouse
            GameManager.Instance.Tabletop.CardManager.HandleMouseInput();
        }

        public void EndPhase()
        {
            // C�digo para finalizar la fase
            Console.WriteLine("Ending Place Cards Phase");

            //ALBA: descomentar cundo se arregle el poder hacer click en cartas colocadas
            // GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }
    }
}

