using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPhase : IGamePhase
{
    public void EnterPhase()
    {
        // L�gica de inicializaci�n
        Debug.Log("Fase de inicializacion de tablero. NewGame o LoadGame");
    }

    public void ExecutePhase()
    {
        // L�gica principal, lo que sucede durante esta fase
    }

    public void EndPhase()
    {
        Debug.Log("Termina la fase de empezar el d�a.");
    }

    public void NewGame()
    {
        
    }

    public void LoadGame()
    {

    }
}
