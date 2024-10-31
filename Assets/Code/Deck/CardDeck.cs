using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CardDeck : IDeck
    {
        // Implementa la pila de cartas
        private Stack<ICard> _cards;

        public CardDeck(IEnumerable<ICard> initialCards = null)
        {
            _cards = initialCards != null ? new Stack<ICard>(initialCards) : new Stack<ICard>();
        }

        // Propiedad para acceder a la pila
        public Stack<ICard> Cards => _cards;

        public void Shuffle()
        {
            var cardsList = new List<ICard>(_cards);
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

        public ICard Draw()
        {
            if (_cards.Count > 0)
            {
                ICard card = _cards.Pop();
                // Activa la carta antes de devolverla
                card.Activate();
                return card;
            }
            return null;
        }

        public void Place(ICard card)
        {
            _cards.Push(card);
        }
    }
}
