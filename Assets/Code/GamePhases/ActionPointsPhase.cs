using Assets.Code.GamePhases;
using System;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }
        public StorageManager storageManager;
        public Tabletop Tabletop;

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
            ICard card = null;
            // C�digo que define la l�gica de la fase
            //check clickn card(de algun sitio)

            //GameManager.Instance.Tabletop.OnCardUseActionPoints();
            //checkap
            //comprobnar npa con el card
            //checkresources(nresources card)->nresources
            //

            //if (cardGameObject != null)
            //{
            //    // Verificar puntos de acción en requiredList
            //    bool hasActionPoints = storageManager.CheckActionPoints(card.);

            //    if (hasActionPoints)
            //    {
            //        // Verificar recursos necesarios
            //        bool hasResources = GameManager.Instance.Tabletop.storageManager.CheckResources(cardgameobject.requiredResources[], );

            //        if (hasResources)
            //        {
            //            // Producir recursos o realizar la acción necesaria
            //            storageManager.ProduceResources(card);
            //            Console.WriteLine("Action points and resources have been checked and resources produced.");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Not enough resources for the card action.");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Not enough action points for the card action.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No card provided to use action points.");
            //}
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
            if (cardGameObject != null)
            {

            }
        }


    }





}
