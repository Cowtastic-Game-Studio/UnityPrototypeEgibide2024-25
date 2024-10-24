using UnityEngine;

public interface IStorage
{
    /// <summary>
    /// Máximo de recursos que puede almacenar.
    /// </summary>
    GameResource type { get; }

    /// <summary>
    /// Máximo de recursos que puede almacenar.
    /// </summary>
    int maxResources { get; }

    /// <summary>
    /// Nivel del almacenamiento.
    /// </summary>
    int level { get; }

    /// <summary>
    /// Cantidad de recursos actualmente almacenados.
    /// </summary>
    int resource { get; }
}
