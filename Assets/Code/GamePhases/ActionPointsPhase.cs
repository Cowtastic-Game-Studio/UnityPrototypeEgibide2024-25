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
            StatisticsManager.Instance.CheckPlayedCards();
        }


        public void ExecutePhase()
        {
        }


        public void EndPhase()
        {
            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;

            //ALBA: quitar cundo se arregle el poder hacer click en cartas colocadas
            //GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }

        private void OnCardClickedHandler(ICard card)
        {
            //comprobar que si se ha seleccionado una carta
            if (card != null)
            {
                //Debug.Log($"Card selected: {card.Name}");
                //MessageManager.Instance.ShowMessage($"Card selected: {card.Name}");

                // Si la carta es del tipo PlaceActivator o PlaceMultiplier
                if (card.Type == CardType.PlaceActivator || card.Type == CardType.PlaceMultiplier)
                {
                    // Obtener el GameObject de la carta seleccionada
                    GameObject cardObject = (card as CardBehaviour).gameObject;

                    // Obtener los hermanos del GameObject de la carta
                    Transform[] siblings = cardObject.transform.parent.GetComponentsInChildren<Transform>();

                    // Iterar a través de los hermanos
                    foreach (Transform sibling in siblings)
                    {
                        // Asegurarse de que el hermano no sea el mismo GameObject
                        if (sibling.gameObject != cardObject)
                        {
                            // Llamar a CheckAgainstStorage con el hermano
                            ICard siblingCard = sibling.GetComponent<ICard>(); // Asegúrate de que el hermano tenga un componente ICard
                            if (siblingCard != null)
                            {
                                CheckAgainstStorage(siblingCard);
                                break; // Salir después de encontrar el primer hermano que no sea el mismo
                            }
                        }
                    }
                }
                else
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
                return;
            }

            // Verificar si hay suficientes recursos para la acción de la carta
            hasResources = GameManager.Instance.Tabletop.StorageManager.CheckResources(selectedCard.RequiredResources, selectedCard.ProducedResources);

            if (!hasResources)
            {
                return;
            }


            // Extraer el CardTemplate
            MonoBehaviour cardComponent = selectedCard as MonoBehaviour;
            Transform parentTransform = cardComponent.transform.parent;
            bool hasMultiplier = false;
            CardTemplate cardTemplateMulti = null;

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform childTransform = parentTransform.GetChild(i);
                CardBehaviour cardBehaviour = childTransform.GetComponent<CardBehaviour>();
                if (cardBehaviour != null)
                {
                    CardTemplate cardTemplate = cardBehaviour.GetTemplate();

                    if (cardTemplate.cardType == CardType.PlaceMultiplier)
                    {
                        hasMultiplier = true;
                        cardTemplateMulti = cardTemplate;
                    }
                }
            }

            if (hasMultiplier)
            {
                GameManager.Instance.Tabletop.StorageManager.SetResourceMultiplierCardAndType(cardTemplateMulti.multiplier, cardTemplateMulti.targetResoruceType);
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

            if (hasMultiplier)
            {
                GameManager.Instance.Tabletop.StorageManager.ClearResourceMultiplierCardAndType();
            }
        }
    }
}
