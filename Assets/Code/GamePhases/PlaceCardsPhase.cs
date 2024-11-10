using System;
using UnityEngine;

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
            // Suscribirse al evento global de clic de carta en GameManager
            GameManager.Instance.OnCardClickedGlobal += OnCardClickedHandler;
            GameManager.Instance.OnPlaceSpaceClickedGlobal += OnPlaceSpaceClickedHandler;
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            Console.WriteLine("Executing Place Cards Phase");
        }

        public void EndPhase()
        {
            // C�digo para finalizar la fase
            Console.WriteLine("Ending Place Cards Phase");

            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;
            GameManager.Instance.OnPlaceSpaceClickedGlobal -= OnPlaceSpaceClickedHandler;

            //ALBA: descomentar cundo se arregle el poder hacer click en cartas colocadas
            // GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }

        private void OnCardClickedHandler(ICard card)
        {
            var cardGameObject = ((MonoBehaviour)card).gameObject;
            GameManager.Instance.Tabletop.CardManager.SelectCard(cardGameObject);
        }

        private void OnPlaceSpaceClickedHandler(Transform placeSpace)
        {
            GameManager.Instance.Tabletop.CardManager.PlaceSelectedCard(placeSpace);
        }
    }
}

