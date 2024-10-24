using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour, IEntity
{

    #region Propiedades de clase
    /// <summary>
    /// Cantidad de dias que dura el recurso
    /// </summary>
    public int LifeCycleDaysLeft { get; }

    /// <summary>
    /// Lista de recursos que produce
    /// </summary>
    public ResourceAmount ProducedResources { get; }

    /// <summary>
    /// Lista de recursos que requiere
    /// </summary>
    public ResourceAmount RequiredResources { get; }
    #endregion

    #region Metodos de clase
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
