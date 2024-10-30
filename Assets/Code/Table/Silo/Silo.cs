using UnityEngine;

public class Silo : MonoBehaviour, IStorage
{
    #region Propiedades

    public GameResource Type { get; }
    public int MaxResources { get; }
    public int Level { get; }
    public int Resource { get; }

    #endregion

    #region Constructor
    /// <summary>
    /// Constructor del almacen
    /// </summary>
    public Silo()
    {
        // Le indicamos que el tipo del recurso es
        Type = GameResource.Cereal;
    }

    #endregion

    #region Metodos

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
