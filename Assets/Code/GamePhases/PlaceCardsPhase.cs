using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PlaceCardsPhase : IGamePhaseWUndo
    {
        #region Properties

        #region Property: Phase
        public GamePhaseTypes Phase { get { return GamePhaseTypes.PlaceCards; } }

        #endregion

        #endregion

        public ActionManager<ICommand> ActionManager { get; private set; }

        public PlaceCardsPhase()
        {
            // Inicializar el ActionManager
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            // C�digo para entrar en la fase
            GameManager.Instance.Tabletop.CardManager.ActivateHandDeckCards();

            // Suscribirse al evento global de clic de carta en GameManager
            GameManager.Instance.OnCardClickedGlobal += OnCardClickedHandler;
            GameManager.Instance.OnPlaceSpaceClickedGlobal += OnPlaceSpaceClickedHandler;
        }

        public void ExecutePhase()
        {
            // C�digo que define la l�gica de la fase
            GameManager.Instance.Tabletop.CardManager.UpdatePlacement();
        }

        public void EndPhase()
        {
            // C�digo para finalizar la fase
            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;
            GameManager.Instance.OnPlaceSpaceClickedGlobal -= OnPlaceSpaceClickedHandler;

            GameManager.Instance.Tabletop.CardManager.StopDragging();

            GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }

        private void OnCardClickedHandler(ICard card)
        {
            var cardGameObject = ((MonoBehaviour) card).gameObject;
            GameManager.Instance.Tabletop.CardManager.SelectCard(cardGameObject);
        }

        private void OnPlaceSpaceClickedHandler(Transform placeSpace)
        {
            GameManager.Instance.Tabletop.CardManager.PlaceSelectedCard(placeSpace);
        }
    }
}

