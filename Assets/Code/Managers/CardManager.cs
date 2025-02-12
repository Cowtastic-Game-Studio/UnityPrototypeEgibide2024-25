using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clipStealCard, clipDiscard, clipPlaceCard, clipSelectedCard;
        private ButtonSoundManager buttonSoundManager;
        [SerializeField] private SODeck initialCards;

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
        private float cardSpacing = 0.1f;

        /// <summary>
        /// Variable para definir desde el editor la cantidad de cartas a robar por turno.
        /// </summary>
        [SerializeField]
        private int drawCards = 5;

        private float hoveredHeight = 0.2f;

        //TODO: revisar esto en clase, usar las Propiedades en mayus, da problemas si no se gestiona bien
        // Properties implementing ICardsManager
        public IDeck DrawDeck => drawDeck;
        public IDeck HandDeck => handDeck;
        public IDeck PlayedDeck => playedCardsDeck;
        public IDeck DiscardDeck => discardDeck;

        //Place card
        private GameObject selectedCard = null;
        private CardBehaviour hoveredCard = null;

        private bool isDragging = false;
        public bool IsDraggingCard => isDragging;

        public float placementHeightOffset = 4f;
        private Vector3 originalPosition;

        public GameObject cardPrefab;

        [SerializeField]
        private List<CardTemplate> cardTemplates = new List<CardTemplate>();

        private Dictionary<string, CardTemplate> cardNameMap;

        private void Awake()
        {
            InitializeCardNameMap();
        }

        private void InitializeCardNameMap()
        {
            // Inicializa el diccionario vacío
            cardNameMap = new Dictionary<string, CardTemplate>();

            // Recorre cada CardTemplate en la lista cardTemplates
            foreach (var cardTemplate in cardTemplates)
            {
                if (!cardNameMap.ContainsKey(cardTemplate.name))
                {
                    cardNameMap.Add(cardTemplate.name, cardTemplate);
                }
                else
                {
                    // Si ya existe una entrada con el mismo nombre
                    // actualizarla
                    cardNameMap[cardTemplate.name] = cardTemplate;
                }
            }
        }

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
            foreach (CardTemplate cardTemplate in initialCards.Cards)
            {
                GameObject newCard = Instantiate(cardPrefab, deckArea);
                newCard.name = cardTemplate.name;
                newCard.transform.SetParent(deckArea.transform);
                newCard.transform.localPosition = Vector3.zero;
                newCard.transform.localRotation = Quaternion.identity;

                CardBehaviour cardBH = newCard.GetComponent<CardBehaviour>();
                cardBH.setCardTemplate(cardTemplate);

                // Agrega la carta al mazo
                drawDeck.Place(newCard);
            }

            drawDeck.Shuffle();
        }

        public void DrawFromDeck()
        {
            source.PlayOneShot(clipStealCard);
            MoveLastCardsToHand(drawCards);
            source.PlayOneShot(clipStealCard);
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
                    cardToMove.transform.localRotation = Quaternion.identity;
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
            //ArrangeHand();
            ArrangeCardsInCurve();
        }

        void ArrangeCardsInCurve()
        {
            List<GameObject> cardsInHand = new List<GameObject>(handDeck.Cards.ToArray());
            int cardCount = cardsInHand.Count;
            if (cardCount == 0) return;

            float baseArcAngle = 15f; // Ángulo base para 5 cartas
            float arcAngle = Mathf.Clamp(baseArcAngle + (cardCount - 5) * 4f, 15f, 50f); // Ajuste dinámico
            float radius = Mathf.Clamp(3.5f + (cardCount - 5) * 0.2f, 3f, 5f); // Radio ajustado

            // Reducir la separación cuando solo hay 2 cartas
            if (cardCount == 2) arcAngle = 5f;
            if (cardCount == 1) arcAngle = 0f;

            float startAngle = -arcAngle / 2f;
            float angleStep = cardCount > 1 ? arcAngle / (cardCount - 1) : 0;

            for (int i = 0; i < cardCount; i++)
            {
                GameObject card = cardsInHand[i];
                if (card != null)
                {
                    float angle = startAngle + (angleStep * i);
                    float radian = angle * Mathf.Deg2Rad;

                    // Posición en curva
                    Vector3 cardPosition = new Vector3(
                        Mathf.Sin(radian) * radius,
                        Mathf.Cos(radian) * radius - radius,
                        i * -0.015f
                    );

                    // Rotación para que sigan la curva
                    Quaternion cardRotation = Quaternion.Euler(0, 0, -angle * 0.75f);

                    // Aplicar transformaciones                   
                    cardsInHand[i].transform.localPosition = cardPosition;
                    cardsInHand[i].transform.localRotation = cardRotation;
                    var cardBH = card.GetComponent<CardBehaviour>();
                    cardBH.IsPlaced = false;
                    cardBH.Activate();
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
                Debug.LogWarning("No hay cartas en el mazo de descarte para barajar."); // ESTA NO
                return;
            }
            // Mueve las cartas de nuevo al mazo de robo
            foreach (ICard card in discardCards)
            {
                GameObject cardGameObject = ((MonoBehaviour)card).gameObject;
                cardGameObject.transform.SetParent(deckArea);
                cardGameObject.transform.localPosition = Vector3.zero;
                cardGameObject.transform.localRotation = Quaternion.identity;

                ResetCardRotation(cardGameObject);
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
            source.PlayOneShot(clipDiscard);

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
                        ResetCardRotation(cardToMove);
                    }
                }
                drawDeck.Shuffle();
                this.MoveLastCardsToHand(handNumber - 1);

                ArrangeCardsInCurve();

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
                    source.PlayOneShot(clipPlaceCard);
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
                card.transform.localRotation = Quaternion.identity;
                SetCardState(card, CardState.onDiscard);
            }
            else
            {
                Debug.LogWarning("La carta no se encuentra en la mano."); // ESTA NO
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
                    card.transform.localRotation = Quaternion.identity;
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

        /// <summary>
        /// Resaltar la carta de la mano al hacer hover
        /// </summary>
        /// <param name="card"></param>
        public void SetHoveredCard(CardBehaviour card)
        {
            if (card.Equals(hoveredCard)) return;
            if (hoveredCard)
            {
                hoveredCard.gameObject.transform.localPosition = new Vector3(hoveredCard.gameObject.transform.localPosition.x, hoveredCard.gameObject.transform.localPosition.y, hoveredCard.gameObject.transform.localPosition.z - hoveredHeight);
            }
            card.gameObject.transform.localPosition = new Vector3(card.gameObject.transform.localPosition.x, card.gameObject.transform.localPosition.y, card.gameObject.transform.localPosition.z + hoveredHeight);
            hoveredCard = card;

        }

        /// <summary>
        /// Resetear el resaltado de la carta caundo se deja de hacer hover
        /// </summary>
        /// <param name="card"></param>
        public void ClearHoveredCard(CardBehaviour card)
        {
            if (hoveredCard == card)
            {
                hoveredCard.gameObject.transform.localPosition = new Vector3(
                    hoveredCard.gameObject.transform.localPosition.x,
                    hoveredCard.gameObject.transform.localPosition.y,
                    hoveredCard.gameObject.transform.localPosition.z - hoveredHeight
                );
                hoveredCard = null;
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
                    source.PlayOneShot(clipSelectedCard);
                    selectedCard.GetComponent<CardBehaviour>()?.Deactivate();
                }

                selectedCard = value;

                if (selectedCard != null)
                {
                    source.PlayOneShot(clipSelectedCard);
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

                ResetCardRotation(card);
                StartDragging();
            }
        }

        private void StartDragging()
        {
            isDragging = true;

            // Ocultar todas las cartas en la mano excepto la seleccionada
            foreach (Transform card in handArea.transform)
            {
                if (card.gameObject != selectedCard)
                {
                    card.gameObject.SetActive(false);
                }
            }

            //roatcion para que la carta mire a camara
            if (selectedCard.transform.parent.CompareTag("Place") && selectedCard.transform.rotation.y != 180 && selectedCard.transform.rotation.y != -90)
            {
                selectedCard.transform.rotation = Quaternion.Euler(-90, 0, 90);

                var placeSpace = selectedCard.transform.parent?.GetComponent<PlaceSpaceBehaviour>();
                if (placeSpace != null)
                {
                    placeSpace.updateEmpty();
                }
            }

            // Activar el outline de las cartas del mismo tipo
            if (selectedCard != null)
            {
                var cardBH = selectedCard.GetComponent<CardBehaviour>();
                GameManager.Instance.Tabletop.OutlineByResource(cardBH.Type, true);
            }
        }

        public void StopDragging()
        {
            if (isDragging && selectedCard != null)
            {
                // Devuelve la carta a su posición original
                if (playedCardsDeck.Cards.Contains(selectedCard))
                    selectedCard.transform.position = originalPosition;
                else
                    selectedCard.transform.SetParent(handArea);
            }

            isDragging = false;

            //handDeck.Place(selectedCard);
            //playedCardsDeck.RemoveCard(selectedCard);

            //ArrangeCardsInCurve();

            // Mostrar todas las cartas en la mano nuevamente
            foreach (Transform card in handArea.transform)
            {
                card.gameObject.SetActive(true);
            }

            // Desactivar el outline de las cartas del mismo tipo
            if (selectedCard != null)
            {
                var cardBH = selectedCard.GetComponent<CardBehaviour>();
                GameManager.Instance.Tabletop.OutlineByResource(cardBH.Type, false);
            }
        }

        public void UpdatePlacement()
        {
            if (isDragging && selectedCard != null)
            {
                MoveSelectedCardWithMouse();

                if (Input.GetMouseButtonDown(1))
                {

                    //handDeck.Place(selectedCard);
                    //playedCardsDeck.RemoveCard(selectedCard);

                    StopDragging();

                    ArrangeCardsInCurve();
                }
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
                    source.PlayOneShot(clipPlaceCard);
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

                    if (cardBH.Type == CardType.PlaceActivator)
                    {
                        MoveLastCardsToHand(1);
                    }

                    if (cardBH.Type == CardType.PlaceMultiplier)
                    {
                        // Desactivo el box colaider de la carta seleccionada
                        cardBH.transform.localPosition += new Vector3(0, 0, -0.06f);

                        MoveLastCardsToHand(1);
                    }

                    ArrangeCardsInCurve();

                    // Desactivar el outline de las cartas del mismo tipo
                    GameManager.Instance.Tabletop.OutlineByResource(cardBH.Type, false);
                }
            }
        }

        /// <summary>
        /// Quita la carta de un espacio de colocado y la devuelve a la mano
        /// </summary>
        public void RemovePlacedCard(GameObject card)
        {
            // Verificar si el padre tiene el componente PlaceSpaceBehaviour
            var placeSpace = card.transform.parent?.GetComponent<PlaceSpaceBehaviour>();
            if (placeSpace != null)
            {
                placeSpace.updateEmpty();
            }

            // Proceder a mover la carta de regreso a la mano
            handDeck.Place(card);

            var cardBH = card.GetComponent<CardBehaviour>();
            cardBH.IsPlaced = false;

            card.transform.SetParent(handArea);

            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;

            playedCardsDeck.RemoveCard(card);

            ArrangeCardsInCurve();
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

            foreach (GameObject card in playedCardsDeck.Cards)
            {
                CardBehaviour cardBH = card.GetComponent<CardBehaviour>();
                cardBH.Activate();
            }
        }


        public void BuyCard(string cardName)
        {
            if (cardNameMap.ContainsKey(cardName))
            {
                CardTemplate cardTemplate = cardNameMap[cardName];

                GameObject newCard = Instantiate(cardPrefab, deckArea);
                newCard.name = cardName;
                newCard.transform.SetParent(deckArea.transform);
                newCard.transform.localPosition = Vector3.zero;
                newCard.transform.localRotation = Quaternion.identity;

                CardBehaviour cardBH = newCard.GetComponent<CardBehaviour>();
                cardBH.setCardTemplate(cardTemplate);

                Debug.LogWarning($"Card buyed {cardName}");

                // Agrega la carta al mazo
                drawDeck.Place(newCard);
                ArrangeCardsInCurve();
                //StatisticsManager.UpdateByBuyedCard(cardBH);
                StatisticsManager.Instance.UpdateByStatisticType(StatisticType.CardsPurchased, 1);
            }
        }

        public GameObject getSelectedCard()
        {
            return selectedCard;
        }

        public List<GameObject> getAllCardsList()
        {
            return discardDeck.Cards.Concat(
                    drawDeck.Cards).Concat(
                    playedCardsDeck.Cards).ToList();
        }

        public void showHand()
        {
            handArea.gameObject.SetActive(true);
        }
        public void hideHand()
        {
            handArea.gameObject.SetActive(false);
        }
        public void DeleteCards(List<CardToDelete> cardsToDelete)
        {
            foreach (var cardToDelete in cardsToDelete)
            {
                // Buscar cartas del tipo especificado en el discardDeck, drawDeck, y playedCardsDeck
                int remainingQuantity = cardToDelete.Quantity;

                // Intentar eliminar cartas del discardDeck
                remainingQuantity = TryRemoveCardsFromDeck(discardDeck, cardToDelete.CardType, remainingQuantity);

                // Intentar eliminar cartas del drawDeck
                if (remainingQuantity > 0)
                {
                    remainingQuantity = TryRemoveCardsFromDeck(drawDeck, cardToDelete.CardType, remainingQuantity);
                }

                // Intentar eliminar cartas del playedCardsDeck
                if (remainingQuantity > 0)
                {
                    remainingQuantity = TryRemoveCardsFromDeck(playedCardsDeck, cardToDelete.CardType, remainingQuantity);
                }

                // Si aún queda alguna cantidad no eliminada, puedes manejarlo si es necesario
                if (remainingQuantity > 0)
                {
                    Debug.LogWarning($"No se pudieron eliminar todas las cartas del tipo {cardToDelete.CardType}. Cartas restantes: {remainingQuantity}");
                }
            }
        }

        private int TryRemoveCardsFromDeck(IDeck deck, CardType cardType, int quantityToRemove)
        {
            // Filtrar las cartas del deck que coincidan con el tipo especificado
            var cardsToRemove = deck.Cards.Where(card => card.GetComponent<CardBehaviour>()?.GetTemplate().cardType == cardType).ToList();

            // Limitar la cantidad a eliminar según la cantidad restante
            int cardsRemoved = 0;

            // Eliminar cartas hasta que la cantidad a eliminar sea 0 o no haya más cartas que coincidan
            foreach (var card in cardsToRemove)
            {
                if (cardsRemoved >= quantityToRemove)
                    break;

                // Llamar a la función RemoveCard para eliminar la carta
                deck.RemoveCard(card);
                RemoveCard(card); // Llamar al método RemoveCard correspondiente

                cardsRemoved++;
            }

            return quantityToRemove - cardsRemoved;
        }

        public void TryRemoveCardsGOFromDecks(GameObject cardToDelete)
        {
            if (discardDeck.Cards.Contains(cardToDelete))
            {
                discardDeck.RemoveCard(cardToDelete);
                RemoveCard(cardToDelete);
            }
            else if (drawDeck.Cards.Contains(cardToDelete))
            {
                drawDeck.RemoveCard(cardToDelete);
                RemoveCard(cardToDelete);
            }
            else if (playedCardsDeck.Cards.Contains(cardToDelete))
            {
                playedCardsDeck.RemoveCard(cardToDelete);
                RemoveCard(cardToDelete);
            }
            else
            {
                Debug.Log("Miau Miau Miau Miau... :(");
            }
        }

        private void RemoveCard(GameObject card)
        {
            // Lógica de eliminación de carta (esta es la función que debe eliminar la carta en el juego)
            Destroy(card);  // Aquí solo se usa Destroy para eliminarla de la escena
        }

        private void ResetCardRotation(GameObject card)
        {
            card.transform.SetParent(deckArea);
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;

        }

    }
}
