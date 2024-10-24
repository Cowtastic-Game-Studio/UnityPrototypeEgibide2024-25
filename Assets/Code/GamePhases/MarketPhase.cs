using System;


namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MarketPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }

        public MarketPhase()
        {
            // Inicializar el ActionManager
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            // C�digo para entrar en la fase
            Console.WriteLine("Entering Market Phase");
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            Console.WriteLine("Executing Market Phase");

        }

        public void EndPhase()
        {
            // C�digo para finalizar la fase
            Console.WriteLine("Ending Market Phase");

            //pasar al siguiente dia
            GameManager.Instance.GameCalendar.NextDay();
        }
    }
}

