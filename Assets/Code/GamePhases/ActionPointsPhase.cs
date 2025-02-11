using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        #region Properties

        #region Property: Phase
        public GamePhaseTypes Phase { get { return GamePhaseTypes.Action; } }

        #endregion

        #endregion

        public ActionManager<ICommand> ActionManager { get; private set; }
        /// <summary>
        /// Carta seleccionada
        /// </summary>
        bool hasActionPoints;
        bool hasResources;
        bool isProduced;

        public ActionPointsPhase()
        {
            ActionManager = new ActionManager<ICommand>();
        }

        public void EnterPhase()
        {
            // Suscribirse al evento global de clic de carta en GameManager
            GameManager.Instance.OnCardClickedGlobal += OnCardClickedHandler;
        }


        public void ExecutePhase()
        {
        }


        public void EndPhase()
        {
            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;

            // Limpiar el tablero
            GameManager.Instance.Tabletop.CardManager.WipeBoard();

            //ALBA: quitar cundo se arregle el poder hacer click en cartas colocadas
            //GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }

        private void OnCardClickedHandler(ICard card)
        {
            //comprobar que si se ha seleccionado una carta
            if (card != null)
            {
                //Debug.Log($"Card selected: {card.Name}");
                MessageManager.Instance.ShowMessage($"Card selected: {card.Name}");

                CheckAgainstStorage(card);
            }
            else
            {
                Debug.Log("No card selected.");
            }

        }

        private void CheckAgainstStorage(ICard selectedCard)
        {
            // Comprobar si hay una carta seleccionada
            if (selectedCard == null)
            {
                Debug.Log("No card selected.");
                return;
            }

            // Verificar si hay suficientes puntos de acción para usar la carta
            hasActionPoints = GameManager.Instance.Tabletop.StorageManager.CheckActionPoints(selectedCard.ActionPointsCost);

            if (!hasActionPoints)
            {
                MessageManager.Instance.ShowMessage("Not action points.");
                Debug.LogWarning($"Not enough action points."); // No estoy muy segura
                return;
            }

            // Verificar si hay suficientes recursos para la acción de la carta
            hasResources = GameManager.Instance.Tabletop.StorageManager.CheckResources(selectedCard.RequiredResources, selectedCard.ProducedResources);

            if (!hasResources)
            {
                MessageManager.Instance.ShowMessage("Not enough resources.");
                Debug.LogWarning($"Not enough resources."); // No estoy muy segura
                return;
            }


            // Extraer el CardTemplate
            MonoBehaviour cardComponent = selectedCard as MonoBehaviour;
            Transform parentTransform = cardComponent.transform.parent;
            Transform firstChildTransform = parentTransform.GetChild(0);
            CardBehaviour cardBehaviourMultiplier = firstChildTransform.GetComponent<CardBehaviour>();
            CardTemplate cardTemplate = cardBehaviourMultiplier.GetTemplate();

            if (cardTemplate != null)
            {
                GameManager.Instance.Tabletop.StorageManager.SetResourceMultiplierCardAndType(cardTemplate.multiplier, cardTemplate.targetResoruceType);
            }

            // Ejecutar la acción de la carta y producir/consumir los recursos necesarios
            isProduced = GameManager.Instance.Tabletop.StorageManager.ProduceResources();
            Debug.Log($"Action executed for card {selectedCard.Name}.");

            if (isProduced)
            {
                CardBehaviour cardBehaviour = selectedCard as CardBehaviour;
                cardBehaviour.Deactivate();
                GameManager.Instance.Tabletop.HUDManager.UpdateResources();
                StatisticsManager.Instance.UpdateByType(selectedCard);
                Debug.LogWarning("Resources have been produced :)"); // No estoy muy segura
            }

            if (cardTemplate != null)
            {
                GameManager.Instance.Tabletop.StorageManager.ClearResourceMultiplierCardAndType();
            }
        }
    }
}
