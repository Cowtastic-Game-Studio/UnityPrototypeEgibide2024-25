using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentPhaseUI : MonoBehaviour
{
    #region Popiedades de la clase
    
    [SerializeField] private TMP_Text currentPhaseTextUI;

    #endregion

    #region Metodos publicos de la clase

    /// <summary>
    /// Metodo que actualiza el texto de la fase actual 
    /// </summary>
    /// <param name="phase">Texto que indica la fase actual</param>
    public void UpdateCurrentPhase(String phase)
    {
        currentPhaseTextUI.text = phase;
    }

    #endregion
}
