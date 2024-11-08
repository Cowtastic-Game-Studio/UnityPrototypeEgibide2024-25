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

        // Variables para Drag & Drop
        private bool isDragging = false;
        private Transform draggedObject;
        private Vector3 dragOffset;
        private float objectZDepth;
        private float fixedYPosition;
        private Vector3 originalPosition;
        private Color originalColor;
        private bool isOverPlace = false;
        public float placementHeightOffset = 0.2f;

        private void Start()
        {
            // Comentado ya que la inicializacion la hace el SetUpPhase
            //InitializeDeck();
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
        }

        private void MoveLastCardsToHand(int cardsToDraw)
        {
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
                }
            }
        }


        /// <summary>
        /// Barajea las cartas del mazo de descarte y la mueve al mazo de robo
        /// </summary>
        public void ShuffleDiscardDeck()
        {
            // TODO: implementar la lógica para barajar el mazo de descarte
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
            foreach (GameObject card in playedCardsDeck.Cards)
            {
                card.transform.SetParent(discardDeckArea);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.Euler(90f, -90f, 0f);
                SetCardState(card, CardState.onDiscard);
            }

            // Reiniciar el mazo de cartas jugadas
            playedCardsDeck = new CardDeck();
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
        public void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseDown();
            }
            if (isDragging)
            {
                HandleMouseDrag();
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleMouseUp();
            }
        }

        private bool IsCardTag(string tag)
        {
            return tag == "CardCow" || tag == "CardSeed" || tag == "CardClient";
        }

        private void HandleMouseDown()
        {
            RaycastHit hit;
            if (RaycastFromMouse(out hit) && IsCardTag(hit.collider.tag))
            {
                StartDragging(hit.collider.transform);
            }
        }

        private void HandleMouseDrag()
        {
            Vector3 mousePosition = GetMouseWorldPosition();

            if (IsMouseOverPlace(out RaycastHit hitBelow))
            {
                if (!isOverPlace)
                {
                    ChangeColor(Color.black);
                    isOverPlace = true;
                }
            }
            else
            {
                if (isOverPlace)
                {
                    RevertColor();
                    isOverPlace = false;
                }
            }

            UpdateObjectPosition(mousePosition);
        }

        private void HandleMouseUp()
        {
            if (draggedObject != null)
            {
                if (IsMouseOverPlace(out RaycastHit hitBelow))
                {
                    SnapToPlace(hitBelow);
                }
                else
                {
                    ReturnToOriginalPosition();
                }
                StopDragging();
            }
        }

        private void StartDragging(Transform objectToDrag)
        {
            draggedObject = objectToDrag;
            isDragging = true;
            objectZDepth = Camera.main.WorldToScreenPoint(draggedObject.position).z;
            fixedYPosition = draggedObject.position.y + 1;
            originalPosition = draggedObject.position;

            Vector3 mousePosition = GetMouseWorldPosition();
            dragOffset = draggedObject.position - new Vector3(mousePosition.x, 0, mousePosition.z);

            //Renderer renderer = draggedObject.GetComponent<Renderer>();
            //if (renderer != null)
            //{
            //    originalColor = renderer.material.color;
            //}
        }

        private void StopDragging()
        {
            isDragging = false;
            if (isOverPlace)
            {
                RevertColor();
                isOverPlace = false;
            }
            draggedObject = null;
        }

        private void UpdateObjectPosition(Vector3 mousePosition)
        {
            draggedObject.position = new Vector3(mousePosition.x + dragOffset.x, fixedYPosition, mousePosition.z + dragOffset.z);
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectZDepth);
            return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        }

        private bool RaycastFromMouse(out RaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit);
        }

        private bool IsMouseOverPlace(out RaycastHit hitBelow)
        {
            Ray downwardRay = new Ray(draggedObject.position, Vector3.down);
            return Physics.Raycast(downwardRay, out hitBelow) && hitBelow.collider.CompareTag("Place");
        }

        private void ChangeColor(Color color)
        {
            Renderer renderer = draggedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                //renderer.material.color = color;
            }
        }

        private void RevertColor()
        {
            Renderer renderer = draggedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                //renderer.material.color = originalColor;
            }
        }

        private void SnapToPlace(RaycastHit hitBelow)
        {
            // Obtener el centro del collider del objeto golpeado
            Vector3 hitPosition = hitBelow.collider.bounds.center;

            // Colocar el objeto arrastrado en el mismo centro que el objeto golpeado
            draggedObject.position = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z + placementHeightOffset);

            // Remover la carta del handDeck
            RemoveCardFromHand(draggedObject.gameObject);

            // Cambiar el estado de la carta a 'onTable'
            SetCardState(draggedObject.gameObject, CardState.onTable);
        }

        private void ReturnToOriginalPosition()
        {
            draggedObject.position = originalPosition;
        }

        private void RemoveCardFromHand(GameObject card)
        {
            // Usar un stack temporal para almacenar las cartas restantes
            Stack<GameObject> tempStack = new Stack<GameObject>();

            while (handDeck.Cards.Count > 0)
            {
                GameObject currentCard = handDeck.Draw();
                if (currentCard != card)
                {
                    tempStack.Push(currentCard);
                }
            }

            // Regresa las cartas que no fueron eliminadas al handDeck
            while (tempStack.Count > 0)
            {
                handDeck.Place(tempStack.Pop());
            }

            Debug.Log($"Se ha eliminado la carta {card.name} de la mano.");
        }
    }
}
