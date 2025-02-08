using System.Collections.Generic;
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

            CheckFillPlaces();
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

        private static void CheckFillPlaces()
        {
            if (CheckMaxActiveFullSpaces(GameManager.Instance.Tabletop.farms))
            {
                StatisticsManager.Instance.UpdateByStatisticType(StatisticType.FarmFull, 1);
            }
            if (CheckMaxActiveFullSpaces(GameManager.Instance.Tabletop.stables))
            {
                StatisticsManager.Instance.UpdateByStatisticType(StatisticType.StableFull, 1);
            }
            if (CheckMaxActiveFullSpaces(GameManager.Instance.Tabletop.taverns))
            {
                StatisticsManager.Instance.UpdateByStatisticType(StatisticType.ShopFull, 1);
            }
        }


        private static bool CheckMaxActiveFullSpaces(List<PlaceSpaceBehaviour> completeList)
        {
            List<PlaceSpaceBehaviour> activeSpaceNotEmptyList = completeList.FindAll(f => f.GetIsActive() && !f.GetIsEmpty());
            return completeList.Count == activeSpaceNotEmptyList.Count;
        }
    }
}

