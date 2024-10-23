using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GamePhaseManager GamePhaseManager { get; private set; }
    public GameCalendar GameCalendar { get; private set; }
    // Puedes añadir más gestores aquí (AudioManager, UIManager, etc.)


    [Header("Calendar Events")]
    public List<CalendarEvent> calendarEvents; // Permitir agregar eventos desde el editor


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Para mantenerlo entre escenas, creo que no sera necesario !!!
        DontDestroyOnLoad(gameObject);  

        InitializeManagers();
    }

    private void InitializeManagers()
    {
        // Instanciar cualquier otro sistema que quieras manejar desde aquí
        GamePhaseManager = new GamePhaseManager();
        GameCalendar = new GameCalendar();
        // Instanciar otros managers si es necesario
    }

    private void Update()
    {
        // Puedes delegar el Update a los distintos sistemas si es necesario
        if (GamePhaseManager != null)
        {
            GamePhaseManager.Update();
        }
    } 
}
