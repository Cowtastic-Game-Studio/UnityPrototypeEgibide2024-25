using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class ICard : ScriptableObject
{
    #region variables de la clase
    /// <summary>
    /// Nombre de la carta
    /// </summary>
    private string name;

    /// <summary>
    /// Descripcion de la carta
    /// </summary>
    private string description;

    /// <summary>
    /// Coste de la carta
    /// </summary>
    private int cost;

    /// <summary>
    /// Puntos de accion que cuesta la carta
    /// </summary>
    private int actionPoints;

    /// <summary>
    /// Dias de vida de la carta
    /// </summary>
    private int lifeCycleDays;

    /// <summary>
    /// Lista de recursos necesarios para la carta
    /// </summary>
    private List<ResourceAmmount> requiredResources;

    /// <summary>
    /// Lista de recursos que produce la carta
    /// </summary>
    private List<ResourceAmmount> producedResources;

    #endregion
}
