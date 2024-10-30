namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IStorage
    {
        #region Propiedades

        /// <summary>
        /// Máximo de recursos que puede almacenar.
        /// </summary>
        public GameResource Type { get; }

        /// <summary>
        /// Máximo de recursos que puede almacenar.
        /// </summary>
        public int MaxResources { get; }

        /// <summary>
        /// Nivel del almacenamiento.
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Cantidad de recursos actualmente almacenados.
        /// </summary>
        public int Resource { get; }

        #endregion
    }
}
