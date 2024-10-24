using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour, IStorage
{
    #region Propiedades de clase
    /// <summary>
    /// Tipo de recurso que almacenara
    /// </summary>
    public GameResource Type { get; }

    /// <summary>
    /// Cantidad maxima que almacenara del recurso
    /// </summary>
    public int MaxResources { get; }

    /// <summary>
    /// Nivel del almacen
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// Cantidad que hay del recurso
    /// </summary>
    public int Resource { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor del almacen
    /// </summary>
    public Bank()
    {
        // Le indicamos que el tipo del recurso es
        this.Type = GameResource.Muuney;
    }
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
