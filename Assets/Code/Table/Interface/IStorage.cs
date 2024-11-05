namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IStorage
    {
        #region Propiedades

        /// <summary>
        /// Máximo de recursos que puede almacenar.
        /// </summary>
        public GameResource Type { get; set; }

        /// <summary>
        /// Máximo de recursos que puede almacenar.
        /// </summary>
        public int MaxResources { get; set; }

        /// <summary>
        /// Nivel del almacenamiento.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Cantidad de recursos actualmente almacenados.
        /// </summary>
        public int Resource { get; set; }

        #endregion
    }
}
