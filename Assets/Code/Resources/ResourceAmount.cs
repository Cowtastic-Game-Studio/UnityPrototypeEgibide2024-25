namespace CowtasticGameStudio.MuuliciousHarvest
{
    [System.Serializable]
    public struct ResourceAmount
    {
        #region Propiedades
        /// <summary>
        /// Tipo de recurso
        /// </summary>
        public GameResource resourceType;

        /// <summary>
        /// Cantidad del recurso
        /// </summary>
        public int resourceQuantity;
        #endregion
    }
}