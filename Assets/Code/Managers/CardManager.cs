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
        onTable
    }

    public class CardManager : MonoBehaviour, ICardsManager
    {
        [SerializeField]
        private SODeck initialCards;

        /// <summary>
        /// Lista de cartas en el mazo.
        /// </summary>
        private List<GameObject> drawDeck = new List<GameObject>();

        /// <summary>
        /// Cartas en la mano.
        /// </summary>
        private List<GameObject> handDeck = new List<GameObject>();

        /// <summary>
        /// Cartas jugadas en la mesa.
        /// </summary>
        private List<GameObject> playedCards = new List<GameObject>();

        /// <summary>
        /// Lugar donde se encuentra el mazo de robo.
        /// </summary>
        public Transform deckArea;

        /// <summary>
        /// Lugar donde se encuentra la mano y se colocar�n las cartas.
        /// </summary>
        public Transform handArea;

        /// <summary>
        /// Lugar donde se encuentra el mazo de descarte.
        /// </summary>
        public Transform discardDeckArea;

        /// <summary>
        /// Espaciado entre cartas en la mano.
        /// </summary>
        public float cardSpacing = 0.4f;

        /// <summary>
        /// Variable para definir desde el editor la cantidad de cartas a robar por turno.
        /// </summary>
        public int drawCards = 5;

        // Implementación de la interfaz ICardsManager
        //TODO: revisar esto en clase, usar las Propiedades en mayus, da problemas si no se gestiona bien
        public IDeck DrawDeck => new CardDeck(drawDeck.ConvertAll(card => card.GetComponent<ICard>()));
        public List<ICard> HandDeck => handDeck.ConvertAll(card => card.GetComponent<ICard>());
        public IDeck DiscardDeck => new CardDeck(); // Para simplificar, puedes implementar un manejo real de descarte

        private void Start()
        {
            InitializeDeck();
        }

        /// <summary>
        /// Inicializa un mazo con cartas desde el ScriptableObject.
        /// </summary>
        private void InitializeDeck()
        {
            // Verifica que el ScriptableObject esté asignado y que la lista de cartas no esté vacía
            if (initialCards == null || initialCards.Cards == null || initialCards.Cards.Count == 0)
            {
                Debug.LogError("El deck inicial (initialCards) no está asignado o está vacío.");
                return;
            }

            // Recorre cada carta en el ScriptableObject y crea una instancia
            foreach (GameObject card in initialCards.Cards)
            {
                // Instancia la carta desde el ScriptableObject
                GameObject newCard = Instantiate(card, deckArea);
                newCard.transform.localPosition = Vector3.zero;

                // Agrega la carta al mazo
                drawDeck.Add(newCard);
            }
        }

        /// <summary>
        /// Lleva cartas del mazo de robo a la mano
        /// </summary>
        public void DrawFromDeck()
        {
            MoveLastCardsToHand(drawCards);
        }

        /// <summary>
        /// Roba una cantidad específica de cartas desde el mazo y las mueve a la mano.
        /// </summary>
        private void MoveLastCardsToHand(int cardsToDraw)
        {
            cardsToDraw = Mathf.Min(cardsToDraw, drawDeck.Count);

            for (int i = 0; i < cardsToDraw; i++)
            {
                // Obtener la última carta en el mazo
                GameObject cardToMove = drawDeck[drawDeck.Count - 1];

                // Quitar la carta del mazo
                drawDeck.RemoveAt(drawDeck.Count - 1);

                // Cambiar la posición y padre de la carta para moverla a la mano
                cardToMove.transform.SetParent(handArea);
                cardToMove.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

                // Agregar la carta a la lista de la mano
                handDeck.Insert(0, cardToMove);
            }

            // Organizar las cartas en la mano
            ArrangeHand();
        }

        /// <summary>
        /// Organiza las cartas en la mano en un diseño horizontal con el espacio definido.
        /// </summary>
        private void ArrangeHand()
        {
            for (int i = 0; i < handDeck.Count; i++)
            {
                GameObject card = handDeck[i];

                // Establece la posición de la carta en la mano
                card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

                // Asegura la rotación en (0, 0, 0)
                card.transform.rotation = Quaternion.Euler(90, -90, 0);
            }
        }

        /// <summary>
        /// Barajea las cartas del mazo de descarte y la mueve al mazo de robo
        /// </summary>
        public void ShuffleDiscardDeck()
        {
            // Aquí deberías implementar la lógica para barajar el mazo de descarte
        }

        /// <summary>
        /// Descarta la mano y roba 4 cartas
        /// </summary>
        public void Mulligan()
        {
            int handNumber = HandDeck.Count;


            if (HandDeck.Count >= handNumber)
            {
                for (int i = 0; i < handNumber; i++)
                {
                    GameObject cardToMove = handDeck[handDeck.Count - 1];
                    handDeck.RemoveAt(handDeck.Count - 1);

                    cardToMove.transform.SetParent(deckArea);
                    cardToMove.transform.localPosition = Vector3.zero;

                    drawDeck.Insert(0, cardToMove);
                }
                drawCards = (handNumber - 1);
                if (handNumber < 2)
                {
                    // Desactivar botón de mulligan si es necesario
                }
                this.DrawFromDeck();
            }

            //foreach (var card in handDeck)
            //{
            //    // Suponiendo que se maneja una pila de descarte, debes implementar la lógica aquí.
            //}
            //handDeck.Clear();
            //DrawFromDeck();
        }

        /// <summary>
        /// Coloca la carta
        /// </summary>
        public void PutCard()
        {
            if (handDeck.Count > 0)
            {
                var card = handDeck[0];
                handDeck.RemoveAt(0);
                // Aquí deberías implementar la lógica para colocar la carta en el mazo de descarte
            }
        }

        /// <summary>
        /// Jugar una carta desde la mano y agregarla a las cartas jugadas.
        /// </summary>
        public void PlayCard(GameObject card)
        {
            if (handDeck.Contains(card))
            {
                handDeck.Remove(card);
                card.transform.SetParent(discardDeckArea);
                playedCards.Add(card);
                // Opcional: Actualiza la posición de la carta en el tablero
                card.transform.localPosition = Vector3.zero;
            }
        }

        /// <summary>
        /// Mueve una carta desde la mano al mazo de descarte.
        /// </summary>
        /// <param name="card">La carta que se desea descartar.</param>
        public void DiscardCardFromHand(GameObject card)
        {
            if (handDeck.Contains(card))
            {
                handDeck.Remove(card); // Remueve la carta de la mano
                card.transform.SetParent(discardDeckArea); // Mueve la carta al área de descarte
                card.transform.localPosition = Vector3.zero; // Ajusta la posición según sea necesario
                card.transform.localRotation = Quaternion.identity; // Restablece la rotación si es necesario
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
            List<GameObject> cardsToDiscard = new List<GameObject>(handDeck);

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
            foreach (GameObject card in playedCards)
            {
                // Mueve la carta al mazo de descarte
                card.transform.SetParent(discardDeckArea);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.identity;
            }

            // Limpia la lista de cartas jugadas
            playedCards.Clear();
        }


        /// <summary>
        /// Limpia las cartas
        /// </summary>
        public void CleanPlayerCards()
        {
            handDeck.Clear();
            drawDeck.Clear();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Controles para hacer pruebas desde el editor.
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DrawFromDeck();
            }

            //if (Input.GetMouseButtonDown(2))
            //{
            //    Mulligan();
            //}
        }
#endif
    }
}
