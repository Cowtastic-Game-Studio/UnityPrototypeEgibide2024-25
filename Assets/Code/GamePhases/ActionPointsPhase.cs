using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
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
            Console.WriteLine("Entering Action Points Phase");

            // Suscribirse al evento global de clic de carta en GameManager
            GameManager.Instance.OnCardClickedGlobal += OnCardClickedHandler;
        }


        public void ExecutePhase()
        {
            //Console.WriteLine("Executing Action Points Phase Logic.");
        }


        public void EndPhase()
        {
            Console.WriteLine("Ending Action Points Phase");

            // Desuscribirse del evento global para evitar referencias persistentes
            GameManager.Instance.OnCardClickedGlobal -= OnCardClickedHandler;

            // Limpiar el tablero
            GameManager.Instance.Tabletop.CardManager.WipeBoard();

            //ALBA: quitar cundo se arregle el poder hacer click en cartas colocadas
            GameManager.Instance.Tabletop.CardManager.DiscardHand();
        }

        private void OnCardClickedHandler(ICard card)
        {
            //comprobar que si se ha seleccionado una carta
            if (card != null)
            {
                Console.WriteLine($"Card selected: {card.Name}");

                CheckAgainstStorage(card);
            }
            else
            {
                Console.WriteLine("No card selected.");
            }

        }

        private void CheckAgainstStorage(ICard selectedCard)
        {
            // Comprobar si hay una carta seleccionada
            if (selectedCard != null)
            {
                // Verificar si hay suficientes puntos de acción para usar la carta
                hasActionPoints = GameManager.Instance.Tabletop.StorageManager.CheckActionPoints(selectedCard.ActionPointsCost);

                if (hasActionPoints)
                {
                    // Verificar si hay suficientes recursos para la acción de la carta
                    hasResources = GameManager.Instance.Tabletop.StorageManager.CheckResources(selectedCard.RequiredResources, selectedCard.ProducedResources);

                    if (hasResources)
                    {
                        // Ejecutar la acción de la carta y producir/consumir los recursos necesarios
                        isProduced = GameManager.Instance.Tabletop.StorageManager.ProduceResources();
                        Console.WriteLine($"Action executed for card {selectedCard.Name}.");
                        if (isProduced)
                        {
                            GameManager.Instance.Tabletop.HUDManager.UpdateResources();
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
    }





}
