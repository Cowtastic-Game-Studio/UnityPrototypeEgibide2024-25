using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define el estado actual de cada carta
/// </summary>
public enum CardState
{
    onDeck,
    onHand,
    onTable
}

/// <summary>
/// Manages moving cards between decks and hands
/// </summary>
public class DeckAndHandManager : MonoBehaviour
{
    /// <summary>
    /// Estado de la carta
    /// </summary>
    public CardState state;

    /// <summary>
    /// El prefab de la carta a instanciar, es decir, el modelo que se muestra en las manos.
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// Lista de cartas en el mazo.
    /// </summary>
    public List<GameObject> DrawDeck = new List<GameObject>();

    /// <summary>
    /// Cartas en la mano.
    /// </summary>
    public List<GameObject> HandDeck = new List<GameObject>();

    /// <summary>
    /// Lugar donde se encuentra el mazo.
    /// </summary>
    public Transform deckArea;

    /// <summary>
    /// Lugar donde se encuentra la mano y se colocarán las cartas.
    /// </summary>
    public Transform handArea;

    /// <summary>
    /// Espaciado entre cartas en la mano.
    /// </summary>
    public float cardSpacing = 0.4f;

    /// <summary>
    /// Variable para definir desde el editor la cantidad de cartas a robar por turno.
    /// </summary>
    public int drawCards;

    /// <summary>
    /// Define el estado actual de cada carta
    /// </summary>
    public enum CardState
    {
        onDeck,
        onHand,
        onTable
    }

    /// <summary>
    /// Inicializa un mazo con un número específico de cartas, para pruebas.
    /// </summary>
    void InitializeDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            // Instanciar un nuevo GameObject de carta
            GameObject newCard = Instantiate(cardPrefab, deckArea);
            // Establece nombre y atributos de las cartas
            newCard.name = "Carta" + (i + 1);
            // Restablecer la posición dentro del área de mazo
            newCard.transform.localPosition = Vector3.zero;
            // Agregar carta al mazo
            DrawDeck.Add(newCard);
        }
    }

    /// <summary>
    /// Roba una cantidad específica de cartas desde el mazo y las mueve a la mano.
    /// </summary>
    public void MoveLastCardsToHand(int cardsToDraw)
    {
        cardsToDraw = Mathf.Min(cardsToDraw, DrawDeck.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            // Obtener la última carta en el mazo
            GameObject cardToMove = DrawDeck[DrawDeck.Count - 1];

            // Quitar la carta del mazo
            DrawDeck.RemoveAt(DrawDeck.Count - 1);

            // Cambiar la posición y padre de la carta para moverla a la mano
            cardToMove.transform.SetParent(handArea);
            cardToMove.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

            // Agregar la carta a la lista de la mano e insertar al principio
            HandDeck.Insert(0, cardToMove);
        }

        // Reorganizar las cartas en la mano después de mover todas las cartas
        ArrangeHand();
    }

    /// <summary>
    /// Organiza las cartas en la mano en un diseño horizontal con espacio de 1.
    /// </summary>
    private void ArrangeHand()
    {
        Camera mainCamera = Camera.main;

        for (int i = 0; i < HandDeck.Count; i++)
        {
            GameObject card = HandDeck[i];
            card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);
            card.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Método de robo que se ejecuta al inicio de la fase.
    /// </summary>
    public void Draw()
    {
        MoveLastCardsToHand(drawCards);
    }

    /// <summary>
    /// Método para realizar el mulligan y devolver cartas a la parte superior del mazo.
    /// </summary>
    public void Mulligan()
    {
        int handNumber = HandDeck.Count;
        if (HandDeck.Count >= handNumber)
        {
            for (int i = 0; i < handNumber; i++)
            {
                GameObject cardToMove = HandDeck[HandDeck.Count - 1];
                HandDeck.RemoveAt(HandDeck.Count - 1);

                cardToMove.transform.SetParent(deckArea);
                cardToMove.transform.localPosition = Vector3.zero;

                DrawDeck.Insert(0, cardToMove);
            }
            drawCards = (handNumber - 1);
            if (handNumber < 2)
            {
                // Desactivar botón de mulligan si es necesario
            }
            this.Draw();
        }
    }

#if UNITY_EDITOR
    void Start()
    {
        InitializeDeck(10); // Crea un mazo con 10 cartas para pruebas
    }

    /// <summary>
    /// Controles para hacer pruebas desde el editor.
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Draw();
        }

        if (Input.GetMouseButtonDown(2))
        {
            Mulligan();
        }
    }
#endif
}
