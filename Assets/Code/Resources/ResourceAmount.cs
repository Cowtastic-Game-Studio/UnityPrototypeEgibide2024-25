using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ResourceAmount : MonoBehaviour
{
    #region Propiedades de clase
    /// <summary>
    /// Tipo de recurso
    /// </summary>
    private GameResource resourceType;

    /// <summary>
    /// Cantidad del recurso
    /// </summary>
    private int quantity;
    #endregion
}
