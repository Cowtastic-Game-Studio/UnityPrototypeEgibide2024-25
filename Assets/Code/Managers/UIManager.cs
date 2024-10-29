using System.Collections;
using System.Collections.Generic;
using CowtasticGameStudio.MuuliciousHarvest;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    #region Popiedades de la clase
    
    [SerializeField] private TMP_Text currentPhaseTextUI;
    [SerializeField] private TMP_Text actionPointTextUI;
    [SerializeField] private GameObject mulliganButton;
    [SerializeField] private GameObject actionPointsPanel;
    private GamePhaseManager gamePhaseManager;

    #endregion

    #region Metodos publicos de la clase

    /// <summary>
    /// Metodo que actualiza el texto de la fase actual 
    /// </summary>
    /// <param name="phase">Texto que indica la fase actual</param>
    public void UpdateCurrentPhase(string phase)
    {
        currentPhaseTextUI.text = phase;
    }

    /// <summary>
    /// Metodo que actualiza el texto de los puntos de acciones 
    /// </summary>
    /// <param name="points">Cantidad de los puntos de accion actuales</param>
    public void UpdateActionPoints(int points)
    {
        actionPointTextUI.text = points.ToString() + " PA";
    }

    public void NextPhaseButton()
    {
        gamePhaseManager.NextPhase();
    }

    public void HideMulliganButton()
    {
        mulliganButton.SetActive(false);
    }

    public void ShowMulliganButton()
    {
        mulliganButton.SetActive(true);
    }

    public void HideAPPanel()
    {
        actionPointsPanel.SetActive(false);
    }

    public void ShowAPPanel()
    {
        actionPointsPanel.SetActive(true);
    }

    #endregion

    void Start()
    {
        gamePhaseManager = new GamePhaseManager();
    }
}
