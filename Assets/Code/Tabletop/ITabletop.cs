﻿namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface ITabletop
    {
        #region Methods

        /// <summary>
        /// Aplica los puntos de accion de la carta
        /// </summary>
        /// <param name="card">Carta</param>
        public void OnCardUseActionPoints(ICard card);

        #endregion

    }
}