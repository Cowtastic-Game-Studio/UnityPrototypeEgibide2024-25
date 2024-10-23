using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    /// <summary>
    /// Nombre de la carta
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Descripcion de la carta
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Coste de la carta
    /// </summary>
    int Cost { get; }

    /// <summary>
    /// Puntos de accion que cuesta la carta
    /// </summary>
    int ActionPoints { get; }

    /// <summary>
    /// Dias de vida de la carta
    /// </summary>
    int LifeCycleDays { get; }

    /// <summary>
    /// Lista de recursos necesarios para la carta
    /// </summary>
    List<ResourceAmmount> RequiredResources { get; }

    /// <summary>
    /// Lista de recursos que produce la carta
    /// </summary>
    List<ResourceAmmount> ProducedResources { get; }
}