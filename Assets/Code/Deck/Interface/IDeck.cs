﻿using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IDeck
    {
        #region Propiedades

        public Stack<ICard> CardsStack { get; }

        #endregion

        #region Methods
        /// <summary>
        /// Barajea las cartas
        /// </summary>
        public void Shuffle();

        /// <summary>
        /// Obtiene la carta superior del mazo
        /// </summary>
        /// <returns>Devuelve la carta robada</returns>
        public ICard Draw();

        /// <summary>
        /// Coloca la carta en la parte superior del mazo
        /// </summary>
        /// <param name="card">Carta a colocar</param>
        public void Place(ICard card);

        #endregion

    }
}
