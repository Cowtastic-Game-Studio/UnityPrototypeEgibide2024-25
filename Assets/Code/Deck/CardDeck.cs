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

        // M�todo para obtener una copia de la carta en la parte superior sin eliminarla
        public GameObject Peek()
        {
            if (_cards.Count > 0)
            {
                GameObject originalCard = _cards.Peek(); // Obtiene la carta en la parte superior sin quitarla
                return Object.Instantiate(originalCard); // Clona el objeto
            }
            return null;
        }
    }
}
