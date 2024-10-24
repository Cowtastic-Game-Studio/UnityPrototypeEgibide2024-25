using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropZone<IEntity>
{
    #region Propiedades
    /// <summary>
    /// Si esta activo o no
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// El tipo de la zona
    /// </summary>
    IEntity Type { get; }
    #endregion
}
