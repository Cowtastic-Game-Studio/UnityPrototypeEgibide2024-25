using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    /// <summary>
    /// Define el estado actual de cada carta
    /// </summary>
    public enum CardState
    {
        onDeck,
        onHand,
        onTable,
        onDiscard
    }

    public class CardManager : MonoBehaviour, ICardsManager
    {
        [SerializeField]
        private SODeck initialCards;

        /// <summary>
        /// Mazo de cartas.
        /// </summary>
        private IDeck drawDeck;

        /// <summary>
        /// Cartas en la mano.
        /// </summary>
        public IDeck handDeck;

        /// <summary>
        /// Cartas jugadas en la mesa.
        /// </summary>
        private IDeck playedCardsDeck;

        /// <summary>
        /// Cartas del descarte.
        /// </summary>
        private IDeck discardDeck;

        /// <summary>
        /// Lugar donde se encuentra el mazo de robo.
        /// </summary>
        [SerializeField]
        private Transform deckArea;

        /// <summary>
        /// Lugar donde se encuentra la mano y se colocar�n las cartas.
        /// </summary>
        [SerializeField]
        private Transform handArea;

        /// <summary>
        /// Lugar donde se encuentra el mazo de descarte.
        /// </summary>
        [SerializeField]
        private Transform discardDeckArea;

        /// <summary>
        /// Espaciado entre cartas en la mano.
        /// </summary>
        [SerializeField]
        private float cardSpacing = 0.4f;

        /// <summary>
        /// Variable para definir desde el editor la cantidad de cartas a robar por turno.
        /// </summary>
        [SerializeField]
        private int drawCards = 5;

        //TODO: revisar esto en clase, usar las Propiedades en mayus, da problemas si no se gestiona bien
        // Properties implementing ICardsManager
        public IDeck DrawDeck => drawDeck;
        public IDeck HandDeck => handDeck;
        public IDeck PlayedDeck => playedCardsDeck;
        public IDeck DiscardDeck => discardDeck;

        //Place card
        public GameObject selectedCard = null;

        private bool isDragging = false;
        public bool IsDraggingCard => isDragging;

        public float placementHeightOffset = 4f;
        private Vector3 originalPosition;

        /// <summary>
        /// Inicializa un mazo con cartas desde el ScriptableObject.
        /// </summary>
        public void InitializeDeck()
        {
            // Verifica que el ScriptableObject esté asignado y que la lista de cartas no esté vacía
            if (initialCards == null || initialCards.Cards == null || initialCards.Cards.Count == 0)
            {
                Debug.LogError("El deck inicial (initialCards) no está asignado o está vacío.");
                return;
            }

            // Inicializa los mazos
            drawDeck = new CardDeck();
            handDeck = new CardDeck();
            playedCardsDeck = new CardDeck();
            discardDeck = new CardDeck();

            // Recorre cada carta en el ScriptableObject y crea una instancia
            foreach (GameObject card in initialCards.Cards)
            {
                GameObject newCard = Instantiate(card, deckArea);
                newCard.transform.localPosition = Vector3.zero;

                // Agrega la carta al mazo
                drawDeck.Place(newCard);
            }

            drawDeck.Shuffle();
        }

        public void DrawFromDeck()
        {
            MoveLastCardsToHand(drawCards);

            InitHandCardLifes();
        }

        public void MoveLastCardsToHand(int cardsToDraw)
        {
            // Verificar que hay suficientes cartas en el mazo de robo
            if (drawDeck.Cards.Count < 5)
            {
                ShuffleDiscardDeck();
            }
            cardsToDraw = Mathf.Min(cardsToDraw, drawDeck.Cards.Count);

            for (int i = 0; i < cardsToDraw; i++)
            {
                GameObject cardToMove = drawDeck.Draw();
                if (cardToMove != null)
                {
                    // Cambiar la posición y padre de la carta para moverla a la mano
                    cardToMove.transform.SetParent(handArea);
                    cardToMove.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

                    // Agregar la carta a la lista de la mano
                    //handDeck.Place(cardToMove);
                    SetCardState(cardToMove, CardState.onHand);

                    //activar las cartas al ir a la mano
                    var cardBH = cardToMove.GetComponent<CardBehaviour>();
                    cardBH.Activate();
                }
            }

            // Organizar las cartas en la mano
            ArrangeHand();
        }

        private void ArrangeHand()
        {
            // Crea una lista temporal de cartas en la mano
            List<GameObject> cardsInHand = new List<GameObject>(handDeck.Cards.ToArray());

            for (int i = 0; i < cardsInHand.Count; i++)
            {
                GameObject card = cardsInHand[i]; // Obtén la carta de la lista temporal
                if (card != null)
                {
                    // Cambia la posición de la carta en la mano
                    card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);
                    card.transform.rotation = Quaternion.Euler(90, -90, 0);
                    var cardBH = card.GetComponent<CardBehaviour>();
                    cardBH.IsPlaced = false;
                    cardBH.PositionInHand = i * cardSpacing;
                }
            }
        }


        /// <summary>
        /// Barajea las cartas del mazo de descarte y las mueve al mazo de robo
        /// </summary>
        public void ShuffleDiscardDeck()
        {
            // Convierte las cartas en el área de descarte a una lista de ICard
            List<ICard> discardCards = new List<ICard>();
            foreach (Transform cardTransform in discardDeckArea)
            {
                ICard card = cardTransform.GetComponent<ICard>();
                if (card != null)
                {
                    discardCards.Add(card);
                }
            }
            if (discardCards.Count == 0)
            {
                Debug.LogWarning("No hay cartas en el mazo de descarte para barajar.");
                return;
            }
            // Mueve las cartas de nuevo al mazo de robo
            foreach (ICard card in discardCards)
            {
                GameObject cardGameObject = ((MonoBehaviour)card).gameObject;
                cardGameObject.transform.SetParent(deckArea);
                cardGameObject.transform.localPosition = Vector3.zero;
                cardGameObject.transform.localRotation = Quaternion.Euler(90f, -90f, 0f);
                drawDeck.Place(cardGameObject); // Vuelve a colocar en el mazo
            }
            // Baraja el mazo
            drawDeck.Shuffle();
        }

        /// <summary>
        /// Descarta la mano y roba 4 cartas
        /// </summary>
        public void Mulligan()
        {
            int handNumber = handDeck.Cards.Count;

            if (handNumber > 1)
            {
                for (int i = 0; i < handNumber; i++)
                {
                    GameObject cardToMove = handDeck.Draw();
                    if (cardToMove != null)
                    {
                        cardToMove.transform.SetParent(deckArea);
                        cardToMove.transform.localPosition = Vector3.zero;

                        SetCardState(cardToMove, CardState.onDeck);
                    }
                }
                drawDeck.Shuffle();
                this.MoveLastCardsToHand(handNumber - 1);

                //esconder botón mulligan si no hay más de una carta en la mano
                if (handDeck.Cards.Count <= 1)
                {
                    GameManager.Instance.Tabletop.HUDManager.HideMulliganButton();
                }
            }

        }

        /// <summary>
        /// Coloca la carta
        /// </summary>
        public void PutCard()
        {
            if (handDeck.Cards.Count > 0)
            {
                var card = handDeck.Draw();
                if (card != null)
                {
                    discardDeck.Place(card);
                    SetCardState(card, CardState.onDiscard);
                }
            }
        }

        /// <summary>
        /// Jugar una carta desde la mano y agregarla a las cartas jugadas.
        /// </summary>
        public void PlayCard(GameObject card)
        {
            if (handDeck.Cards.Contains(card))
            {
                handDeck.Draw();
                card.transform.SetParent(discardDeckArea);
                playedCardsDeck.Place(card);
                card.transform.localPosition = Vector3.zero;
                SetCardState(card, CardState.onTable);
            }
        }

        /// <summary>
        /// Mueve una carta desde la mano al mazo de descarte.
        /// </summary>
        /// <param name="card">La carta que se desea descartar.</param>
        public void DiscardCardFromHand(GameObject card)
        {
            if (handDeck.Cards.Contains(card))
            {
                handDeck.Draw();
                card.transform.SetParent(discardDeckArea);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.Euler(90f, -90f, 0f);
                SetCardState(card, CardState.onDiscard);
            }
            else
            {
                Debug.LogWarning("La carta no se encuentra en la mano.");
            }
        }


        /// <summary>
        /// Descarta todas las cartas de la mano.
        /// </summary>
        public void DiscardHand()
        {
            // Iterar sobre una copia de handDeck para evitar modificar la lista mientras la recorremos.
            List<GameObject> cardsToDiscard = new List<GameObject>(handDeck.Cards);

            foreach (GameObject card in cardsToDiscard)
            {
                DiscardCardFromHand(card);
            }
        }


        /// <summary>
        /// Limpia las cartas de la mesa y las coloca en el mazo de descarte.
        /// </summary>
        public void WipeBoard()
        {
            UpdatePlacedCardLifes();

            foreach (GameObject card in playedCardsDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();
                if (cardBH.LifeCycleDaysRemaining <= 0)
                {
                    handDeck.RemoveCard(card);

                    card.transform.SetParent(discardDeckArea);
                    card.transform.localPosition = Vector3.zero;
                    card.transform.localRotation = Quaternion.Euler(90f, -90f, 0f);
                    SetCardState(card, CardState.onDiscard);
                }
            }

            // Reiniciar el mazo de cartas jugadas
            //playedCardsDeck = new CardDeck();
        }


        /// <summary>
        /// Limpia las cartas
        /// </summary>
        public void CleanPlayerCards()
        {
            // Limpiar mano
            handDeck = new CardDeck();
            // Limpiar mazo de robo
            drawDeck = new CardDeck();
        }

        /// Refactor
        public void SetCardState(GameObject card, CardState newState)
        {
            var cardComponent = card.GetComponent<ICard>();
            if (cardComponent == null) return;

            // Actualizar el estado de la carta
            cardComponent.State = newState;

            // Colocar la carta en el mazo correcto según el nuevo estado
            switch (newState)
            {
                case CardState.onDeck:
                    drawDeck.Place(card);
                    break;
                case CardState.onHand:
                    handDeck.Place(card);
                    break;
                case CardState.onTable:
                    playedCardsDeck.Place(card);
                    break;
                case CardState.onDiscard:
                    discardDeck.Place(card);
                    break;
            }
        }

        //drag and drop

        public GameObject SelectedCard
        {
            get => selectedCard;
            private set
            {
                if (selectedCard != null)
                {
                    selectedCard.GetComponent<CardBehaviour>()?.Deactivate();
                }

                selectedCard = value;

                if (selectedCard != null)
                {
                    selectedCard.GetComponent<CardBehaviour>()?.Activate();
                }
            }
        }

        public void SelectCard(GameObject card)
        {
            if (card != null && card.GetComponent<CardBehaviour>() != null)
            {
                SelectedCard = card;

                // Guarda la posición original de la carta
                originalPosition = card.transform.position;

                StartDragging();
            }
        }

        private void StartDragging()
        {
            isDragging = true;

            if (selectedCard.transform.parent.CompareTag("Place") && selectedCard.transform.rotation.y != 180 && selectedCard.transform.rotation.y != -90)
            {
                selectedCard.transform.rotation = Quaternion.Euler(-90, 0, 90); ;
            }
        }

        public void StopDragging()
        {
            if (isDragging && selectedCard != null)
            {
                // Devuelve la carta a su posición original
                selectedCard.transform.position = originalPosition;
            }

            isDragging = false;
        }

        public void UpdatePlacement()
        {
            if (isDragging && selectedCard != null)
            {
                MoveSelectedCardWithMouse();
            }
        }

        private void MoveSelectedCardWithMouse()
        {
            // Detecta el estado actual para cambiar la capa de colisión
            LayerMask mask = isDragging ? LayerMask.GetMask("BoardLayer") : ~LayerMask.GetMask("BoardLayer");

            // Hacer el raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                // Mover la carta sobre el plano
                Vector3 newPosition = hit.point + Vector3.up;

                selectedCard.transform.position = newPosition;
            }
        }

        /// <summary>
        /// Coloca la carta en el espacio de colocacion(Mercado, establo, huerta)
        /// </summary>
        /// <param name="target"></param>
        public void PlaceSelectedCard(Transform target)
        {
            if (selectedCard != null)
            {
                if (target != null && target.gameObject.CompareTag("Place"))
                {
                    // Desactiva el arrastre
                    StopDragging();

                    // Coloca la carta en el lugar objetivo
                    selectedCard.transform.SetParent(target);
                    //rota la carta  a la rotacion del padre

                    selectedCard.transform.rotation = target.transform.rotation;

                    selectedCard.transform.position = target.transform.position;

                    selectedCard.transform.localPosition += new Vector3(
                        0,
                        0,
                        +placementHeightOffset
                    );

                    handDeck.RemoveCard(selectedCard);
                    // Actualiza el estado
                    SetCardState(selectedCard, CardState.onTable);

                    // Limpia la selección
                    var cardBH = selectedCard.GetComponent<CardBehaviour>();
                    cardBH.IsPlaced = true;
                    cardBH.Activate();
                    selectedCard = null;


                    //Special case
                    if (cardBH.Type == CardType.Helper)
                    {
                        cardBH.Deactivate();
                        GameManager.Instance.Tabletop.StorageManager.AddResourceUpToMax(6, GameResource.ActionPoints, false);
                        GameManager.Instance.Tabletop.HUDManager.UpdateResources();
                        MoveLastCardsToHand(1);
                    }

                }
            }
        }

        /// <summary>
        /// Quita la carta de un espacio de colocado y la devuelve a la mano
        /// </summary>
        public void RemovePlacedCard(GameObject card)
        {
            handDeck.Place(card);

            var cardBH = card.GetComponent<CardBehaviour>();
            cardBH.IsPlaced = false;

            card.transform.SetParent(handArea);
            card.transform.localPosition = new Vector3(cardBH.PositionInHand.Value, 0, 0);
            card.transform.rotation = handArea.transform.rotation;
            playedCardsDeck.RemoveCard(card);
        }


        private void InitHandCardLifes()
        {
            foreach (GameObject card in handDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();
                cardBH.LifeCycleDaysRemaining = cardBH.LifeCycleDays;
            }
        }

        private void UpdatePlacedCardLifes()
        {
            foreach (GameObject card in playedCardsDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();
                cardBH.LifeCycleDaysRemaining -= 1;
                //cardBH.Activate();
            }
        }

        public void ActivateHandDeckCards()
        {
            foreach (GameObject card in HandDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();
                cardBH.Activate();
            }
        }

        //TODO: change to receive scriptableObject CardTemplate
        // + move card prefab back to Prefab folder
        internal void buyCard(CardType cardType)
        {
            GameObject card = null;
            string path = "Cards/Prefab/";

            switch (cardType)
            {
                case CardType.Cow:
                    path += "CowCard";
                    break;
                case CardType.Seed:
                    path += "SeedCard";
                    break;
                case CardType.Customer:
                    path += "CustomerCard";
                    break;
                case CardType.None:
                default:
                    return;
            }

            card = Resources.Load<GameObject>(path);
            if (card != null)
            {
                GameObject newCard = Instantiate(card, deckArea);
                newCard.transform.SetParent(deckArea.transform);
                newCard.transform.localPosition = Vector3.zero;

                // Agrega la carta al mazo
                drawDeck.Place(newCard);
            }
        }
    }
}