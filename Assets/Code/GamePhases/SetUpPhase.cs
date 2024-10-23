using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPhase : IGamePhase
{
    public void EnterPhase()
    {
        // Lógica de inicialización
        Debug.Log("Fase de inicializacion de tablero. NewGame o LoadGame");
    }

    public void ExecutePhase()
    {
        // Lógica principal, lo que sucede durante esta fase
    }

    public void EndPhase()
    {
        Debug.Log("Termina la fase de empezar el día.");
    }

    public void NewGame()
    {
        
    }

    public void LoadGame()
    {

    }
}
