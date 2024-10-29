using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
#endif

public class DeckAndHandManager : MonoBehaviour
{
    /// <summary>
    /// El prefab de la carta a instanciar, es decir, el modelo que se muestra en las manos
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// Lista de cartas en el mazo
    /// </summary>
    public List<GameObject> DrawDeck = new List<GameObject>();

    /// <summary>
    /// Cartas en la mano
    /// </summary>
    public List<GameObject> HandDeck = new List<GameObject>();

    /// <summary>
    /// Lugar donde se haya el mazo
    /// </summary>
    public Transform deckArea;

    /// <summary>
    /// Lugar donde se haya la mano y se colocaran las cartas
    /// </summary>
    public Transform handArea;

    /// <summary>
    /// Espaciado entre cartas
    /// </summary>
    public float cardSpacing = 0.4f;

    /// <summary>
    /// Variable para poder decidir desde el editor la cantidad de cartas que se roban por turno.
    /// </summary>
    public int drawCards;

    /// <summary>
    /// Crea un mazo con un n�mero espec�fico de cartas, utilizado aqu� para pruebas de instanciaci�n.
    /// </summary>
    /// <param name="numberOfCards">N�mero de cartas a generar en el mazo para pruebas</param>
    void InitializeDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            // Instanciar un nuevo GameObject de carta en el �rea del mazo
            GameObject newCard = Instantiate(cardPrefab, deckArea);

            // Establece un nombre �nico para distinguir las cartas
            newCard.name = "Carta" + (i + 1);

            // Restablecer la posici�n inicial dentro del �rea de mazo
            newCard.transform.localPosition = Vector3.zero;

            // Agregar carta al mazo (lista DrawDeck)
            DrawDeck.Add(newCard);
        }
    }

    /// <summary>
    /// Roba un n�mero espec�fico de cartas desde el mazo y las mueve a la mano,
    /// destruyendo las cartas originales en el mazo para evitar duplicados en la escena.
    /// </summary>
    /// <param name="cardsToDraw">N�mero de cartas a robar y mover a la mano</param>
    public void MoveLastCardsToHand(int cardsToDraw)
    {
        // Asegura que no se roben m�s cartas de las disponibles en el mazo
        cardsToDraw = Mathf.Min(cardsToDraw, DrawDeck.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            // Obtener la �ltima carta en el mazo (�ltimo elemento en la lista DrawDeck)
            GameObject originalCard = DrawDeck[DrawDeck.Count - 1];

            // Quitar la carta del mazo (lista DrawDeck)
            DrawDeck.RemoveAt(DrawDeck.Count - 1);

            // Instanciar una nueva carta del prefab en el �rea de la mano (handArea)
            GameObject newCard = Instantiate(cardPrefab, handArea);

            // Establecer las propiedades de la nueva carta basadas en la original
            newCard.name = originalCard.name;

            // Destruir el objeto de la carta original para evitar duplicados en la escena
            Destroy(originalCard);

            // Agregar la nueva carta a la mano (lista HandDeck), insert�ndola al inicio
            // para que la carta m�s reciente est� a la izquierda
            HandDeck.Insert(0, newCard);
        }

        // Reorganizar las cartas en la mano despu�s de mover todas las cartas
        ArrangeHand();
    }

    /// <summary>
    /// Organiza las cartas en la mano en un dise�o horizontal con un espacio especificado entre cada carta,
    /// asegur�ndose de que cada carta mantenga su rotaci�n original.
    /// </summary>
    private void ArrangeHand()
    {
        // Obtener la referencia de la c�mara principal para orientar las cartas hacia ella
        Camera mainCamera = Camera.main;

        for (int i = 0; i < HandDeck.Count; i++)
        {
            GameObject card = HandDeck[i];

            // Posicionar la carta en la mano, aplicando espaciado horizontal seg�n su �ndice
            card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

            // Asegurarse de que la carta mantenga su rotaci�n original del prefab
            card.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// M�todo de robo que se ejecuta al principio de la fase de inicio, invocando
    /// el m�todo MoveLastCardsToHand para robar la cantidad especificada de cartas.
    /// </summary>
    public void Draw()
    {
        MoveLastCardsToHand(drawCards);
    }

    /// <summary>
    /// M�todo para hacer un "Mulligan", que devuelve las cartas de la mano al mazo
    /// y permite robar de nuevo, destruyendo las cartas originales en la mano para evitar duplicados.
    /// </summary>
    public void Mulligan()
    {
        int handNumber = HandDeck.Count;

        // Si el n�mero de cartas en la mano es suficiente para un Mulligan
        if (HandDeck.Count >= handNumber)
        {
            for (int i = 0; i < handNumber; i++)
            {
                // Obtener la �ltima carta en la mano
                GameObject originalCard = HandDeck[HandDeck.Count - 1];

                // Quitar la carta de la lista de la mano
                HandDeck.RemoveAt(HandDeck.Count - 1);

                // Instanciar una nueva carta en el �rea del mazo
                GameObject newCard = Instantiate(cardPrefab, deckArea);

                // Establecer las propiedades de la nueva carta basadas en la original
                newCard.name = originalCard.name;

                // Destruir el objeto de la carta original para evitar duplicados en la escena
                Destroy(originalCard);

                // Agregar la nueva carta al mazo (lista DrawDeck) al inicio
                DrawDeck.Insert(0, newCard);
            }

            // Actualizar la cantidad de cartas para robar en el pr�ximo Draw
            drawCards = (handNumber - 1);

            if (handNumber < 2)
            {
                // TODO: Desactivar el bot�n de Mulligan si se cumplen las condiciones
            }

            // Ejecuta un nuevo Draw despu�s del Mulligan
            this.Draw();
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Inicializa el mazo con 10 cartas al iniciar el juego en modo Editor para pruebas.
    /// </summary>
    void Start()
    {
        InitializeDeck(10);
    }

    /// <summary>
    /// Controles para hacer pruebas en modo Editor: Click derecho para Draw y click central para Mulligan.
    /// </summary>
    private void Update()
    {
        // Prueba para ejecutar el m�todo Draw cuando se presiona el bot�n derecho del rat�n
        if (Input.GetMouseButtonDown(1))
        {
            Draw();
        }

        // Prueba para ejecutar el m�todo Mulligan cuando se presiona el bot�n central del rat�n
        if (Input.GetMouseButtonDown(2))
        {
            Mulligan();
        }
    }
#endif
}
