using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDeck : IDeck
    {
        // Implementa la pila de cartas
        private Stack<GameObject> _cards;

        public CardDeck(IEnumerable<GameObject> initialCards = null)
        {
            _cards = initialCards != null ? new Stack<GameObject>(initialCards) : new Stack<GameObject>();
        }

        // Propiedad para acceder a la pila
        public Stack<GameObject> Cards => _cards;

        public void Shuffle()
        {
            var cardsList = new List<GameObject>(_cards);
            _cards.Clear();

            // Baraja la lista
            for (int i = cardsList.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (cardsList[i], cardsList[j]) = (cardsList[j], cardsList[i]);
            }

            // Reconstruye la pila a partir de la lista barajada
            foreach (var card in cardsList)
            {
                _cards.Push(card);
            }
        }

        public GameObject Draw()
        {
            if (_cards.Count > 0)
            {
                GameObject card = _cards.Pop();
                //TODO: Activa la carta antes de devolverla, si es necesario
                return card;
            }
            return null;
        }

        public void Place(GameObject card)
        {
            _cards.Push(card);
        }

        // Método para obtener una copia de la carta en la parte superior sin eliminarla
        public GameObject Peek()
        {
            if (_cards.Count > 0)
            {
                GameObject originalCard = _cards.Peek(); // Obtiene la carta en la parte superior sin quitarla
                return Object.Instantiate(originalCard); // Clona el objeto
            }
            return null;
        }

        // Método para eliminar una carta específica
        public bool RemoveCard(GameObject targetCard)
        {
            bool cardRemoved = false;
            Stack<GameObject> tempStack = new Stack<GameObject>();

            // Mover cartas a la pila temporal hasta encontrar la carta específica
            while (_cards.Count > 0)
            {
                GameObject currentCard = _cards.Pop();

                if (currentCard == targetCard && !cardRemoved)
                {
                    cardRemoved = true;
                    // La carta específica se encuentra y se elimina, no se agrega a tempStack
                }
                else
                {
                    // Si no es la carta específica, la movemos a tempStack
                    tempStack.Push(currentCard);
                }
            }

            // Restaurar las cartas a la pila original en el orden correcto
            while (tempStack.Count > 0)
            {
                _cards.Push(tempStack.Pop());
            }

            return cardRemoved; // Devuelve true si la carta se eliminó correctamente
        }
    }
}
