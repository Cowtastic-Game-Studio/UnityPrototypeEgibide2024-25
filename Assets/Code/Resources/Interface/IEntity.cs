using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    #region Propiedades
    /// <summary>
    /// Cantidad de dias que dura el recurso
    /// </summary>
    int LifeCycleDaysLeft { get; }

    /// <summary>
    /// Lista de recursos que produce
    /// </summary>
    ResourceAmount ProducedResources { get; }

    /// <summary>
    /// Lista de recursos que requiere
    /// </summary>
    ResourceAmount RequiredResources { get; }
    #endregion
}
