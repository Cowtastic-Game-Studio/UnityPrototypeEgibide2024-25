using System;
using UnityEngine;

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
            // C�digo que define la l�gica de la fase
            //check clickn card(de algun sitio)
            ICard card = null;
            GameManager.Instance.Tabletop.OnCardUseActionPoints(card);
            //checkap
            //comprobnar npa con el card
            //checkresources(nresources card)->nresources
            //


        }

        public void EndPhase()
        {
            Console.WriteLine("Ending Action Points Phase");

            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;
        }

        private void OnCardClickedHandler(GameObject cardGameObject)
        {
            Console.WriteLine($"Card clicked: {cardGameObject.name}");
            // L�gica espec�fica al hacer clic en una carta
        }
    }
}
