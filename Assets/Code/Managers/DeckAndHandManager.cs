using System.Collections.Generic;
using Unity.VisualScripting;
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
    /// Crea un mazo con un numero de cartas a especificar para hacer pruebas.
    /// </summary>
    void initializeDeck(int numberOfCards)
    {
        for(int i = 0; i < numberOfCards; i++)
        {
            //Instanciar un nuevo gameObject de carta
            GameObject newCard = Instantiate(cardPrefab, deckArea);
            //Establece nombre y atributos de las cartas
            newCard.name="Carta" + (i+1);
            //Restablecer la posiion dentro del area de mazo
            newCard.transform.localPosition = Vector3.zero;
            //Agregar carta al mazo
            DrawDeck.Add(newCard);
        }
    }

    /// <summary>
    /// La quid de la cuestion, esto se encarga de robar las ultimas cartas del mazo, y de trasladarlas a la mano. Otra funcion las ordena y coloca mas adelante
    /// </summary>
    public void MoveLastCardsToHand(int cardsToDraw)
    {
        // Mover hasta el numero de cartas que recibe, o menos si el mazo tiene menos de dicho numero
            cardsToDraw = Mathf.Min(cardsToDraw, DrawDeck.Count);

        for (int i = 0; i < cardsToDraw; i++)
        {
            // Obtener la �ltima carta en el mazo
            GameObject originalCard = DrawDeck[DrawDeck.Count - 1];

            // Quitar la carta del mazo
            DrawDeck.RemoveAt(DrawDeck.Count - 1);

            // Instanciar una nueva carta del prefab
            GameObject newCard = Instantiate(cardPrefab, handArea);

            // Establecer las propiedades de la nueva carta basadas en la original
            newCard.name = originalCard.name; 
            // TODO: Asignar el resto de atributos de las cartas
                
            //Destruye el objeto de la carta que has retirado para que no se quede una carta vacia en la mano.
            Destroy(originalCard);

            // Agregar la nueva carta a la mano e insertar al principio para mantener la mas reciente a la izquierda
            HandDeck.Insert(0, newCard);
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

        for (int i = 0; i < HandDeck.Count; i++)
        {
            GameObject card = HandDeck[i];
            // Posicionar la carta seg�n su �ndice y espacio
            card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

            // Asegurarse de que la carta mantenga su rotaci�n original del prefab
            // Establecer a la rotaci�n original del prefab si es necesario
            card.transform.rotation = Quaternion.identity;
        }
    }
    /// <summary>
    /// El metodo de robo que se ejecuta a principio de la fase de inicio.
    /// </summary>
    private void Draw(){
            MoveLastCardsToHand(drawCards);
    }

    /// <summary>
    /// El metodo de mulligan
    /// </summary>
    private void Mulligan(int handNumber){
        if(HandDeck.Count >= handNumber){
            for (int i = 0; i < handNumber; i++)
            {
                // Obtener la ultima carta en el mazo
                GameObject originalCard = HandDeck[HandDeck.Count - 1];

                // Quitar la carta de la lista de la mano.
                HandDeck.RemoveAt(HandDeck.Count - 1);

                // Instanciar una nueva carta del prefab
                GameObject newCard = Instantiate(cardPrefab, handArea);

                // Establecer las propiedades de la nueva carta basadas en la original
                newCard.name = originalCard.name;
                // TODO: Asignar el resto de atributos de las cartas
                
                //Destruye el objeto de la carta que has retirado para que no se quede una carta vacia en la mano.
                Destroy(originalCard);
                
                // Agregar la nueva carta a la mano e insertar al principio para mantener la m�s reciente a la izquierda
                DrawDeck.Insert(0, newCard);
            }
        drawCards = (handNumber - 1);
        if(handNumber < 2)
        {
            // TODO: Desactivar el boton de mulligan
        }
        this.Draw();
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
        if (Input.GetMouseButtonDown(0))
        {
             Draw();
        }

         if (Input.GetMouseButtonDown(1))
        {
             Mulligan(HandDeck.Count);
        }
    }
#endif
}
