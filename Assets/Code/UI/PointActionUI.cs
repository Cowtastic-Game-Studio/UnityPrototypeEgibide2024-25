using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointActionUI : MonoBehaviour
{
    #region Popiedades de la clase

    [SerializeField] private TMP_Text actionPointTextUI;

    #endregion

    #region Metodos publicos de la clase

    /// <summary>
    /// Metodo que actualiza el texto de los puntos de acciones 
    /// </summary>
    /// <param name="points">Cantidad de los puntos de accion actuales</param>
    public void UpdateActionPoints(int points)
    {
        actionPointTextUI.text = points.ToString() + " PA";
    }

    #endregion
}
