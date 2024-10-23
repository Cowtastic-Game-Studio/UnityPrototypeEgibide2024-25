namespace CowtasticGameStudio.MuuliciousHarvest.Tabletop
{
    internal interface ITabletop
    {
        #region Methods

        /// <summary>
        /// Aplica los puntos de accion de la carta
        /// </summary>
        /// <param name="card">Carta</param>
        public void UseCardActionPoints(ICard card);
        
        #endregion

    }
}