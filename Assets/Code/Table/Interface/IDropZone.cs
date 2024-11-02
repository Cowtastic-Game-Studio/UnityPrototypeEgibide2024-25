namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IDropZone<IEntity>
    {
        #region Propiedades

        bool IsActive { get; }
        IEntity Type { get; }

        #endregion
    }
}