using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionPointsPhase : IGamePhaseWUndo
    {
        public ActionManager<ICommand> ActionManager { get; private set; }
        
        private ICard selectedCard;  // Variable para almacenar la carta seleccionada
        bool hasActionPoints;
        bool hasResources;

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



        // C�digo que define la l�gica de la fase
        //check clickn card(de algun sitio)

        //GameManager.Instance.Tabletop.OnCardUseActionPoints();
        //checkap
        //comprobnar npa con el card
        //checkresources(nresources card)->nresources
        //

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
                    // Verificar si hay suficientes recursos para la acción de la carta
                    hasResources = GameManager.Instance.Tabletop.StorageManager.CheckResources(selectedCard.RequiredResources, selectedCard.ProducedResources);

                    if (hasResources)
                    {
                        // Ejecutar la acción de la carta y producir/consumir los recursos necesarios
                        GameManager.Instance.Tabletop.StorageManager.ProduceResources();
                        Console.WriteLine($"Action executed for card {selectedCard.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"Not enough resources to use card {selectedCard.Name}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Not enough action points to use card {selectedCard.Name}.");
                }
            }
            else
            {
                Console.WriteLine("No card has been selected.");
            }
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
            if (card != null)
            {
                //ICard card = cardGameObject.GetComponent<ICard>();

                if (card != null)
                {
                    Console.WriteLine($"Card {card.Name} clicked in Action Points Phase.");
                    selectedCard = card;  // Almacena la carta seleccionada

                   
                }
                else
                {
                    Console.WriteLine("The clicked GameObject does not have an ICard component.");
                }
            }
            else
            {
                Console.WriteLine("No GameObject was clicked.");
            }
        }



    }





}
