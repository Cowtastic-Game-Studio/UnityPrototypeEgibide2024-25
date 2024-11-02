using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface ICardsManager
    {
        #region Properties

        public IDeck Deck { get; }

        public List<ICard> Hand { get; }

        public IDeck DiscardPile { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Lleva cartas del mazo de robo a la mano
        /// </summary>
        public void DrawFromDeck();

        /// <summary>
        /// Barajea las cartas del mazo de descarte y la mueve al mazo de robo
        /// </summary>
        public void ShuffleDiscardDeck();

        /// <summary>
        /// Descarta la mano y roba 4 cartas
        /// </summary>
        public void Mulligan();

        /// <summary>
        /// Coloca la carta
        /// </summary>
        public void PutCard();

        /// <summary>
        /// Limpia las cartas
        /// </summary>
        public void CleanPlayerCards();

        #endregion

    }
}
