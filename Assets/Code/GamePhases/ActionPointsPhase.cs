using System;
using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }
        /// <summary>
        /// Carta seleccionada
        /// </summary>
        private ICard selectedCard;
        bool hasActionPoints;
        bool hasResources;
        bool isProduced;

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
            Console.WriteLine("Executing Action Points Phase Logic.");

            // Comprobar si hay una carta seleccionada
            if (selectedCard != null)
            {
                // Verificar si hay suficientes puntos de acción para usar la carta
                hasActionPoints = GameManager.Instance.Tabletop.StorageManager.CheckActionPoints(selectedCard.ActionPointsCost);

                if (hasActionPoints)
                {
                    List<ResourceAmount> requiredResources = new List<ResourceAmount>();
                    requiredResources.Add(new ResourceAmount { resourceQuantity = 1, resourceType = GameResource.Cereal });

                    List<ResourceAmount> producedResources = new List<ResourceAmount>();
                    producedResources.Add(new ResourceAmount { resourceQuantity = 1, resourceType = GameResource.Milk });

                    // Verificar si hay suficientes recursos para la acción de la carta
                    hasResources = GameManager.Instance.Tabletop.StorageManager.CheckResources(requiredResources, producedResources);

                    if (hasResources)
                    {
                        // Ejecutar la acción de la carta y producir/consumir los recursos necesarios
                        isProduced = GameManager.Instance.Tabletop.StorageManager.ProduceResources();
                        Console.WriteLine($"Action executed for card {selectedCard.Name}.");
                        if (isProduced)
                        {
                            Console.WriteLine("Resources have been produced :)");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Not enough resources.");
                    }
                }
                else
                {
                    Console.WriteLine($"Not enough action points.");
                }
            }
            else
            {
                Console.WriteLine("No card selected.");
            }
        }


        public void EndPhase()
        {
            Console.WriteLine("Ending Action Points Phase");

            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;

            // Limpiar el tablero
            GameManager.Instance.Tabletop.CardManager.WipeBoard();
        }

        private void OnCardClickedHandler(ICard card)
        {
            //comprobar que si se ha seleccionado una carta
            if (card != null)
            {
                Console.WriteLine($"Card selected: {card.Name}");
                selectedCard = card;  // Almacena la carta seleccionada                   
            }
            else
            {
                Console.WriteLine("No card selected.");
            }

        }



    }





}
