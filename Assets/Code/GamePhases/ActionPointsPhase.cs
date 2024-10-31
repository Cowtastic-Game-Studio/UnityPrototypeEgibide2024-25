using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }

        public ActionPointsPhase()
        {
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            Console.WriteLine("Entering Action Points Phase");

            // Suscribirse al evento global de clic de carta en GameManager
            GameManager.Instance.OnCardClickedGlobal += OnCardClickedHandler;
        }

        public void ExecutePhase()
        {
            Console.WriteLine("Executing Action Points Phase");
        }

        public void EndPhase()
        {
            Console.WriteLine("Ending Action Points Phase");

            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;

            GameManager.Instance.Tabletop.CardManager.WipeBoard();
        }

        private void OnCardClickedHandler(ICard card)
        {
            Console.WriteLine($"Card clicked: {card.Name}");
            // L�gica espec�fica al hacer clic en una carta
        }
    }
}
