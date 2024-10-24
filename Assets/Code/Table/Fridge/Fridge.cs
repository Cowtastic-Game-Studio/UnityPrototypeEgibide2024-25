using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour, IStorage
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

    #region Constructor de la clase
    /// <summary>
    /// Constructor del almacen
    /// </summary>
    public Fridge()
    {
        // Le indicamos que el tipo del recurso es
        this.Type = GameResource.Milk;
    }
    #endregion

    #region Metodos de la clase
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
