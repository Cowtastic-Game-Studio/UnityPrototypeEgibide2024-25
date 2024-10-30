using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }

        public ActionPointsPhase()
        {
            // Inicializar el ActionManager
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            // C�digo para entrar en la fase
            Console.WriteLine("Entering Action Points Phase");
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            Console.WriteLine("Executing Action Points Phase");
            ICard card = null;
            GameManager.Instance.Tabletop.OnCardUseActionPoints(card);

        }

        public void EndPhase()
        {
            // C�digo para finalizar la fase
            Console.WriteLine("Ending Action Points Phase");
        }
    }
}
