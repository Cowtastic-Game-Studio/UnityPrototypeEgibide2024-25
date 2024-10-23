using UnityEngine;

public interface IStorage
{
    /// <summary>
    /// Máximo de recursos que puede almacenar.
    /// </summary>
    int MaxResources { get; }

    /// <summary>
    /// Nivel del almacenamiento.
    /// </summary>
    int Level { get; }

    /// <summary>
    /// Cantidad de recursos actualmente almacenados.
    /// </summary>
    int Resource { get; }
}
