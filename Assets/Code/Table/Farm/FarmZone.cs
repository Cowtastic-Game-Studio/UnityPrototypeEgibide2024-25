using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmZone : MonoBehaviour, IDropZone<Cow>
{
    #region Propiedades de la clase
    /// <summary>
    /// Si esta activo o no
    /// </summary>
    public bool IsActive { get; }

    /// <summary>
    /// El tipo de la zona
    /// </summary>
    public Cow Type { get; }
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
