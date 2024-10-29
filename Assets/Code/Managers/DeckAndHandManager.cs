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
    public List<GameObject> deck = new List<GameObject>();
    /// <summary>
    /// Cartas en la mano
    /// </summary>
    public List<GameObject> hand = new List<GameObject>();
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
    /// Inicializa un mazo con un n�mero espec�fico de cartas, este ha sido usado para las pruebas, borrar mas adelante o intercambiarlo por el mazo en si
    /// </summary>
    /// <param name="numberOfCards">Cuantas cartas quieres generar</param>
    void InitializeDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            // Instanciar un nuevo GameObject de carta
            GameObject newCard = Instantiate(cardPrefab, deckArea);

            // Establecer un nombre �nico o atributos para distinguir las cartas
            newCard.name = "Carta " + (i + 1);
            newCard.transform.localPosition = Vector3.zero;  // Restablecer posici�n dentro de deckArea

            // Agregar la carta a la lista del mazo
            deck.Add(newCard);
        }
    }

    /// <summary>
    /// La quid de la cuestion, esto se encarga de robar las 4 ultimas cartas del mazo, y de trasladarlas a la mano. Otra funcion las ordena y coloca mas adelante
    /// </summary>
    public void MoveLastCardsToHand()
    {
        // Mover hasta 4 cartas, o menos si el mazo tiene menos de 4
        int cardsToMove = Mathf.Min(4, deck.Count);

        for (int i = 0; i < cardsToMove; i++)
        {
            // Obtener la �ltima carta en el mazo
            GameObject originalCard = deck[deck.Count - 1];

            // Quitar la carta del mazo
            deck.RemoveAt(deck.Count - 1);

            // Instanciar una nueva carta del prefab
            GameObject newCard = Instantiate(cardPrefab, handArea);

            // Establecer las propiedades de la nueva carta basadas en la original
            newCard.name = originalCard.name; // Copiar el nombre o establecer cualquier otro atributo seg�n sea necesario

            // Las futuras propiedades de cartas aplicarlas aqu�, ejemplo para ello
            // En caso de tener script con atributos, es posible hacerlo asi:
            // CardAttributes originalAttributes = originalCard.GetComponent<CardAttributes>();
            // CardAttributes newAttributes = newCard.GetComponent<CardAttributes>();
            // newAttributes.CopyFrom(originalAttributes); // Implementar el m�todo CopyFrom para copiar datos

            // Agregar la nueva carta a la mano e insertar al principio para mantener la m�s reciente a la izquierda
            hand.Insert(0, newCard);
        }

        // Reorganizar las cartas en la mano despu�s de mover todas las cartas
        ArrangeHand();
    }

    /// <summary>
    /// Organiza las cartas en la mano en un dise�o horizontal con espacio de 1, es el encargado principal de ello
    /// </summary>
    private void ArrangeHand()
    {
        // Obtener la referencia de la c�mara principal para que est�n mirando a la camara
        Camera mainCamera = Camera.main;

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i];
            // Posicionar la carta seg�n su �ndice y espacio
            card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

            // Asegurarse de que la carta mantenga su rotaci�n original del prefab
            // Establecer a la rotaci�n original del prefab si es necesario
            card.transform.rotation = Quaternion.identity;
        }
    }

#if UNITY_EDITOR
    // Crear un mazo con 10 cartas
    void Start()
    {
        InitializeDeck(10);
    }
    /// <summary>
    /// Permite mover las �ltimas cartas a la mano al presionar el bot�n derecho del rat�n
    /// </summary>
    private void Update()
    {
        // Si se presiona el bot�n derecho del rat�n en el editor
        if (Input.GetMouseButtonDown(1))
        {
            MoveLastCardsToHand();
        }
    }
#endif
}
